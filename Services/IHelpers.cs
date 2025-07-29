using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompleteOfficeApplication.Services
{
    public interface IHelpers
    {
        string ContactUsHTMLBody(string name, string email, string subject, string message);
        List<SelectListItem>? LoadDepartments();
        List<SelectListItem>? LoadPositions();
        Task<string> RedirectToDepartment(string userEmail);
    }
}