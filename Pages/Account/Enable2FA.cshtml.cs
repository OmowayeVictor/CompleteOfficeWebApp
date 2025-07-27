using iTextSharp.text.pdf.qrcode;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
namespace CompleteOfficeApplication.Pages.Account
{
    public class Enabale2FAModel(UserManager<User> userManager) : PageModel
    {
        private readonly UserManager<User> userManager = userManager;
        [BindProperty]
        public Enable2FAModel Enable2FAModel { get; set; } = new Enable2FAModel();

        public string? QRCodeUri { get; set; }
        public string? Key { get; set; }
        public async Task OnGet(string email)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return;

            var key = await userManager.GetAuthenticatorKeyAsync(user);

            if (string.IsNullOrEmpty(key))
            {
                await userManager.ResetAuthenticatorKeyAsync(user);
                key = await userManager.GetAuthenticatorKeyAsync(user);
            }
            Key = key;
            QRCodeUri = GenerateQRCodeUri(user.Email!, key!);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User Not Found!");
            }
            var response = await userManager.VerifyTwoFactorTokenAsync(user!, userManager.Options.Tokens.AuthenticatorTokenProvider, Enable2FAModel.Code!);
            if (!response)
            {
                ModelState.AddModelError(string.Empty, "2FA Verfication Failed!");
                return Page();
            }
            await userManager.SetTwoFactorEnabledAsync(user!, true);
            TempData["Success"] = "Two-Factor Authentication has been enabaled!";
            return RedirectToPage("/Account/Login");

        }

        public string GenerateQRCodeUri(string email, string unformattedKey)
        {
            var uri = $"otpauth://totp/CompleteOffice:{email}?secret={unformattedKey}&issuer=CompleteOffice&digits=6";
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(uri, QRCodeGenerator.ECCLevel.Q);

            using var qrCode = new PngByteQRCode(qrCodeData);
            var qrCodeBytes = qrCode.GetGraphic(20);


            return $"data:image/png;base64,{Convert.ToBase64String(qrCodeBytes)}";
        }
    }
}
