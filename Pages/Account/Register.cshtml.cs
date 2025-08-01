using CompleteOfficeApplication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace CompleteOfficeApplication.Pages.Account
{
    public class RegisterModel(UserManager<User> userManager, IEmail email, ILogger<RegisterModel> logger, IHelpers helpers) : PageModel
    {
        private readonly UserManager<User> userManager = userManager;
        private readonly IEmail email = email;
        private readonly ILogger<RegisterModel> logger = logger;
        private readonly IHelpers helpers = helpers;

        [BindProperty]
        public Registration Registration { get; set; } = new Registration();
        public List<SelectListItem>? PositionOptions { get; set; }
        public List<SelectListItem>? DepartmentOptions { get; set; }

        public void OnGet()
        {
            PositionOptions = helpers.LoadPositions();
            DepartmentOptions = helpers.LoadDepartments();
        }
        public async Task<IActionResult> OnPost()
        {
            PositionOptions = helpers.LoadPositions();
            DepartmentOptions = helpers.LoadDepartments();

            if (!ModelState.IsValid) {
                return Page();
        }
            var user = new User
            {
                UserName = Registration.Email,
                Email = Registration.Email,
                Position = Registration.IsSuperAdmin ? Position.SuperAdmin : Position.Officer,
                Department = Registration.Department,
            };
            var department = new Claim("Department", Registration.Department);
            var position = Registration.IsSuperAdmin ? new Claim("Position", Position.SuperAdmin.ToString()) : new Claim("Position", Position.Officer.ToString());

            var response = await userManager.CreateAsync(user, Registration.Password);

            if(!response.Succeeded)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
            var claims = new List<Claim>
            {
               department,
               position,
            };

            var DepartmentClaims = await this.userManager.AddClaimsAsync(user, claims);

            var emailConfirmation = await userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmationLink = Url.PageLink(pageName: "/Account/ConfirmEmail", values: new { userId = user.Id, token = emailConfirmation });
            var subject = "Confirm your Email";
            var body = $"Please confirm your email by clicking <a href='{confirmationLink}'>here</a>";
            await email.SendAsync(Registration.Email, subject, body);

            return RedirectToPage("/Account/Login");


        }
    }
}

   
