using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Infrastructure.DataBase;
using Microsoft.AspNetCore.Authorization;
using System.Drawing.Printing;
using System.Globalization;
using JeevanRakt.Core.Services;
using JeevanRakt.Core.Domain.RepositoryContracts;
using Microsoft.AspNetCore.Identity;
using JeevanRakt.Core.Domain.Identity;

namespace JeevanRakt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecipientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IRecipientRepository _recipientRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public RecipientsController(ApplicationDbContext context, IRecipientRepository recipientRepository, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _recipientRepository = recipientRepository;
            _userManager = userManager;
        }

        // GET: api/Recipients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipient>>> GetRecipients(int page = 1, int pageSize = 10, string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAccending = true)
        {
            IEnumerable<Recipient> recipients = await _recipientRepository.GetRecipientsAsync(page,pageSize,filterOn,filterQuery,sortBy,isAccending);

            if(recipients == null)
            {
                return BadRequest("recipients not found");
            }

            return Ok(recipients);
        }

        // GET: api/Recipients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipient>> GetRecipient(Guid id)
        {
            Recipient recipient = await _recipientRepository.GetRecipientAsync(id);

            if(recipient == null)
            {
                return BadRequest("recipient not found");
            }

            return Ok(recipient);
        }

        // PUT: api/Recipients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipient(Guid id, Recipient recipient)
        {
            bool result = await _recipientRepository.UpdateRecipientAsync(id, recipient);

            if(result == false)
            {
                BadRequest("recipient update fail");
            }

            return NoContent();
            
        }

        // POST: api/Recipients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recipient>> PostRecipient(Recipient recipient)
        {
            ApplicationUser? user = await _userManager.GetUserAsync(User);
            if (user == null) { BadRequest("user not found"); }

            recipient.UserId = user.Id;

            Recipient recipient1 = await _recipientRepository.AddRecipientAsync(recipient);
            

            return Ok(recipient1);
        }

        // DELETE: api/Recipients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipient(Guid id)
        {
            bool result = await _recipientRepository.DeleteRecipientAsync(id);

            if(result == false)
            {
                BadRequest("deletion fail");
            }

            return NoContent();
        }

       

        // GET: api/Donors/bloodbank
        [HttpGet("bloodbank")]
        public async Task<ActionResult<IEnumerable<Recipient>>> GetRecipientsById([FromQuery] Guid bloodbankId, int page = 1, int pageSize = 10, string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAccending = true)
        {
            IEnumerable<Recipient> recipients = await _recipientRepository.GetRecipientsByBloodBankIdAsync(bloodbankId,page, pageSize, filterOn, filterQuery, sortBy, isAccending);

            if(recipients == null)
            {
                BadRequest("Recipients not found");
            }
            return Ok(recipients); 

        }

        [HttpGet("generate")]
        [AllowAnonymous]
        public async Task<IActionResult> GenerateData()
        {
            await _recipientRepository.GenerateTestDataAsync();
            List<Recipient> recipients = await _context.Recipients.ToListAsync();

            return Ok(recipients);
        }


        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingRequest()
        {
            ApplicationUser? user = await _userManager.GetUserAsync(User);

            if(user == null) { return BadRequest("user not found"); }

           IEnumerable<Recipient> recipients =  await _recipientRepository.GetUnpaidRequest(user);

            return Ok(recipients);
        }
    }
}
