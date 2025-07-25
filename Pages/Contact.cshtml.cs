using CompleteOfficeApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CompleteOfficeApplication.Pages
{
    public class ContactModel : PageModel
    {
        private readonly IEmail email;
        private readonly IHelpers helpers;
        private readonly IConfiguration configuration;

        public ContactModel(IEmail email, IHelpers helpers, IConfiguration configuration)
        {
            this.email = email;
            this.helpers = helpers;
            this.configuration = configuration;
        }
        [BindProperty]
        public ContactUs ContactUs { get; set; } = new ContactUs();
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (ContactUs == null)
            {
                ModelState.AddModelError(string.Empty, "Please fill out the form correctly");
                return Page();
            }
            string htmlBody = helpers.ContactUsHTMLBody(ContactUs.Name, ContactUs.Email, ContactUs.Subject, ContactUs.Message);
            await email.SendAsync(configuration["ContactUs:Email"]!, ContactUs.Subject, htmlBody);

            return RedirectToPage("/Index");

        }
    }
}
