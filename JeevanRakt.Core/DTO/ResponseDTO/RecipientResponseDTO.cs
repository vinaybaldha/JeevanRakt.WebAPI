using JeevanRakt.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Core.DTO.ResponseDTO
{
    public class RecipientResponseDTO
    {
        public Guid RecipientId { get; set; }
        public string RecipientName { get; set; }
        public string RecipientBloodType { get; set; }
        public string RecipientContactNumber { get; set; }
     
    }
}
