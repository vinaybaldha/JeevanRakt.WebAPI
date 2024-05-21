using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Core.Domain.Identity;
using JeevanRakt.Core.Domain.RepositoryContracts;
using JeevanRakt.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Core.Services
{
    public class RecipientRepository:IRecipientRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DataGeneraterService _dataGeneraterService;

        public RecipientRepository(ApplicationDbContext context, DataGeneraterService dataGeneraterService)
        {
            _context = context;
            _dataGeneraterService = dataGeneraterService;
        }

        public async Task<IEnumerable<Recipient>> GetRecipientsAsync(int page = 1, int pageSize = 10, string filterOn = null, string filterQuery = null, string sortBy = null, bool isAccending = true)
        {
            if (_context.Recipients == null)
            {
                return null;
            }

            //filtering
            var Recipients = _context.Recipients.Where(x => x.RecStatus == 'A' && x.PaymentStatus == "paid").AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("RecipientName", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientName.Contains(filterQuery));
                }

                else if (filterOn.Equals("RecipientAge", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientAge.Equals(filterQuery));
                }

                else if (filterOn.Equals("RecipientGender", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientGender.Contains(filterQuery));
                }

                else if (filterOn.Equals("RecipientAddress", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientAddress.Contains(filterQuery));
                }

                else if (filterOn.Equals("RecipientBloodType", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientBloodType.Contains(filterQuery));
                }

                else if (filterOn.Equals("RecipientContactNumber", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientContactNumber.Contains(filterQuery));
                }

            }

            //sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("RecipientName", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientName) : Recipients.OrderByDescending(x => x.RecipientName);
                }

                if (sortBy.Equals("RecipientAge", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientAge) : Recipients.OrderByDescending(x => x.RecipientAge);
                }

                if (sortBy.Equals("RecipientGender", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientGender) : Recipients.OrderByDescending(x => x.RecipientGender);
                }

                if (sortBy.Equals("RecipientAddress", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientAddress) : Recipients.OrderByDescending(x => x.RecipientAddress);
                }

                if (sortBy.Equals("RecipientBloodType", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientBloodType) : Recipients.OrderByDescending(x => x.RecipientBloodType);
                }

                if (sortBy.Equals("RecipientContactNumber", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientContactNumber) : Recipients.OrderByDescending(x => x.RecipientContactNumber);
                }

            }

            //pagination

            return await Recipients.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Recipient> GetRecipientAsync(Guid id)
        {
            if (_context.Recipients == null)
            {
                return null;
            }
            var recipient = await _context.Recipients.FindAsync(id);

            if (recipient == null)
            {
                return null;
            }

            return recipient;
        }

        public async Task<Recipient> AddRecipientAsync(Recipient recipient)
        {
            if (_context.Recipients == null)
            {
                return null;
            }
            recipient.PaymentStatus = "pending";
            _context.Recipients.Add(recipient);
            await _context.SaveChangesAsync();

            return recipient;
        }

        public async Task<bool> UpdateRecipientAsync(Guid id, Recipient recipient)
        {
            if (id != recipient.RecipientId)
            {
                return false;
            }

            _context.Entry(recipient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipientExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> DeleteRecipientAsync(Guid id)
        {
            if (_context.Recipients == null)
            {
                return false;
            }
            var recipient = await _context.Recipients.FindAsync(id);
            if (recipient == null)
            {
                return false;
            }

            _context.Recipients.Remove(recipient);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool RecipientExists(Guid id)
        {
            return (_context.Recipients?.Any(e => e.RecipientId == id)).GetValueOrDefault();
        }

        public async Task<IEnumerable<Recipient>> GetRecipientsByBloodBankIdAsync(Guid bloodbankId, int page = 1, int pageSize = 10, string filterOn = null, string filterQuery = null, string sortBy = null, bool isAccending = true)
        {
            if (_context.Recipients == null)
            {
                return null;
            }

            //filtering
            var Recipients = _context.Recipients.Where(x => x.BloodBankId == bloodbankId && x.RecStatus == 'A' && x.PaymentStatus == "paid").AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("RecipientName", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientName.Contains(filterQuery));
                }

                else if (filterOn.Equals("RecipientAge", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientAge.Equals(filterQuery));
                }

                else if (filterOn.Equals("RecipientGender", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientGender.Contains(filterQuery));
                }

                else if (filterOn.Equals("RecipientAddress", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientAddress.Contains(filterQuery));
                }

                else if (filterOn.Equals("RecipientBloodType", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientBloodType.Contains(filterQuery));
                }

                else if (filterOn.Equals("RecipientContactNumber", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = Recipients.Where(x => x.RecipientContactNumber.Contains(filterQuery));
                }

            }

            //sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("RecipientName", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientName) : Recipients.OrderByDescending(x => x.RecipientName);
                }

                if (sortBy.Equals("RecipientAge", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientAge) : Recipients.OrderByDescending(x => x.RecipientAge);
                }

                if (sortBy.Equals("RecipientGender", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientGender) : Recipients.OrderByDescending(x => x.RecipientGender);
                }

                if (sortBy.Equals("RecipientAddress", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientAddress) : Recipients.OrderByDescending(x => x.RecipientAddress);
                }

                if (sortBy.Equals("RecipientBloodType", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientBloodType) : Recipients.OrderByDescending(x => x.RecipientBloodType);
                }

                if (sortBy.Equals("RecipientContactNumber", StringComparison.OrdinalIgnoreCase))
                {
                    Recipients = isAccending ? Recipients.OrderBy(x => x.RecipientContactNumber) : Recipients.OrderByDescending(x => x.RecipientContactNumber);
                }
            }

            //pagination

            return await Recipients.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task GenerateTestDataAsync()
        {
            List<BloodBank> bloodBanks = _context.BloodBanks.ToList();

            foreach (var bloodBank in bloodBanks)
            {
                for (int i = 0; i < 100; i++)
                {
                    Recipient recipient = _dataGeneraterService.GenerateRecipient();

                    recipient.BloodBankId = bloodBank.BloodBankId;
                    _context.Recipients.Add(recipient);
                }
            }
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Recipient>> GetUnpaidRequest(ApplicationUser user)
        {
            return await _context.Recipients.Where(x => x.RecStatus == 'A' && x.PaymentStatus == "pending" && x.UserId == user.Id).ToListAsync();
        }
    }
}
