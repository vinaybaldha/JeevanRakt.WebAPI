using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Core.Domain.RepositoryContracts;
using JeevanRakt.Core.Helper;
using JeevanRakt.Infrastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
