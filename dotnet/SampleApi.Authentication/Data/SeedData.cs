using Microsoft.AspNetCore.Identity;

namespace SampleApi.Authentication.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var roles = new[] { "Admin", "User", "WeatherUser" };

            // Create roles if they don't exist
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create default admin user
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            var adminEmail = configuration["DefaultAdminUser:Email"];
            var adminPassword = configuration["DefaultAdminUser:Password"];

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var user = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRolesAsync(user, roles);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error creating user: {error.Description}");
                    }
                }
            }
        }
    }
}
