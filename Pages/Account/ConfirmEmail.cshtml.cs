using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CompleteOfficeApplication.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<User> userManager;
        [BindProperty]
        public string Message { get; set; } = string.Empty;
        public ConfirmEmailModel(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        
        public async Task<IActionResult> OnGet(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null) 
            {
                this.Message = "Failed to validate Email";
                return Page();
            }
            var response = await userManager.ConfirmEmailAsync(user, token);
            if (!response.Succeeded)
            {
                this.Message = "Faild to Validate Email";
                return Page();
            }
            return RedirectToPage("/Account/Login");
        }
    }
}
