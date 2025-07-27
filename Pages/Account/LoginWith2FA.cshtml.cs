using CompleteOfficeApplication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CompleteOfficeApplication.Pages.Account
{
    public class LoginWith2FAModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ILogger<LoginWith2FAModel> logger;
        private readonly IHelpers helpers;

        public LoginWith2FAModel(UserManager<User> userManager, SignInManager<User> _signInManager, ILogger<LoginWith2FAModel> logger, IHelpers helpers)
        {
            this.userManager = userManager;
            this.signInManager = _signInManager;
            this.logger = logger;
            this.helpers = helpers;
        }
        [BindProperty]
        public LoginWith2FA LoginPage { get; set; } = new LoginWith2FA();
      
        public async Task<IActionResult>  OnGet()
        {
            var user = await signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new InvalidOperationException("Cannot load 2FA user.");
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var user = await signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found!");
                return Page();
            }
            var code = LoginPage.Code?.Replace(" ", string.Empty).Replace("-", string.Empty);
            var response = await signInManager.TwoFactorAuthenticatorSignInAsync(LoginPage.Code!, LoginPage.RemeberMe, false);
            if (!response.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Two Factor Authentication Failed!");
                return Page();
            }
            if (response.Succeeded)
            {
                logger.LogInformation("User with ID '{UserId}' logged in with 2fa.", user.Id);
                return RedirectToPage(await helpers.RedirectToDepartment(user.Email!));
            }
            else if (response.IsLockedOut)
            {
                logger.LogWarning("User account locked out.");
                return RedirectToPage("./Login");
            }
            else
            {
                logger.LogWarning("Invalid authenticator code entered.");
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return Page();
            }
            
        }
    }
}
