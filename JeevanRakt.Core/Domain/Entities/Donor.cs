using JeevanRakt.Core.Domain.Identity;
using JeevanRakt.Core.Domain.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JeevanRakt.Core.Domain.Entities
{
    public class Donor:ISoftDelete
    {
       
        public Guid DonorId { get; set; }
        public string DonorName { get; set; }
        public int DonorAge { get; set; }
        public string DonorGender { get; set; }
        public string DonorAddress { get; set; }
        public string DonorBloodType { get; set; }
        public string DonorContactNumber { get; set; }
        public char RecStatus { get; set; } = 'A';

        public Guid BloodBankId { get; set; }
        [JsonIgnore]
        public BloodBank? BloodBank { get; set; }
        public Guid UserId { get; set; }
        [JsonIgnore]
        public ApplicationUser? User { get; set; }
    }
}
