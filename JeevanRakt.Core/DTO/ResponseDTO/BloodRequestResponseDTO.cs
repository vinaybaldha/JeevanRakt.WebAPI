using JeevanRakt.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Core.DTO.ResponseDTO
{
    public class BloodRequestResponseDTO
    {
        public Guid RequestId { get; set; }
        public Guid RecipientId { get; set; }  // Foreign key to Recipient
        public RecipientResponseDTO? RecipientResponseDTO { get; set; }  // Navigation property
        public string RequestBloodType { get; set; }
        public int RequestQuantityInMl { get; set; }
        public DateTime RequestDate { get; set; }
        public bool RequestIsFulfilled { get; set; }
    }
}
