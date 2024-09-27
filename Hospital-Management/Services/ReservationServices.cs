using Hospital_Management.Data;
using Hospital_Management.Models;
using Hospital_Management.Services.Helpers;
using Hospital_Management.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Services
{
    public class ReservationServices(ApplicationDbContext context) : IReservationServices
    {
        public async Task<DoctorWithResPropertiesViewModel> GetDoctorWithResPropertiesAsync(string doctorId)
        {
            var doctor = await context.Doctors
                .Where(d => d.Id == doctorId)
                .Select(d =>  new DoctorWithResPropertiesViewModel
                {
                    Id = d.Id,
                    Name = d.FullName,
                    SpecialityName = d.Speciality.Name,
                    AvgRating = (int)d.Rates.Average(r => r.Value),
                    WorkingDays = d.WorkingDays,
                    WorkingHours = d.WorkingHours,
                    StartHour = d.StartHour,
                    ExaminationsMinutes = d.ExaminationsMinutes,
                    ExaminationFees = d.ExaminationFees
                }).FirstOrDefaultAsync();

            return doctor;
        }

        public async Task<List<ReservationInListViewModel>> GetReservationsInListAsync(string doctorId, DateTime date)
        {
            // Get the doctor.
            var doctorReservationProperties = await context.Doctors
                .Where(d => d.Id == doctorId)
                .Select(d => new {
                    StartHour = d.StartHour,
                    WorkingHours = d.WorkingHours,
                    ExaminationMinutes = d.ExaminationsMinutes
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            // Check if the doctor not found.
            if(doctorReservationProperties == null)
            {
                return new List<ReservationInListViewModel>();
            }

            // Get the taken reservations.
            var takenReservations = await context.Reservations
                .Where(r => r.DoctorId == doctorId && r.Date.Date == date.Date && r.ReservationStatus == Models.DataTypes.ReservationStatus.Pending)
                .Select(r => new ReservationInListViewModel
                {
                    Date = r.Date,
                    Description = "Description",
                })
                .AsNoTracking()
                .ToListAsync();

            // Store doctors working times to use it to generate all possible reservations.
            var doctorStartHour = date.AddHours(doctorReservationProperties.StartHour);
            var doctorEndHour = date.AddHours(doctorReservationProperties.StartHour + doctorReservationProperties.WorkingHours);

            // Generate all possible reservations.
            var allReservations = new List<ReservationInListViewModel>();
            while (doctorStartHour < doctorEndHour)
            {
                allReservations.Add(new ReservationInListViewModel
                {
                    Date = doctorStartHour,
                    Description = doctorStartHour.ToString("hh:mm tt"),
                });
                doctorStartHour = doctorStartHour.AddMinutes(doctorReservationProperties.ExaminationMinutes);
            }

            // Get the available reservations by removing the taken ones.
            var availableReservations = allReservations.Except(takenReservations, new ReservationServiceResComparer()).OrderBy(r => r.Date).ToList();

            return availableReservations;
        }

        public async Task<Reservation> ReserveAsync(string doctorId, DateTime date, string userName)
        {
            // Get the doctor.
            var doctorIdAndAssId = await context.Doctors
                .Where(d => d.Id == doctorId)
                .Select(d => new {
                    docId = d.Id,
                    assId = d.Assistants.FirstOrDefault().Id
                }).FirstOrDefaultAsync();

            // Get the user.
            var userId = await context.Users
                .Where(u => u.UserName == userName)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            // Create the reservation.
            var reservation = new Reservation
            {
                DoctorId = doctorIdAndAssId.docId,
                PatientId = userId,
                AssistantId = doctorIdAndAssId.assId,
                Date = date,
                ReservationStatus = Models.DataTypes.ReservationStatus.Pending,
            };

            // Add the reservation to the context.
            context.Reservations.Add(reservation);

            // Save the changes.
            await context.SaveChangesAsync();

            return reservation;
        }

        public Task<ReservationEmailViewModel> GetReservationEmailViewModelAsync(int reservationId)
        {
            var model = context.Reservations
                .Where(r => r.Id == reservationId)
                .Select(r => new ReservationEmailViewModel
                {
                    DoctorName = r.Doctor.FullName,
                    PatientName = r.Patient.FullName,
                    PatientEmail = r.Patient.Email,
                    Date = r.Date,
                }).FirstOrDefaultAsync();

            return model;
        }
    }
}
