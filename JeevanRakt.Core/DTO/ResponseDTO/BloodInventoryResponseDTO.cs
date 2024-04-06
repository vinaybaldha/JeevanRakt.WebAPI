using JeevanRakt.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Core.DTO.ResponseDTO
{
    public class BloodInventoryResponseDTO
    {
        public Guid InventoryId { get; set; }
        public string InventoryBloodType { get; set; }
        public int InventoryQuantityInMl { get; set; }
        public DateTime InventoryExpiryDate { get; set; }
        public Guid DonorId { get; set; }
        public DonorResponseDTO? DonorResponseDTO { get; set; }
    }
}
