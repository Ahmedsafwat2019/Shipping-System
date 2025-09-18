using DAL;
using DAL.UserModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ui.Services
{
    public class ContextConfig
    {
        private static readonly string seedAdminEmail = "admin@gmail.com";
        private static readonly string seedReviewerEmail = "Reviewer@gmail.com";
        private static readonly string seedOperationEmail = "Operation@gmail.com";
        private static readonly string seedOperationManagerEmail = "OperationManager@gmail.com";

        public static async Task SeedDataAsync(ShippingContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await SeedRolesAsync(roleManager);
            await SeedUsersAsync(userManager);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Admin", "User", "Reviewer", "Operation", "OperationManager" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(role));
                    if (!result.Succeeded)
                    {
                        Console.WriteLine($"Failed to create role '{role}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }
        }

        private static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            var users = new[]
            {
                new { Email = seedAdminEmail, Role = "Admin", UserName = seedAdminEmail, FirstName = "admin", LastName = "admin", Password = "admin123456" },
                new { Email = seedReviewerEmail, Role = "Reviewer", UserName = "ReviewerUserName", FirstName = "Reviewer", LastName = "Name", Password = "Reviewer123456" },
                new { Email = seedOperationEmail, Role = "Operation", UserName = "OperationUserName", FirstName = "Operation", LastName = "Name", Password = "Operation123456" },
                new { Email = seedOperationManagerEmail, Role = "OperationManager", UserName = "OperationManagerUserName", FirstName = "OperationManager", LastName = "Name", Password = "OperationManager123456" }
            };

            foreach (var u in users)
            {
                var user = await userManager.FindByEmailAsync(u.Email);
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = u.UserName,
                        Email = u.Email,
                        EmailConfirmed = true,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Phone = "65656566"
                    };

                    var result = await userManager.CreateAsync(user, u.Password);
                    if (!result.Succeeded)
                    {
                        Console.WriteLine($"Failed to create user '{u.Email}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                        continue;
                    }
                }

                if (!await userManager.IsInRoleAsync(user, u.Role))
                {
                    var roleResult = await userManager.AddToRoleAsync(user, u.Role);
                    if (!roleResult.Succeeded)
                    {
                        Console.WriteLine($"Failed to assign role '{u.Role}' to user '{u.Email}': {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                    }
                }
            }
        }
    }
}
