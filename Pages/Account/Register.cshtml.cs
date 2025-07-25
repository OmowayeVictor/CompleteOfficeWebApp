using CompleteOfficeApplication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace CompleteOfficeApplication.Pages.Account
{
    public class RegisterModel(UserManager<User> userManager, IEmail email, ILogger<RegisterModel> logger) : PageModel
    {
        private readonly UserManager<User> userManager = userManager;
        private readonly IEmail email = email;
        private readonly ILogger<RegisterModel> logger = logger;

        [BindProperty]
        public Registration Registration { get; set; } = new Registration();
        public List<SelectListItem>? PositionOptions { get; set; }
        public List<SelectListItem>? DepartmentOptions { get; set; }

        public void OnGet()
        {
            LoadPositions();
            LoadDepartments();
        }
        public async Task<IActionResult> OnPost()
        {
            LoadPositions();
            LoadDepartments();

            if (!ModelState.IsValid) {
                return Page();
        }
            var user = new User
            {
                UserName = Registration.Email,
                Email = Registration.Email,
            };
            var department = new Claim("Department", Registration.Department);
            var position = new Claim("Position", Registration.Position.ToString()!);

            var response = await userManager.CreateAsync(user, Registration.Password);

            if(!response.Succeeded)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }
                return Page();
            }
            var claims = new List<Claim>
            {
                new Claim("Department", Registration.Department),
                new Claim("Position", Registration.Position.ToString()!)
            };

            var DepartmentClaims = await this.userManager.AddClaimsAsync(user, claims);

            var emailConfirmation = await userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmationLink = Url.PageLink(pageName: "/Account/ConfirmEmail", values: new { userId = user.Id, token = emailConfirmation });
            var subject = "Confirm your Email";
            var body = $"Please confirm your email by clicking <a href='{confirmationLink}'>here</a>";
            await email.SendAsync(Registration.Email, subject, body);

            return RedirectToPage("/Account/Login");


        }

        private void LoadPositions()
        {
            PositionOptions = Enum.GetValues(typeof(Position))
                .Cast<Position>()
                .Select(p => new SelectListItem
                {
                    Text = p.ToString(),
                    Value = p.ToString()
                })
                .ToList();
        }

        private void LoadDepartments()
        {
            DepartmentOptions = Enum.GetValues(typeof(Department))
                .Cast<Department>()
                .Select(d => new SelectListItem
                {
                    Text = d.ToString(),
                    Value = d.ToString()
                })
                .ToList();
        }
    }
}

   
