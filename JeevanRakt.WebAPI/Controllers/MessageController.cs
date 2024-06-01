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

        public MessageController(IHubContext<ProductNotificationHub, INotificationHub> hubContext)
        {
            _productNotification = hubContext;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task UpdateProduct(Notification notification)
        {
            await _productNotification.Clients.All.SendMessage(notification);
        }
    }
}
