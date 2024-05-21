using JeevanRakt.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Core.Domain.RepositoryContracts
{
    public interface IBloodBankService
    {
        BloodBank FindNearestBloodBank(double userLatitude, double userLongitude);
        Task<BloodBank> GetBloodBankAsync(Guid id);
        Task<IEnumerable<BloodBank>> GetBloodBanksAsync(int page = 1, int pageSize = 10, string filterOn = null, string filterQuery = null, string sortBy = null, bool isAscending = true);
        Task<BloodBank> AddBloodBankAsync(BloodBank bloodBank);
        Task<bool> UpdateBloodBankAsync(BloodBank bloodBank);
        Task<bool> DeleteBloodBankAsync(Guid id);
        bool BloodBankExists(Guid id);
    }
}
