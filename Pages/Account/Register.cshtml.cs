using CompleteOfficeApplication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace CompleteOfficeApplication.Pages.Account
{
    public class RegisterModel(UserManager<User> userManager, IEmail email) : PageModel
    {
        private readonly UserManager<User> userManager = userManager;
        private readonly IEmail email = email;

        [BindProperty]
        public Registration Registration { get; set; } = new Registration();
        public List<SelectListItem>? PositionOptions { get; set; }

        public void OnGet()
        {
            LoadPositions();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            LoadPositions();

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
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
            await userManager.AddClaimAsync(user, department);
            await userManager.AddClaimAsync(user, position);
            
            var emailConfirmation = await userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmationLink = Url.PageLink(pageName: "/Account/ConfirmEmail", values: new { userId = user.Id, token = emailConfirmation });
            var subject = "Confirm your Email";
            var body = $"Please confirm your email by clicking here: <a {confirmationLink}>here</a>";
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
    }
}

   
