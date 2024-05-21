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
    public class Recipient:ISoftDelete
    {
        public Guid RecipientId { get; set; }
        public string RecipientName { get; set; }
        public int RecipientAge { get; set; }
        public string RecipientGender { get; set; }
        public string RecipientAddress { get; set; }
        public string RecipientBloodType { get; set; }
        public string RecipientContactNumber { get; set; }
        public Guid BloodBankId { get; set; }
        [JsonIgnore]
        public BloodBank? BloodBank { get; set; }
        public char RecStatus { get; set; } = 'A';
        public string PaymentStatus { get; set; } = "pending";
        public Guid UserId { get; set; }
        [JsonIgnore]
        public ApplicationUser? User { get; set; }

    }
}
