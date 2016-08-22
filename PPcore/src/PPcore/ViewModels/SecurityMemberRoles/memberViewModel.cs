using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PPcore.ViewModels.SecurityMemberRoles
{
    public class memberViewModel
    {
        public Models.SecurityMemberRoles SecurityMemberRoles { get; set; }
        public int memberCount { get; set; }
    }
}
