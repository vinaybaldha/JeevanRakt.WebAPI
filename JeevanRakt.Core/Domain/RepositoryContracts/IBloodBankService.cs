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
    }
}
