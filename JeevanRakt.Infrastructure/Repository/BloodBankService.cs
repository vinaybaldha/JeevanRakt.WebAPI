using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Core.Domain.RepositoryContracts;
using JeevanRakt.Core.DTO;
using JeevanRakt.Core.Helper;
using JeevanRakt.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;


namespace JeevanRakt.Infrastructure.Repository
{
    public class BloodBankService : IBloodBankService
    {
        private readonly ApplicationDbContext _dbContext;

        public BloodBankService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BloodBank FindNearestBloodBank(double userLatitude, double userLongitude)
        {
            var bloodBanks = _dbContext.BloodBanks.ToList(); // Retrieve all blood banks from the database

            BloodBank nearestBloodBank = null;
            double shortestDistance = double.MaxValue;

            foreach (var bloodBank in bloodBanks)
            {
                double distance = GeoHelper.CalculateDistance(userLatitude, userLongitude, bloodBank.Latitude, bloodBank.Longitude);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestBloodBank = bloodBank;
                }
            }

            return nearestBloodBank;
        }

        public async Task<bloodbankResponse> GetBloodBanksAsync(int page = 1, int pageSize = 10, string filterOn = null, string filterQuery = null, string sortBy = null, bool isAscending = true)
        {
            IQueryable<BloodBank> bloodBanksQuery = _dbContext.BloodBanks.Include(x=>x.Donors).Include(x=>x.Recipients).Include(x=>x.BloodInventory).Where(x => x.RecStatus == 'A');

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("BloodBankName", StringComparison.OrdinalIgnoreCase))
                {
                    bloodBanksQuery = bloodBanksQuery.Where(x => x.BloodBankName.Contains(filterQuery));
                }
                else if (filterOn.Equals("Address", StringComparison.OrdinalIgnoreCase))
                {
                    bloodBanksQuery = bloodBanksQuery.Where(x => x.Address.Contains(filterQuery));
                }
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("BloodBankName", StringComparison.OrdinalIgnoreCase))
                {
                    bloodBanksQuery = isAscending ? bloodBanksQuery.OrderBy(x => x.BloodBankName) : bloodBanksQuery.OrderByDescending(x => x.BloodBankName);
                }
                else if (sortBy.Equals("Address", StringComparison.OrdinalIgnoreCase))
                {
                    bloodBanksQuery = isAscending ? bloodBanksQuery.OrderBy(x => x.Address) : bloodBanksQuery.OrderByDescending(x => x.Address);
                }
            }

            var totalCount = bloodBanksQuery.Count();
            var totalPages = (int) Math.Ceiling((decimal)totalCount/pageSize);

            List<BloodBank> bloodBanks = await bloodBanksQuery.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            bloodbankResponse bloodbankResponse = new bloodbankResponse() { BloodBanks = bloodBanks, Pages = totalPages };

            return bloodbankResponse;
        }

        public async Task<BloodBank> AddBloodBankAsync(BloodBank bloodBank)
        {
            _dbContext.BloodBanks.Add(bloodBank);
            await _dbContext.SaveChangesAsync();
            return bloodBank;
        }

        public async Task<bool> UpdateBloodBankAsync(BloodBank bloodBank)
        {
            _dbContext.Entry(bloodBank).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteBloodBankAsync(Guid id)
        {
            var bloodBank = await _dbContext.BloodBanks.FindAsync(id);
            if (bloodBank == null)
            {
                return false;
            }

            _dbContext.BloodBanks.Remove(bloodBank);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public bool BloodBankExists(Guid id)
        {
            return _dbContext.BloodBanks.Any(e => e.BloodBankId == id);
        }

        public async Task<BloodBank> GetBloodBankAsync(Guid id)
        {
            return await _dbContext.BloodBanks
                .Include(x => x.Donors)
                .Include(x => x.Recipients)
                .Include(x => x.BloodInventory)
                .FirstOrDefaultAsync(x => x.BloodBankId == id);
        }

        public async Task<int> GetTotalBloodBankAsync()
        {
            return await _dbContext.BloodBanks.CountAsync();
        }

    }
}
