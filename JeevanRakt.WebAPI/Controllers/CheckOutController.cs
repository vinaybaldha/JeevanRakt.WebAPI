using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Core.Domain.RepositoryContracts;
using JeevanRakt.Infrastructure.DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Stripe.Checkout;


namespace JeevanRakt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckOutController : ControllerBase
    {
        private IRecipientRepository _recipientRepository;
        private IHttpContextAccessor _httpContextAccessor;
        private ApplicationDbContext _context;
        public CheckOutController(IRecipientRepository recipientRepository, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _recipientRepository = recipientRepository;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CheckOut(Guid recipientId)
         {
            Recipient recipient = await _recipientRepository.GetRecipientAsync(recipientId);

            var domain = "https://localhost:7016/";

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain+"api/CheckOut/OrderConfirmation?session_id={CHECKOUT_SESSION_ID}&recipientId="+recipient.RecipientId,
                CancelUrl = domain+"CheckOut/Login",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment"
            };

         
                var sessionListItems = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = 10000,
                        Currency = "inr",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = recipient.RecipientName,
                            Description = "1 unit of "+recipient.RecipientBloodType + "blood-type"
                        }
                    },
                    Quantity = 1
                };

                options.LineItems.Add(sessionListItems);
            

            var service = new SessionService();

            Session session = await service.CreateAsync(options);



            // Store session ID in HttpContext.Items

           

            Response.Headers.Add("Location", session.Url);

            return Ok(new { url = session.Url });

        }

        [HttpGet("OrderConfirmation")]
        [AllowAnonymous]
        public async Task<IActionResult> OrderConfirmation([FromServices] IHttpContextAccessor httpContextAccessor, [FromQuery] string session_id, Guid recipientId)
        {
            var service = new SessionService();

            var domain = "http://localhost:4200/";

            Recipient recipient = await _recipientRepository.GetRecipientAsync(recipientId);


            if (session_id == null)
            {
                return BadRequest("something went wrong");
            }

            Session session = service.Get(session_id);

            if(session.PaymentStatus == "paid")
            {
                recipient.PaymentStatus = session.PaymentStatus;
                await _context.SaveChangesAsync();

                return Redirect($"{domain}success");
            }

            return Redirect($"{domain}fail");
        }
    }
}
