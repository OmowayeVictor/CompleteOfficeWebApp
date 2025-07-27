using Microsoft.AspNetCore.Identity;

namespace CompleteOfficeApplication.Services
{
    public class Helpers : IHelpers
    {
        private readonly UserManager<User> userManager;

        public Helpers(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public string ContactUsHTMLBody(string name, string email, string subject, string message)
        {
            return @$"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <title>New Contact Message</title>
    <style>
        body {{
            font-family: 'Segoe UI', sans-serif;
            background-color: #f9f9f9;
            padding: 20px;
            color: #333;
        }}

        .container {{
            max-width: 600px;
            margin: auto;
            background: #fff;
            padding: 30px;
            border-radius: 8px;
            border: 1px solid #ddd;
        }}

        h2 {{
            color: #0a0e27;
            margin-bottom: 20px;
        }}

        .label {{
            font-weight: bold;
            color: #0a0e27;
            margin-top: 15px;
        }}

        .value {{
            margin-bottom: 10px;
        }}

        .footer {{
            margin-top: 30px;
            font-size: 0.9em;
            color: #888;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <h2>New Contact Message</h2>

        <div>
            <div class='label'>Name:</div>
            <div class='value'>{name}</div>

            <div class='label'>Email:</div>
            <div class='value'>{email}</div>

            <div class='label'>Subject:</div>
            <div class='value'>{subject}</div>

            <div class='label'>Message:</div>
            <div class='value'>{message}</div>
        </div>

        <div class=""footer"">
            Sent from CompleteOffice Contact Form.
        </div>
    </div>
</body>
</html>
";
        }

        public async Task<string> RedirectToDepartment(string userEmail)
        {
            User? user = await userManager.FindByEmailAsync(userEmail);
            var claims = await userManager.GetClaimsAsync(user!);
            var department = claims.FirstOrDefault(c => c.Type == "Department")?.Value;

            return $"/Departments/{department}/Index";
        }
    }
}
