using JeevanRakt.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Core.DTO
{
    public class bloodbankResponse
    {
        public int Pages { get; set; }
        public List<BloodBank> BloodBanks { get; set; }
    }
}
