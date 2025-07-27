using CompleteOfficeApplication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CompleteOfficeApplication.Pages.Account
{
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly IHelpers helpers;

        public ChangePasswordModel(UserManager<User> userManager, IHelpers helpers)
        {
            this.userManager = userManager;
            this.helpers = helpers;
        }
        [BindProperty]
        public ChangePassword ChangePassword { get; set; } = new ChangePassword();
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync ()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var user = await userManager.GetUserAsync(User);

            var response = await userManager.ChangePasswordAsync(user!, ChangePassword.OldPassword, ChangePassword.Password);

            if (!response.Succeeded)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
            return RedirectToPage(await helpers.RedirectToDepartment(user!.Email!));

        }
    }
}
