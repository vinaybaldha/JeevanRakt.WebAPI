using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JeevanRakt.Core.Domain.Entities
{
    public class BloodInventory
    {
        public Guid BloodInventoryId { get; set; }
        public int A1 { get; set; }
        public int A2 { get; set; }
        public int B1 { get; set; }
        public int B2 { get; set; }
        public int AB1 { get; set; }
        public int AB2 { get; set; }
        public int O1 { get; set; }
        public int O2 { get; set; }
       
        public Guid BloodBankId { get; set; }
        [JsonIgnore]
        [ForeignKey("BloodBankId")]
        public BloodBank? BloodBank { get; set; }
    }
}
