using System.Net.Mail;

namespace LinkMe.Framework.Communications
{
    public interface IEmailClient
    {
        void Send(MailMessage message);
    }
}