using Hospital_Management.Models;
using Hospital_Management.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace Hospital_Management.Services
{
    public interface IEmailServices
    {
        Task SendEmailAsync(MailMessage mailMessage);

        Task SendConfirmationEmailAsync(ApplicationUser user, string token, IUrlHelper urlHelper);
        Task SendPasswordResetEmailAsync(ApplicationUser user, string token, IUrlHelper urlHelper);
        Task SendReservationDoneEmailAsync(ReservationEmailViewModel reservation);
    }
}
