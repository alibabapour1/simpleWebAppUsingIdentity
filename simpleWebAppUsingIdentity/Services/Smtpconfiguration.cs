using System.Net;
using System.Net.Mail;
using System.Text;

namespace simpleWebAppUsingIdentity.Services
{
    public  class Smtpconfiguration
    {
        public  Task Excute(string UserMail , string body , string Subject)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.EnableSsl = true;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("mr.alibabapour@gmail.com", "robk uraz tyqf dssm ");
            smtpClient.Timeout = 1000000; 
            MailMessage mailMessage = new MailMessage("mr.alibabapour@gmail.com",UserMail,Subject,body);
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = UTF8Encoding.UTF8;
            mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            smtpClient.Send(mailMessage);
            return Task.CompletedTask;
        }

        
    }
}
