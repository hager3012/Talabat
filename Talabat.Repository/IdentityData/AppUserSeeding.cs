using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.IdentityData
{
    public static class AppUserSeeding
    {
        public static async Task SeedUser(UserManager<AppUser> _userManager)
        {
            if(_userManager.Users.Count() == 0)
            {
                var user = new AppUser() 
                {
                    Name = "Hager",
                    Email = "hagershaaban7@gmail.com",
                    PhoneNumber = "01065695783",
                    UserName = "Hager30"
                };
                await _userManager.CreateAsync(user, "P@ssw0rd");
            }
        }
    }
}
