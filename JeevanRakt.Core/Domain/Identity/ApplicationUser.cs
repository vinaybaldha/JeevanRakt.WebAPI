using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Core.Domain.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? EmployeeName { get; set; }

        public string? FilePath { get; set; }
    }
}
