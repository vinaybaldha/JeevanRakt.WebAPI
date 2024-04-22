using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Core.Domain.Entities
{
    public class BloodBank
    {
        public Guid BloodBankId { get; set; }

        public string BloodBankName { get; set; }

        public string Address { get; set; } // Address or description of the location

        public double Latitude { get; set; } // Latitude coordinate
        public double Longitude { get; set; } // Longitude coordinate

        
    }
}
