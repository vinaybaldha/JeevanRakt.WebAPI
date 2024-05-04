using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JeevanRakt.Core.Domain.Entities
{
    public class Recipient
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

    }
}
