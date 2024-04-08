﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Core.Domain.Entities
{
    public class Donor
    {
       
        public Guid DonorId { get; set; }
        public string DonorName { get; set; }
        public int DonorAge { get; set; }
        public string DonorGender { get; set; }
        public string DonorAddress { get; set; }
        public string DonorBloodType { get; set; }
        public string DonorContactNumber { get; set; }
    }
}
