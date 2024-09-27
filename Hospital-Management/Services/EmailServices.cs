
using Azure.Core;
using Hospital_Management.Models;
using Hospital_Management.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;

namespace Hospital_Management.Services
{
    public class EmailServices(IConfiguration configuration,
        IWebHostEnvironment environment
        ) : IEmailServices
    {
        private readonly string emailAddress = configuration["EmailOptions:Email"];
        private readonly string emailPassword = configuration["EmailOptions:EmailPassword"];

        public async Task SendEmailAsync(MailMessage mailMessage)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(emailAddress, emailPassword),
                EnableSsl = true,
            };

            await smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendConfirmationEmailAsync(ApplicationUser user, string token, IUrlHelper urlHelper)
        {
            
            var confirmationLink = urlHelper.Action("ConfirmEmail", "User", new { userId = user.Id, token }, "http");

            var mailSubject = "Confirm your email";

            var template = await GetEmailTemplate("ConfirmEmail");

            var mailBody = template.Replace("{{confirmation_link}}", confirmationLink);

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailAddress),
                Subject = mailSubject,
                Body = mailBody,
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8,
            };

            mailMessage.To.Add(user.Email);

            await SendEmailAsync(mailMessage);
        }

        public async Task SendPasswordResetEmailAsync(ApplicationUser user, string token, IUrlHelper urlHelper)
        {
            var confirmationLink = urlHelper.Action("ResetPassword", "User", new { userId = user.Id, token }, "http");

            var mailSubject = "Requested Password reset email";

            var template = await GetEmailTemplate("ResetPassword");

            var mailBodyBeforeLink = template.Replace("{{UserName}}", user.FirstName);
            var mailBody = mailBodyBeforeLink.Replace("{{ResetLink}}", confirmationLink);

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailAddress),
                Subject = mailSubject,
                Body = mailBody,
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8,
            };

            mailMessage.To.Add(user.Email);

            await SendEmailAsync(mailMessage);
        }
        
        public async Task SendReservationDoneEmailAsync(ReservationEmailViewModel reservation)
        {
            var mailSubject = "Reservation done successfully";

            var template = await GetEmailTemplate("ReservationSucceded");

            var mailBodyBeforeDoctor = template.Replace("{{UserName}}", reservation.PatientName);
            var mailBodyBeforeDate = mailBodyBeforeDoctor.Replace("{{DoctorName}}", reservation.DoctorName);
            var mailBodyBeforeTime = mailBodyBeforeDate.Replace("{{ReservationDate}}", reservation.Date.Date.ToString());
            var mailBody = mailBodyBeforeTime.Replace("{{ReservationTime}}", reservation.Date.TimeOfDay.ToString());

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailAddress),
                Subject = mailSubject,
                Body = mailBody,
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8,
            };

            mailMessage.To.Add(reservation.PatientEmail);

            await SendEmailAsync(mailMessage);
        }

        private async Task<string> GetEmailTemplate(string templateName)
        {
            var templatePath = Path.Combine(environment.WebRootPath, "EmailTemplates", $"{templateName}.html");

            return await File.ReadAllTextAsync(templatePath);
        }
    }
}
