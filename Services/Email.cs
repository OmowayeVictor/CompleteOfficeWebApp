using System.Net;
using System.Net.Mail;

namespace CompleteOfficeApplication.Services
{
    public class Email : IEmail
    {
        private readonly IConfiguration configuration;

        public Email(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public async Task SendAsync(string to, string subject, string body)
        {
            var message = new MailMessage(configuration["SMTP:Email"] ?? "vhicktarh@gmail.com", to)
            {
                Subject = subject,
                Body = body,
            };
            using (var smtpClient = new SmtpClient(configuration["SMTP:Host"], configuration.GetValue<int>("SMTP:Port")))
            {
                smtpClient.Credentials = new NetworkCredential(configuration["SMTP:Username"], configuration["SMTP:Password"]);
                await smtpClient.SendMailAsync(message);
            }
        }
    }
}
