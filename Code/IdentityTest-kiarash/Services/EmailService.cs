using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace IdentityTest.Services
{
    public class EmailService
    {
        public Task Execute(string UserEmail,string Body,string Subject)
        {
            //https://myaccount.google.com/lesssecureapps
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 1000000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("kiarashkianian81@gmail.com","kiata2829");
            MailMessage message = new MailMessage("kiarashkianian81@gmail.com", UserEmail,Subject,Body);
            message.IsBodyHtml = true;
            message.BodyEncoding = UTF8Encoding.UTF8;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            client.Send(message);
            return Task.CompletedTask;

        }
    }
}
