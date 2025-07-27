namespace CompleteOfficeApplication.Services
{
    public interface IHelpers
    {
        public string ContactUsHTMLBody(string name, string email, string subject, string message);

        public Task<string> RedirectToDepartment(string userEmail);
    }
}