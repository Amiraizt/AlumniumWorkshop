using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alumnium.Core
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; } = "";
        public string AccountType { get; set; } = Consts.GeneralAdminAccountType;
    }
}
