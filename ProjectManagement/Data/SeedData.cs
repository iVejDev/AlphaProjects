using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Check if the test user already exists
            if (await userManager.FindByEmailAsync("test@example.com") == null)
            {
                // Create the test user
                var user = new IdentityUser
                {
                    UserName = "test@example.com",
                    Email = "test@example.com",
                    EmailConfirmed = true
                };

                // Add the user with password "Test123!"
                var result = await userManager.CreateAsync(user, "Test123!");

                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create seed user: " +
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}