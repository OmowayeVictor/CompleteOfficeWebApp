using CompleteOfficeApplication.Pages.Account;
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

        public SignInModel(UserManager<User> userManager ,SignInManager<User> signInManager, ILogger<RegisterModel> logger)
        {
            this.userManager = userManager;
            this.signInManger = signInManager;
            this.logger = logger;
        }
        [BindProperty]
        public Login Login { get; set; } = new Login();
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
                    return RedirectToPage("/Account/LoginMFA", new
                    {
                        Login.Email,
                        Login.RememberMe
                    });
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
            return RedirectToPage(await RedirectToDepartment(Login.Email));
        }

        public async Task<string> RedirectToDepartment(string userEmail)
        {
            User? user = await userManager.FindByEmailAsync(userEmail);
            var claims = await userManager.GetClaimsAsync(user!);
            var department = claims.FirstOrDefault(c => c.Type == "Department")?.Value;

            return $"/Departments/{department}/Index";

        }
    }
}
