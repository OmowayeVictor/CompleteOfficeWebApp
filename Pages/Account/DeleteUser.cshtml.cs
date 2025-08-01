using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CompleteOfficeApplication.Pages.Account
{
    public class DeleteUserModel(UserManager<User> userManager) : PageModel
    {
        private readonly UserManager<User> userManager = userManager;

        public string? Id { get; set; }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await userManager.DeleteAsync(user);
            return RedirectToPage("/Account/AdminCRUD");
        }
    }
}
