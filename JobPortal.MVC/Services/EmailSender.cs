using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

namespace JobPortal.MVC.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
                var smtpUsername = _configuration["EmailSettings:Username"];
                var smtpPassword = _configuration["EmailSettings:Password"];

                if (string.IsNullOrEmpty(smtpServer) || string.IsNullOrEmpty(smtpUsername) || string.IsNullOrEmpty(smtpPassword))
                {
                    throw new InvalidOperationException("SMTP yapılandırması eksik");
                }

                var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpServer"])
                {
                    Port = int.Parse(_configuration["EmailSettings:SmtpPort"]),
                    Credentials = new NetworkCredential(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]),
                    EnableSsl = true,  // SSL bağlantısının etkinleştirildiğinden emin olun
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["EmailSettings:SenderEmail"], _configuration["EmailSettings:SenderName"]),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(email);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Hata yakalama ve loglama
                throw new InvalidOperationException($"E-posta gönderimi sırasında bir hata oluştu: {ex.Message}", ex);
            }
        }
    }
}