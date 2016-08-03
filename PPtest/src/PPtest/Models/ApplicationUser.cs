using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace PPtest.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        //async Task<ClaimsIdentity> GenerateUserIdentityAsync(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> manager)
        //{
        //    //var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    var userIdentity = await manager.CreateAsync(this);
        //    return userIdentity;
        //}

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var authenticationType = "ApplicationCookie";
            var userIdentity = new ClaimsIdentity(await manager.GetClaimsAsync(this), authenticationType);
            return userIdentity;
        }

        string Address { get; set; }
        string City { get; set; }
        string State { get; set; }
    }
}
