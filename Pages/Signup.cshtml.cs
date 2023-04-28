using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using SessionFinalProject;
using System.Net.Mail;

namespace SessionFinalProject.Pages
{
    public class SignupModel : PageModel
    {
        private readonly UserContext userContext;
        public bool IsLoggedIn { get; set; }
        public bool IsAdmin { get; set; }
        public SignupModel(UserContext userContext)
        {
            this.userContext = userContext;
        }
        public void OnGet(string message)
        {
            Message = message;
        }
        public string Message { get; set; }
        public async Task<IActionResult> OnPost(string email)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 2525;
            client.Host = "localhost";
            if (userContext.Users.Any(u => u.Email == email))
            {
                var message = new MailMessage();
                message.From = new MailAddress("no-reply@localhost");
                message.To.Add(new MailAddress(email));
                message.Subject = "Sign up";
                message.Body = $"Someone tried to signup with this email. Was this you?";
                client.Send(message);
            }
            else
            {
                var link = new SignupCode
                {
                    Email = email,
                    ExpiresOn = DateTime.Now.AddHours(1),
                    Code = Guid.NewGuid().ToString()
                };
                userContext.SignupCodes.Add(link);
                await userContext.SaveChangesAsync();
                var message = new MailMessage();
                message.From = new MailAddress("no-reply@localhost");
                message.To.Add(new MailAddress(email));
                message.Subject = "Sign up";
                message.Body = $"Click this <a href= 'https://localhost:7220/createaccount?code={link.Code}'> Signup Link </a> to complete the signup process";
                message.IsBodyHtml = true;
                client.Send(message);
            }
            return RedirectToPage("/signup", new { message = "Check your email for a link to create an account." });
        }
    }
}