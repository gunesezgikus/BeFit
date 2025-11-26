using Microsoft.AspNetCore.Identity;

namespace BeFit.Data
{
    public static class RoleInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roleNames = { "Admin" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminUser = new IdentityUser
            {
                UserName = "admin@befit.com",
                Email = "admin@befit.com",
                EmailConfirmed = true
            };

            string adminPassword = "Admin123!";
            var user = await userManager.FindByEmailAsync(adminUser.Email);

            if (user == null)
            {
                var createPowerUser = await userManager.CreateAsync(adminUser, adminPassword);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            var testUser = new IdentityUser
            {
                UserName = "abc@gmail.com",
                Email = "abc@gmail.com",
                EmailConfirmed = true
            };

            string testUserPassword = "Abc123!";
            var existingTestUser = await userManager.FindByEmailAsync(testUser.Email);

            if (existingTestUser == null)
            {
                await userManager.CreateAsync(testUser, testUserPassword);
            }
        }
    }
}
