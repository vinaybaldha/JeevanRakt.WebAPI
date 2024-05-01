using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Core.Domain.Entities
{
    [Keyless]
    public class RoleAccess
    {
        public string? Role {  get; set; }
        public string? Menu { get; set; }
    }
}
