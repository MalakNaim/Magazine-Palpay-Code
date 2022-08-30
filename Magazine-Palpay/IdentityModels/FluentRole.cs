using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Magazine_Palpay.Web.IdentityModels
{
    public class FluentRole : IdentityRole
    {
        public string DisplayName { get; set; }

        public string Description { get; set; }

        public int SystemInfoId { get; set; }

        public virtual ICollection<FluentRoleClaim> RoleClaims { get; set; } 

        public FluentRole()
            : base()
        {
            RoleClaims = new HashSet<FluentRoleClaim>();
        }

        public FluentRole(string roleName, string roleDescription = null, string displayName = null, int systemInfoId = default)
            : base(roleName)
        {
            RoleClaims = new HashSet<FluentRoleClaim>();
            Description = roleDescription;
            SystemInfoId = systemInfoId;
            DisplayName = displayName;
        }
    }
}