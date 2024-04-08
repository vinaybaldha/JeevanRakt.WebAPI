using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Core.Domain.Entities
{
    public class Blood
    {
        [Key]
        public string BloodGroup { get; set; }
        public int BloodStock { get; set; }
    }
}
