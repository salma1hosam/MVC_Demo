using System.Net;
using System.Net.Mail;

namespace Demo.Presentation.Utilities
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);  //Creates an object from the client that you will send the email through it
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("salma.hossam2003@gmail.com", "ciksuflxkckibbsx");  //The Account on the Mail Server that will send the Emails (Sender)
            client.Send("salma.hossam2003@gmail.com", email.To , email.Subject , email.Body );

        }
    }
}
