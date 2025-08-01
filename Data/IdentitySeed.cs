using CompleteOfficeApplication.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CompleteOfficeApplication.Data
{
    public class IdentitySeed
    {
        public readonly IEmail email;

        public IdentitySeed(IEmail email)
        {
            this.email = email;
        }

        public static async Task SeedAdmin(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var existingUser = await userManager.FindByEmailAsync(configuration["Admin:Email"]!);
            if (existingUser == null)
            {
                var user = new User
                {
                    UserName = configuration["Admin:Email"]!,
                    Email = configuration["Admin:Email"]!,
                    Position = Position.SuperAdmin,
                    Department = "IT"
                };
                var department = new Claim("Department","IT");
                var position = new Claim("Position", Position.SuperAdmin.ToString());

                var claims = new List<Claim>
                {
                   department,
                   position
                };

               
                var response = await userManager.CreateAsync(user, configuration["Admin:Password"]!);
                var createClaims = await userManager.AddClaimsAsync(user, claims);
                var emailConfirmation = await userManager.GenerateEmailConfirmationTokenAsync(user);

                await userManager.ConfirmEmailAsync(user, emailConfirmation);
                if (!response.Succeeded)
                {
                    foreach (var error in response.Errors)
                    {
                        Console.WriteLine($"Error creating super admin: {error.Description}");
                    }
                }
            }
        }
    }
}
