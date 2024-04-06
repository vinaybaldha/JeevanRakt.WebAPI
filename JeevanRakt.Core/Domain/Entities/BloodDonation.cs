using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Core.Domain.Entities
{
    public class BloodDonation
    {
        [Key]
        public Guid DonationId { get; set; }
        public Guid DonorId { get; set; }  // Foreign key to Donor
        public Donor Donor { get; set; }  // Navigation property to Donor

        public DateTime DonationDate { get; set; }
        public string DonationBloodType { get; set; }
        public int DonationQuantityInMl { get; set; }
        public bool DonationIsTested { get; set; }
        public Guid InventoryId { get; set; }
        public BloodInventory BloodInventory { get; set; }
    }
}
