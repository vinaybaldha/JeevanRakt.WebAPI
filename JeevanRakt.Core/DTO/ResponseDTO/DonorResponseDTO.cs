using JeevanRakt.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Core.DTO.ResponseDTO
{
    public class DonorResponseDTO
    {
        public Guid DonorId { get; set; }
        public string DonorName { get; set; }
        public string DonorBloodType { get; set; }
        public string DonorContactNumber { get; set; }
    }
}
