using Eticaret.Core.Entities;
using System.Net;
using System.Net.Mail;

namespace Eticaret.WebUI.Utils
{
    public class MailHelper
    {
        public static async Task SendMailAsync(Contact contact)
        {
            SmtpClient smtpClient = new SmtpClient("mail.siteadı.com", 587);
            smtpClient.Credentials = new NetworkCredential("info@siteadı.com", "mailşifre");
            smtpClient.EnableSsl = false;
            MailMessage message = new MailMessage();
            message.From = new MailAddress("info@siteadı.com");
            message.To.Add("bilgi@siteadı.com");
            message.Subject = "siteden mesaj geldi";
            message.Body = $"İsim {contact.Name} - Soyisim {contact.SurName} - Email{contact.Email} - Telefon {contact.Phone} - Mesaj {contact.Message}";
            message.IsBodyHtml = true;
            await smtpClient.SendMailAsync(message);
            smtpClient.Dispose();
        }
    }
}
