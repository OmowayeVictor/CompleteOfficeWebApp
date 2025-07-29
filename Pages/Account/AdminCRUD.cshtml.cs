using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CompleteOfficeApplication.Pages.Account
{
    public class AdminCRUDModel(UserManager<User> userManager) : PageModel
    {
        private readonly UserManager<User> userManager = userManager;

        [BindProperty]
        public string? UserPosition {  get; set; }
        [BindProperty]
        public string? Department {  get; set; }

        [BindProperty]
        public List<User>? UsersList { get; set; }
        public bool IsAuthorized =>
                    UserPosition == "Manager" ||
                    UserPosition == "Senior Manager" ||
                    UserPosition == "Director";
        public async Task<IActionResult> OnGetAsync()
        {
            UserPosition = User.FindFirst("Position")?.Value!;
            Department = User.FindFirst("Department")?.Value;
            if (!IsAuthorized)
            {
                return Page();
            };

            UsersList = await userManager.Users
                .Where(u => u.Department == Department).ToListAsync();

            return Page();

        }

        public async Task<IActionResult> OnDeleteAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null) { return NotFound(); }

            await userManager.DeleteAsync(user);
            return RedirectToPage("./AdminCRUD");
        }
    }
}
