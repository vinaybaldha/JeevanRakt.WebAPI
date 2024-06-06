using JeevanRakt.Core.Domain.RepositoryContracts;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Core.Services
{
    public class ProductNotificationHub : Hub<INotificationHub>
    {
        public async Task AddToRoleGroup(string role)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, role);
            Console.WriteLine($"Connection {Context.ConnectionId} added to group {role}");
        }

        public async Task RemoveFromRoleGroup(string role)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, role);
            Console.WriteLine($"Connection {Context.ConnectionId} removed from group {role}");
        }
    }
}
