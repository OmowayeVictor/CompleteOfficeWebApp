using CompleteOfficeApplication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CompleteOfficeApplication.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly ILogger<ForgotPasswordModel> logger;
        private readonly IEmail email;

        public ForgotPasswordModel(UserManager<User> userManager, ILogger<ForgotPasswordModel> logger, IEmail email)
        {
            this.userManager = userManager;
            this.logger = logger;
            this.email = email;
        }

        [BindProperty]
        public ForgotPassword ForgotPassword { get; set; }
        public void OnGet(string email, string token)
        {
           
        }

        public async Task<IActionResult> OnPostAsync()
        {

            try
            {
                if (!ModelState.IsValid) return Page();

                var user = await userManager.FindByEmailAsync(ForgotPassword.Email);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "An unknown error occured!");
                    return Page();
                }

                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { token, email = ForgotPassword.Email },
                    protocol: Request.Scheme);
                await email.SendAsync(user.Email!, "Password Reset", $"Reset your password by clicking <a href='{callbackUrl}'>here</a>.");
                return RedirectToPage("./Login");
            }
            catch (Exception ex) 
            {
                logger.LogError("An unknown error occured!");
                ModelState.AddModelError(string.Empty, $"{ex.Message}, Please try again later !");
                return Page();
            }

        }
    }
}
