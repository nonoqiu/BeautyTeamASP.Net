using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System;

namespace BeautyTeamWeb
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            MailMessage mailMsg = new MailMessage();
            mailMsg.To.Add(new MailAddress(message.Destination));
            mailMsg.From = new MailAddress(Secrets.EmailAddress,
                "Obisoft服务邮件");
            mailMsg.Subject = message.Subject;
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(
                message.Body, null, MediaTypeNames.Text.Html));
            SmtpClient smtpClient = new SmtpClient("smtpdm.aliyun.com", 25);
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(
                Secrets.EmailAddress,
                Secrets.EmailSmtpPassword);
            smtpClient.Credentials = credentials;
            await smtpClient.SendMailAsync(mailMsg);
        }
    }
    public class WeChatVerifyService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return null;
        }
    }
}