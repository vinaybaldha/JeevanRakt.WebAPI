using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Core.DTO
{
    public class UserResponseDTO
    {
        public string? EmployeeName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public IList<string>? Role { get; set; }

    }
}
