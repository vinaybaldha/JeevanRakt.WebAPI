using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Core.Domain.Entities
{
    public class BloodRequest
    {
        [Key]
        public Guid RequestId { get; set; }
        public Guid RecipientId { get; set; }  // Foreign key to Recipient
        public Recipient Recipient { get; set; }  // Navigation property
        public string RequestBloodType { get; set; }
        public int RequestQuantityInMl { get; set; }
        public DateTime RequestDate { get; set; }
        public bool RequestIsFulfilled { get; set; }
    }
}
