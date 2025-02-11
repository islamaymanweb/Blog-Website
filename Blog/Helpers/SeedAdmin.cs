using Microsoft.AspNetCore.Identity;

namespace csharpBlog.Helpers
{
    public static class SeedAdmin
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider, string adminEmail, string adminPassword)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Ensure the admin role exists
            var adminRole = "Admin";
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            // Check if an admin user already exists
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }
            }
            else
            {
                // Ensure the user is in the admin role if they already exist
                if (!await userManager.IsInRoleAsync(adminUser, adminRole))
                {
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }
            }
        }
    }
}
