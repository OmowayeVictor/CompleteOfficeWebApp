using CompleteOfficeApplication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompleteOfficeApplication.Pages.Account
{
    public class EditUserModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly IHelpers helpers;

        public EditUserModel(UserManager<User> userManager, IHelpers helpers)
        {
            this.userManager = userManager;
            this.helpers = helpers;
        }
        [BindProperty]
        public EditModel EditUser { get; set; } = new EditModel();
        public List<SelectListItem>? PositionOptions { get; set; }
        public List<SelectListItem>? DepartmentOptions { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            PositionOptions = helpers.LoadPositions();
            DepartmentOptions = helpers.LoadDepartments();
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            EditUser = new EditModel
            {
                Email = user.Email,
                Department = user.Department ?? "",
                Position = user.Position
            };

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            PositionOptions = helpers.LoadPositions();
            DepartmentOptions = helpers.LoadDepartments();
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.Department = EditUser.Department ?? user.Department;
            user.Position = EditUser.Position ?? user.Position;

            await userManager.UpdateAsync(user);
            return RedirectToPage("/Account/AdminCRUD");
        }
    }
}
