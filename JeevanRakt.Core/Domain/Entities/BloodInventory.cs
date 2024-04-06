﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JeevanRakt.Core.Domain.Entities
{
    public class BloodInventory
    {
        [Key]
        public Guid InventoryId { get; set; }
        public string InventoryBloodType { get; set; }
        public int InventoryQuantityInMl { get; set; }
        public DateTime InventoryExpiryDate { get; set; }
        public Guid DonorId { get; set; }
        public Donor? Donor { get; set;}
    }
}
