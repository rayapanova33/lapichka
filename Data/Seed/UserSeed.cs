using LapichkaShop.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace LapichkaShop.Data.Seed
{
    public static class UserSeed
    {
        public static void SeedAdmin(UserManager<ApplicationUser> userManager)
        {
            var firstName = "Admin";
            var lastName = "Account";
            var adminEmail = "admin@ubm.com";
            var password = "Password123!";
            var registrationDate = DateTime.UtcNow;
            var adminUser = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();

            if (adminUser == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = firstName,
                    LastName = lastName,
                    RegistrationDate = registrationDate,
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = userManager.CreateAsync(user, password).GetAwaiter().GetResult();

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, LapichkaShop.Data.Constants.UserRoles.Admin).GetAwaiter().GetResult();
            }
        }
    }
}
