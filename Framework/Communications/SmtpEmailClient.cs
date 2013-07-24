using System;
using System.Net;
using System.Net.Mail;

namespace LinkMe.Framework.Communications
{
    public class SmtpEmailClient
        : IEmailClient
    {
        private readonly string _server;

        public SmtpEmailClient(string server)
        {
            if (string.IsNullOrEmpty(server))
                throw new ArgumentException(server);
            _server = server;
        }

        public int? Port { get; set; }
        public int? Timeout { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        void IEmailClient.Send(MailMessage message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            try
            {
                var client = Port == null ? new SmtpClient(_server) : new SmtpClient(_server, Port.Value);

                if (Timeout != null)
                    client.Timeout = Timeout.Value;

                if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(UserName, Password);
                }

                client.Send(message);
            }
            catch (Exception ex)
            {
                // Something bad has happened and no re-attempt will fix it so simply throw.

                throw new ApplicationException("Unable to send mail using SMTP server '" + _server + "'.", ex);
            }
        }
    }
}