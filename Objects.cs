using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace CompleteOfficeApplication
{
    public enum Position
    {
        Officer,
        Supervisor,
        Manager,
        SeniorManager,
        Director
    }
    public enum Department
    {
        IT,
        HR,
        Operations
    }
    public class Registration
    {
        [Required]
        [EmailAddress(ErrorMessage = "Inavlid Email Address")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;
        public Position? Position { get; set; } 
    }
    public class Login
    {
        [Required]
        [EmailAddress(ErrorMessage = "Inavlid Email Address")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; } = false;
    }

    public class User : IdentityUser
    {
        public string Department { get; set; } = string.Empty;
        public Position Position { get; set; }
    }
    public class OfficeWebAppDbContext : IdentityDbContext<User>
    {
        public OfficeWebAppDbContext(DbContextOptions<OfficeWebAppDbContext> options) : base(options)
        {

        }
    }

    public class ContactUs
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress(ErrorMessage = "Inavlid Email Address")]
        public string Email { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        [Required]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Message must be between 10 and 500 characters.")]
        public string Message { get; set; } = string.Empty;
    }
}
