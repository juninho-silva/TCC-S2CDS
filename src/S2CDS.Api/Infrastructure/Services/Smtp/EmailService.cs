using S2CDS.Api.Configurations;
using System.Net.Mail;
using System.Net;

namespace S2CDS.Api.Infrastructure.Services.Smtp
{
    /// <summary>
    /// Email Service
    /// </summary>
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Sends the email asynchronous.
        /// </summary>
        /// <param name="toEmail">To email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The message.</param>
        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings").Get<SmtpSettings>();

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpSettings.SenderEmail, smtpSettings.SenderName),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(new MailAddress(toEmail));

            using var client = new SmtpClient(smtpSettings.Server, smtpSettings.Port);

#if DEBUG
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            client.EnableSsl = false;
#else
            client.Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password);
            client.EnableSsl = true;
#endif

            await client.SendMailAsync(mailMessage);
        }
    }
}