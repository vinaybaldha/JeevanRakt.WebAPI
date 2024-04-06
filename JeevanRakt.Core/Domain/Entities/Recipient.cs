using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Core.Domain.Entities
{
    public class Recipient
    {
        [Key]
        public Guid RecipientId { get; set; }
        public string RecipientName { get; set; }
        public string RecipientBloodType { get; set; }
        public string RecipientContactNumber { get; set; }
        public ICollection<BloodRequest>? BloodRequests { get; set; }

    }
}
