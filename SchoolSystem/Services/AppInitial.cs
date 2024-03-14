using Microsoft.AspNetCore.Identity;

namespace SchoolSystem.Services
{
    public class AppInitial
    {

        public static async Task SeedAdminUser(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            var adminEmail = configuration["AdminCredentials:Email"];
            var adminPassword = configuration["AdminCredentials:Password"];

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail };
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}

