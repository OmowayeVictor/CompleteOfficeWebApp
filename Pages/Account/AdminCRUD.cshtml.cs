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
                    UserPosition ==Position.Manager.ToString()||
                    UserPosition == Position.SeniorManager.ToString() ||
                    UserPosition == Position.Director.ToString() ||
                    UserPosition == Position.SuperAdmin.ToString();
        public async Task<IActionResult> OnGetAsync()
        {
            UserPosition = User.FindFirst("Position")?.Value!;
            Department = User.FindFirst("Department")?.Value;
            if (!IsAuthorized)
            {
                return Page();
            };
            if (UserPosition == "SuperAdmin")
            {
                UsersList = await userManager.Users.ToListAsync();
            }
            else
            {
                UsersList = await userManager.Users
                   .Where(u => u.Department == Department).ToListAsync();
            }

            return Page();

        }
    }
}
