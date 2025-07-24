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
    public class User : IdentityUser
    {
        public string? Email { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public Position Position { get; set; }
    }
    public class OfficeWebAppDbContext : IdentityDbContext<User>
    {
        public OfficeWebAppDbContext(DbContextOptions<OfficeWebAppDbContext> options) : base(options)
        {

        }
    }
}
