using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Core.Domain.RepositoryContracts;
using JeevanRakt.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace JeevanRakt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IHubContext<ProductNotificationHub, INotificationHub> _productNotification;
        private readonly ILogger<MessageController> _logger;

        public MessageController(IHubContext<ProductNotificationHub, INotificationHub> hubContext, ILogger<MessageController> logger)
        {
            _productNotification = hubContext;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task UpdateProduct(Notification notification)
        {
            _logger.LogInformation("Sending notification to Admin group");
            await _productNotification.Clients.Group("Admin").SendMessage(notification);
        }
    }
}
