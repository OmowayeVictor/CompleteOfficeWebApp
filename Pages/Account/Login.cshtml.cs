using CompleteOfficeApplication.Pages.Account;
using CompleteOfficeApplication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System.Threading.Tasks;

namespace CompleteOfficeApplication.Pages.Auth
{
    public class SignInModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManger;
        private readonly ILogger<RegisterModel> logger;
        private readonly IHelpers helpers;

        public SignInModel(UserManager<User> userManager ,SignInManager<User> signInManager, ILogger<RegisterModel> logger,IHelpers helpers)
        {
            this.userManager = userManager;
            this.signInManger = signInManager;
            this.logger = logger;
            this.helpers = helpers;
        }
        [BindProperty]
        public LoginModel Login { get; set; } = new LoginModel();
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await this.signInManger.PasswordSignInAsync( Login.Email, Login.Password, Login.RememberMe, false );
            if (!response.Succeeded)
            {
                if (response.RequiresTwoFactor)
                {
                    return RedirectToPage("/Account/LoginWith2FA");
                }
                if (response.IsLockedOut)
                {
                    ModelState.AddModelError("Login", "Account is locked out. Please try again later.");
                }
                else
                {
                    ModelState.AddModelError("Login", "Invalid login attempt. Please check your credentials.");
                }
                return Page();
            }
            return RedirectToPage(await helpers.RedirectToDepartment(Login.Email));
        }
    }
}
