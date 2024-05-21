using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Core.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Core.Domain.RepositoryContracts
{
    public interface IRecipientRepository
    {
        Task<IEnumerable<Recipient>> GetRecipientsAsync(int page = 1, int pageSize = 10, string filterOn = null, string filterQuery = null, string sortBy = null, bool isAscending = true);
        Task<IEnumerable<Recipient>> GetUnpaidRequest(ApplicationUser user);
        Task<Recipient> GetRecipientAsync(Guid id);
        Task<Recipient> AddRecipientAsync(Recipient recipient);
        Task<bool> UpdateRecipientAsync(Guid id, Recipient recipient);
        Task<bool> DeleteRecipientAsync(Guid id);
        Task<IEnumerable<Recipient>> GetRecipientsByBloodBankIdAsync(Guid bloodbankId, int page = 1, int pageSize = 10, string filterOn = null, string filterQuery = null, string sortBy = null, bool isAscending = true);
        Task GenerateTestDataAsync();

    }
}
