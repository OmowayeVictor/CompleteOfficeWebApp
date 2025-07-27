using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CompleteOfficeApplication.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<User> userManager;

        public ResetPasswordModel(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        [BindProperty]
        public ResetPassword ResetPassword { get; set; } = new ResetPassword();
        public IActionResult OnGet(string email, string token)
        {
            ResetPassword = new ResetPassword
            {
                Email = email,
                Token = token
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await userManager.FindByEmailAsync(ResetPassword.Email);
            if (user == null)
            {
                return RedirectToPage("./ResetPasswordConfirmation"); ;
            }

            var response = await userManager.ResetPasswordAsync(user, ResetPassword.Token, ResetPassword.Password);
            if (!response.Succeeded)
            {
                foreach (var error in response.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return Page();
            }
            return RedirectToPage("./ResetPasswordConfirmation");

        }
    }
}
