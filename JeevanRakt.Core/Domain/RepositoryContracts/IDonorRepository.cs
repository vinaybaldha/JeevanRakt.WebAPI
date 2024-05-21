using JeevanRakt.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Core.Domain.RepositoryContracts
{
    public interface IDonorRepository
    {
        Task<IEnumerable<Donor>> GetDonorsAsync(int page = 1, int pageSize = 10, string filterOn = null, string filterQuery = null, string sortBy = null, bool isAscending = true);
        Task<Donor> GetDonorAsync(Guid id);
        Task<Donor> AddDonorAsync(Donor donor);
        Task<bool> UpdateDonorAsync(Donor donor);
        Task<bool> DeleteDonorAsync(Guid id);
        Task<int> GetTotalDonorsCountAsync();
        Task<IEnumerable<Donor>> GetDonorsByBloodBankIdAsync(Guid bloodbankId, int page = 1, int pageSize = 10, string filterOn = null, string filterQuery = null, string sortBy = null, bool isAscending = true);
        Task GenerateTestDataAsync();
        Task<int> GetTotalDonor(Guid bloodbankId);
    }
}
