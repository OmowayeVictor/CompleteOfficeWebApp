
namespace CompleteOfficeApplication.Services
{
    public interface IEmail
    {
        Task SendAsync(string to, string subject, string body);
    }
}