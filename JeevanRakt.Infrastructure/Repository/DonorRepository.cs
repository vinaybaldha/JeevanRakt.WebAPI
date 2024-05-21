using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Core.Domain.RepositoryContracts;
using JeevanRakt.Core.Services;
using JeevanRakt.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Infrastructure.Repository
{
    public class DonorRepository:IDonorRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DataGeneraterService _dataGeneraterService;

        public DonorRepository(ApplicationDbContext context, DataGeneraterService dataGeneraterService)
        {
            _context = context;
            _dataGeneraterService = dataGeneraterService;
        }

        public async Task<IEnumerable<Donor>> GetDonorsAsync(int page = 1, int pageSize = 10, string filterOn = null, string filterQuery = null, string sortBy = null, bool isAccending = true)
        {
            if (_context.Donors == null)
            {
                return null;
            }

            //filtering
            var Donors = _context.Donors.Where(x => x.RecStatus == 'A').AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("DonorName", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorName.Contains(filterQuery));
                }

                else if (filterOn.Equals("DonorAge", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorAge.Equals(filterQuery));
                }

                else if (filterOn.Equals("DonorGender", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorGender.Contains(filterQuery));
                }

                else if (filterOn.Equals("DonorAddress", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorAddress.Contains(filterQuery));
                }

                else if (filterOn.Equals("DonorBloodType", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorBloodType.Contains(filterQuery));
                }

                else if (filterOn.Equals("DonorContactNumber", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorContactNumber.Contains(filterQuery));
                }

            }

            //sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("DonorName", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorName) : Donors.OrderByDescending(x => x.DonorName);
                }

                if (sortBy.Equals("DonorAge", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorAge) : Donors.OrderByDescending(x => x.DonorAge);
                }

                if (sortBy.Equals("DonorGender", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorGender) : Donors.OrderByDescending(x => x.DonorGender);
                }

                if (sortBy.Equals("DonorAddress", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorAddress) : Donors.OrderByDescending(x => x.DonorAddress);
                }

                if (sortBy.Equals("DonorBloodType", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorBloodType) : Donors.OrderByDescending(x => x.DonorBloodType);
                }

                if (sortBy.Equals("DonorContactNumber", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorContactNumber) : Donors.OrderByDescending(x => x.DonorContactNumber);
                }

            }

            //pagination

            return await Donors.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Donor> GetDonorAsync(Guid id)
        {
            return await _context.Donors.FindAsync(id);
        }

        public async Task<Donor> AddDonorAsync(Donor donor)
        {
            _context.Donors.Add(donor);
            await _context.SaveChangesAsync();
            return donor;
        }

        public async Task<bool> UpdateDonorAsync(Donor donor)
        {
            _context.Entry(donor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonorExists(donor.DonorId))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteDonorAsync(Guid id)
        {
            var donor = await _context.Donors.FindAsync(id);
            if (donor == null)
            {
                return false;
            }

            _context.Donors.Remove(donor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetTotalDonorsCountAsync()
        {
            return await _context.Donors.CountAsync();
        }

        public async Task<IEnumerable<Donor>> GetDonorsByBloodBankIdAsync(Guid bloodbankId, int page = 1, int pageSize = 10, string filterOn = null, string filterQuery = null, string sortBy = null, bool isAccending = true)
        {

            if (_context.Donors == null)
            {
                return null;
            }

            //filtering
            var Donors = _context.Donors.Where(x => x.BloodBankId == bloodbankId && x.RecStatus == 'A').AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("DonorName", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorName.Contains(filterQuery));
                }

                else if (filterOn.Equals("DonorAge", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorAge.Equals(filterQuery));
                }

                else if (filterOn.Equals("DonorGender", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorGender.Contains(filterQuery));
                }

                else if (filterOn.Equals("DonorAddress", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorAddress.Contains(filterQuery));
                }

                else if (filterOn.Equals("DonorBloodType", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorBloodType.Contains(filterQuery));
                }

                else if (filterOn.Equals("DonorContactNumber", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = Donors.Where(x => x.DonorContactNumber.Contains(filterQuery));
                }

            }

            //sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("DonorName", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorName) : Donors.OrderByDescending(x => x.DonorName);
                }

                if (sortBy.Equals("DonorAge", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorAge) : Donors.OrderByDescending(x => x.DonorAge);
                }

                if (sortBy.Equals("DonorGender", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorGender) : Donors.OrderByDescending(x => x.DonorGender);
                }

                if (sortBy.Equals("DonorAddress", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorAddress) : Donors.OrderByDescending(x => x.DonorAddress);
                }

                if (sortBy.Equals("DonorBloodType", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorBloodType) : Donors.OrderByDescending(x => x.DonorBloodType);
                }

                if (sortBy.Equals("DonorContactNumber", StringComparison.OrdinalIgnoreCase))
                {
                    Donors = isAccending ? Donors.OrderBy(x => x.DonorContactNumber) : Donors.OrderByDescending(x => x.DonorContactNumber);
                }
            }

            //pagination

            return await Donors.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        }

        public async Task GenerateTestDataAsync()
        {
            List<BloodBank> bloodBanks = _context.BloodBanks.ToList();

            foreach (var bloodBank in bloodBanks)
            {
                for (int i = 0; i < 100; i++)
                {
                    Donor donor = _dataGeneraterService.GenerateDonor();

                    donor.BloodBankId = bloodBank.BloodBankId;
                    _context.Donors.Add(donor);
                }
            }
            await _context.SaveChangesAsync();            
        }

        private bool DonorExists(Guid id)
        {
            return _context.Donors.Any(e => e.DonorId == id);
        }

        public async Task<int> GetTotalDonor(Guid bloodbankId)
        {
            return await _context.Donors.Where(x=>x.BloodBankId == bloodbankId).CountAsync();
        }
    }
}
