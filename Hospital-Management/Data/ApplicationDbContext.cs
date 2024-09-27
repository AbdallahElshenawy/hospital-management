using Hospital_Management.Models;
using Hospital_Management.Models.DataTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Hospital_Management.ViewModels;

namespace Hospital_Management.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Assistant> Assistants { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Article> Articles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Convert DaysOfWeek to int and vice versa.
            builder.Entity<Doctor>()
                .Property(u => u.WorkingDays)
                .HasConversion(
                    v => (int)v,
                    v => (DaysOfWeek)v);

            // Convert ReservationStatus to int and vice versa.
            builder.Entity<Reservation>()
                .Property(res => res.ReservationStatus)
                .HasConversion(
                    v => (int)v,
                    v => (ReservationStatus)v);

            // Set the precision of ExaminationFees to 2 decimal places.
            builder.Entity<Doctor>()
            .Property(d => d.ExaminationFees)
            .HasPrecision(18, 2);

            // Don't Delete the Assistants if a Doctor is deleted.
            builder.Entity<Assistant>()
                .HasOne(d => d.Doctor)
                .WithMany(d => d.Assistants)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            // Don't Delete the Rates if a Patient is deleted.
            builder.Entity<Rate>()
                .HasOne(r => r.Patient)
                .WithMany(p => p.Rates)
                .HasForeignKey(r => r.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            // Don't Delete the Record if a Doctor is deleted.
            builder.Entity<Record>()
                .HasOne(r => r.Doctor)
                .WithMany()
                .HasForeignKey(r => r.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            // Don't Delete the Reservation if a Patient is deleted.
            builder.Entity<Reservation>()
                .HasOne(r => r.Patient)
                .WithMany(p => p.Reservations)
                .HasForeignKey(r => r.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            // Don't Delete the Reservation if a Doctor is deleted.
            builder.Entity<Reservation>()
                .HasOne(r => r.Doctor)
                .WithMany(d => d.Reservations)
                .HasForeignKey(r => r.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);


            #region Data Seed.
            // Seed Roles
            var adminRoleId = Guid.NewGuid().ToString();
            var doctorRoleId = Guid.NewGuid().ToString();
            var assistantRoleId = Guid.NewGuid().ToString();
            var patientRoleId = Guid.NewGuid().ToString();
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = doctorRoleId, Name = "Doctor", NormalizedName = "DOCTOR" },
                new IdentityRole { Id = assistantRoleId, Name = "Assistant", NormalizedName = "ASSISTANT" },
                new IdentityRole { Id = patientRoleId, Name = "Patient", NormalizedName = "PATIENT" }
            );

            // Seed Admin Users
            var admin1Id = Guid.NewGuid().ToString();
            var admin2Id = Guid.NewGuid().ToString();

            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = admin1Id,
                    UserName = "admin1@example.com",
                    NormalizedUserName = "ADMIN1@EXAMPLE.COM",
                    Email = "admin1@example.com",
                    NormalizedEmail = "ADMIN1@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Super",
                    LastName = "Admin",
                    BirthDate = new DateTime(1970, 1, 1),
                    Address = "123 Admin St"
                },
                new ApplicationUser
                {
                    Id = admin2Id,
                    UserName = "admin2@example.com",
                    NormalizedUserName = "ADMIN2@EXAMPLE.COM",
                    Email = "admin2@example.com",
                    NormalizedEmail = "ADMIN2@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Master",
                    LastName = "Admin",
                    BirthDate = new DateTime(1975, 2, 2),
                    Address = "456 Admin Blvd"
                }
            );

            // Assign Roles to Admin Users
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = adminRoleId, UserId = admin1Id },
                new IdentityUserRole<string> { RoleId = adminRoleId, UserId = admin2Id }
            );

            // Seed Specialities
            builder.Entity<Speciality>().HasData(
                new Speciality { Id = 1, Name = "Cardiology" },
                new Speciality { Id = 2, Name = "Diagnosis" },
                new Speciality { Id = 3, Name = "Surgery" },
                new Speciality { Id = 4, Name = "First Aid" },
                new Speciality { Id = 5, Name = "Orthopedics" }
            );

            // Seed Doctors
            var doctor1Id = Guid.NewGuid().ToString();
            var doctor2Id = Guid.NewGuid().ToString();
            var doctor3Id = Guid.NewGuid().ToString();
            var doctor4Id = Guid.NewGuid().ToString();
            var doctor5Id = Guid.NewGuid().ToString();

            builder.Entity<Doctor>().HasData(
                new Doctor
                {
                    Id = doctor1Id,
                    UserName = "doctor1@example.com",
                    NormalizedUserName = "DOCTOR1@EXAMPLE.COM",
                    Email = "doctor1@example.com",
                    NormalizedEmail = "DOCTOR1@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Doctor@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "John",
                    LastName = "Doe",
                    BirthDate = new DateTime(1980, 1, 1),
                    SpecialityId = 1,
                    WorkingDays = DaysOfWeek.Monday | DaysOfWeek.Wednesday | DaysOfWeek.Friday,
                    WorkingHours = 8,
                    StartHour = 9,
                    ExaminationsMinutes = 20,
                    ExaminationFees = 200
                },
                new Doctor
                {
                    Id = doctor2Id,
                    UserName = "doctor2@example.com",
                    NormalizedUserName = "DOCTOR2@EXAMPLE.COM",
                    Email = "doctor2@example.com",
                    NormalizedEmail = "DOCTOR2@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Doctor@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Jane",
                    LastName = "Smith",
                    BirthDate = new DateTime(1985, 2, 15),
                    SpecialityId = 2,
                    WorkingDays = DaysOfWeek.Tuesday | DaysOfWeek.Thursday,
                    WorkingHours = 6,
                    StartHour = 10,
                    ExaminationsMinutes = 30,
                    ExaminationFees = 250
                },
                new Doctor
                {
                    Id = doctor3Id,
                    UserName = "doctor3@example.com",
                    NormalizedUserName = "DOCTOR3@EXAMPLE.COM",
                    Email = "doctor3@example.com",
                    NormalizedEmail = "DOCTOR3@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Doctor@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Alice",
                    LastName = "Johnson",
                    BirthDate = new DateTime(1975, 3, 20),
                    SpecialityId = 3,
                    WorkingDays = DaysOfWeek.Monday | DaysOfWeek.Thursday,
                    WorkingHours = 7,
                    StartHour = 8,
                    ExaminationsMinutes = 25,
                    ExaminationFees = 300
                },
                new Doctor
                {
                    Id = doctor4Id,
                    UserName = "doctor4@example.com",
                    NormalizedUserName = "DOCTOR4@EXAMPLE.COM",
                    Email = "doctor4@example.com",
                    NormalizedEmail = "DOCTOR4@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Doctor@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Bob",
                    LastName = "Williams",
                    BirthDate = new DateTime(1970, 4, 5),
                    SpecialityId = 4,
                    WorkingDays = DaysOfWeek.Wednesday | DaysOfWeek.Friday,
                    WorkingHours = 5,
                    StartHour = 11,
                    ExaminationsMinutes = 15,
                    ExaminationFees = 180
                },
                new Doctor
                {
                    Id = doctor5Id,
                    UserName = "doctor5@example.com",
                    NormalizedUserName = "DOCTOR5@EXAMPLE.COM",
                    Email = "doctor5@example.com",
                    NormalizedEmail = "DOCTOR5@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Doctor@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Charlie",
                    LastName = "Brown",
                    BirthDate = new DateTime(1965, 5, 10),
                    SpecialityId = 5,
                    WorkingDays = DaysOfWeek.Tuesday | DaysOfWeek.Thursday,
                    WorkingHours = 9,
                    StartHour = 9,
                    ExaminationsMinutes = 20,
                    ExaminationFees = 220
                }
            );

            // Assign Roles to Doctors
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = doctorRoleId, UserId = doctor1Id },
                new IdentityUserRole<string> { RoleId = doctorRoleId, UserId = doctor2Id },
                new IdentityUserRole<string> { RoleId = doctorRoleId, UserId = doctor3Id },
                new IdentityUserRole<string> { RoleId = doctorRoleId, UserId = doctor4Id },
                new IdentityUserRole<string> { RoleId = doctorRoleId, UserId = doctor5Id }
            );

            // Seed Assistants
            var assistant1Id = Guid.NewGuid().ToString();
            var assistant2Id = Guid.NewGuid().ToString();
            var assistant3Id = Guid.NewGuid().ToString();
            var assistant4Id = Guid.NewGuid().ToString();
            var assistant5Id = Guid.NewGuid().ToString();

            builder.Entity<Assistant>().HasData(
                new Assistant
                {
                    Id = assistant1Id,
                    UserName = "assistant1@example.com",
                    NormalizedUserName = "ASSISTANT1@EXAMPLE.COM",
                    Email = "assistant1@example.com",
                    NormalizedEmail = "ASSISTANT1@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Assistant@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Eve",
                    LastName = "Adams",
                    BirthDate = new DateTime(1990, 1, 1),
                    DoctorId = doctor1Id
                },
                new Assistant
                {
                    Id = assistant2Id,
                    UserName = "assistant2@example.com",
                    NormalizedUserName = "ASSISTANT2@EXAMPLE.COM",
                    Email = "assistant2@example.com",
                    NormalizedEmail = "ASSISTANT2@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Assistant@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Frank",
                    LastName = "Clark",
                    BirthDate = new DateTime(1992, 3, 10),
                    DoctorId = doctor2Id
                },
                new Assistant
                {
                    Id = assistant3Id,
                    UserName = "assistant3@example.com",
                    NormalizedUserName = "ASSISTANT3@EXAMPLE.COM",
                    Email = "assistant3@example.com",
                    NormalizedEmail = "ASSISTANT3@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Assistant@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Grace",
                    LastName = "Harris",
                    BirthDate = new DateTime(1994, 5, 15),
                    DoctorId = doctor3Id
                },
                new Assistant
                {
                    Id = assistant4Id,
                    UserName = "assistant4@example.com",
                    NormalizedUserName = "ASSISTANT4@EXAMPLE.COM",
                    Email = "assistant4@example.com",
                    NormalizedEmail = "ASSISTANT4@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Assistant@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Henry",
                    LastName = "Baker",
                    BirthDate = new DateTime(1996, 7, 20),
                    DoctorId = doctor4Id
                },
                new Assistant
                {
                    Id = assistant5Id,
                    UserName = "assistant5@example.com",
                    NormalizedUserName = "ASSISTANT5@EXAMPLE.COM",
                    Email = "assistant5@example.com",
                    NormalizedEmail = "ASSISTANT5@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Assistant@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Ivy",
                    LastName = "Davis",
                    BirthDate = new DateTime(1998, 9, 25),
                    DoctorId = doctor5Id
                }
            );

            // Assign Roles to Assistants
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = assistantRoleId, UserId = assistant1Id },
                new IdentityUserRole<string> { RoleId = assistantRoleId, UserId = assistant2Id },
                new IdentityUserRole<string> { RoleId = assistantRoleId, UserId = assistant3Id },
                new IdentityUserRole<string> { RoleId = assistantRoleId, UserId = assistant4Id },
                new IdentityUserRole<string> { RoleId = assistantRoleId, UserId = assistant5Id }
            );

            // Seed Patients
            var patient1Id = Guid.NewGuid().ToString();
            var patient2Id = Guid.NewGuid().ToString();
            var patient3Id = Guid.NewGuid().ToString();
            var patient4Id = Guid.NewGuid().ToString();
            var patient5Id = Guid.NewGuid().ToString();

            builder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = patient1Id,
                    UserName = "patient1@example.com",
                    NormalizedUserName = "PATIENT1@EXAMPLE.COM",
                    Email = "patient1@example.com",
                    NormalizedEmail = "PATIENT1@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Patient@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Olivia",
                    LastName = "Brown",
                    BirthDate = new DateTime(2000, 1, 1)
                },
                new Patient
                {
                    Id = patient2Id,
                    UserName = "patient2@example.com",
                    NormalizedUserName = "PATIENT2@EXAMPLE.COM",
                    Email = "patient2@example.com",
                    NormalizedEmail = "PATIENT2@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Patient@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Sophia",
                    LastName = "Miller",
                    BirthDate = new DateTime(2002, 3, 10)
                },
                new Patient
                {
                    Id = patient3Id,
                    UserName = "patient3@example.com",
                    NormalizedUserName = "PATIENT3@EXAMPLE.COM",
                    Email = "patient3@example.com",
                    NormalizedEmail = "PATIENT3@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Patient@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Liam",
                    LastName = "Wilson",
                    BirthDate = new DateTime(2004, 5, 15)
                },
                new Patient
                {
                    Id = patient4Id,
                    UserName = "patient4@example.com",
                    NormalizedUserName = "PATIENT4@EXAMPLE.COM",
                    Email = "patient4@example.com",
                    NormalizedEmail = "PATIENT4@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Patient@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Noah",
                    LastName = "Taylor",
                    BirthDate = new DateTime(2006, 7, 20)
                },
                new Patient
                {
                    Id = patient5Id,
                    UserName = "patient5@example.com",
                    NormalizedUserName = "PATIENT5@EXAMPLE.COM",
                    Email = "patient5@example.com",
                    NormalizedEmail = "PATIENT5@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Patient@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Emma",
                    LastName = "Anderson",
                    BirthDate = new DateTime(2008, 9, 25)
                }
            );

            // Assign Roles to Patients
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = patientRoleId, UserId = patient1Id },
                new IdentityUserRole<string> { RoleId = patientRoleId, UserId = patient2Id },
                new IdentityUserRole<string> { RoleId = patientRoleId, UserId = patient3Id },
                new IdentityUserRole<string> { RoleId = patientRoleId, UserId = patient4Id },
                new IdentityUserRole<string> { RoleId = patientRoleId, UserId = patient5Id }
            );

            // Seed Articles
            builder.Entity<Article>().HasData(
                new Article { Id = 1, Title = "The Importance of Regular Checkups", Content = "Regular checkups can help identify potential health issues...", DateTime = DateTime.Now, DoctorId = doctor1Id },
                new Article { Id = 2, Title = "Healthy Eating Habits", Content = "A balanced diet is essential for maintaining good health...", DateTime = DateTime.Now.AddDays(-2), DoctorId = doctor2Id },
                new Article { Id = 3, Title = "Managing Stress", Content = "Stress management techniques are important for overall well-being...", DateTime = DateTime.Now.AddDays(-4), DoctorId = doctor3Id },
                new Article { Id = 4, Title = "Understanding Diabetes", Content = "Diabetes management is crucial for preventing complications...", DateTime = DateTime.Now.AddDays(-6), DoctorId = doctor4Id },
                new Article { Id = 5, Title = "The Benefits of Exercise", Content = "Regular exercise can improve your physical and mental health...", DateTime = DateTime.Now.AddDays(-8), DoctorId = doctor5Id }
            );

            // Seed Rates
            builder.Entity<Rate>().HasData(
                new Rate { Id = 1, DoctorId = doctor1Id, PatientId = patient1Id, Value = 5, Comment = "Excellent service!" },
                new Rate { Id = 2, DoctorId = doctor2Id, PatientId = patient2Id, Value = 4, Comment = "Very good, but could be quicker." },
                new Rate { Id = 3, DoctorId = doctor3Id, PatientId = patient3Id, Value = 3, Comment = "Average experience." },
                new Rate { Id = 4, DoctorId = doctor4Id, PatientId = patient4Id, Value = 2, Comment = "Not satisfied with the consultation." },
                new Rate { Id = 5, DoctorId = doctor5Id, PatientId = patient5Id, Value = 1, Comment = "Poor service." }
            );

            // Seed Records
            builder.Entity<Record>().HasData(
                new Record { Id = 1, Description = "Annual physical exam", Diagnosis = "Healthy", Prescription = "Continue regular exercise", Notes = "Patient is in good health.", Date = DateTime.Now.AddMonths(-1), PatientId = patient1Id, DoctorId = doctor1Id },
                new Record { Id = 2, Description = "Follow-up for hypertension", Diagnosis = "Hypertension under control", Prescription = "Continue medication", Notes = "Blood pressure is stable.", Date = DateTime.Now.AddMonths(-2), PatientId = patient2Id, DoctorId = doctor2Id },
                new Record { Id = 3, Description = "Consultation for diabetes management", Diagnosis = "Type 2 diabetes", Prescription = "Insulin therapy", Notes = "Patient is advised to monitor blood sugar levels.", Date = DateTime.Now.AddMonths(-3), PatientId = patient3Id, DoctorId = doctor3Id },
                new Record
                {
                    Id = 4,
                    Description = "Initial consultation for weight management",
                    Diagnosis = "Obesity",
                    Prescription = "Diet and exercise plan",
                    Notes = "Patient is advised to follow a structured diet and exercise regimen.",
                    Date = DateTime.Now.AddMonths(-4),
                    PatientId = patient4Id,
                    DoctorId = doctor4Id
                },
                new Record
                {
                    Id = 5,
                    Description = "Consultation for chronic back pain",
                    Diagnosis = "Chronic lower back pain",
                    Prescription = "Physical therapy and pain management",
                    Notes = "Patient is referred to physical therapy.",
                    Date = DateTime.Now.AddMonths(-5),
                    PatientId = patient5Id,
                    DoctorId = doctor5Id
                }
               );

            // Seed Reservations
            builder.Entity<Reservation>().HasData(
                new Reservation { Id = 1, Date = DateTime.Now.AddDays(7), PatientId = patient1Id, DoctorId = doctor1Id, AssistantId = assistant1Id },
                new Reservation { Id = 2, Date = DateTime.Now.AddDays(14), PatientId = patient2Id, DoctorId = doctor2Id, AssistantId = assistant2Id },
                new Reservation { Id = 3, Date = DateTime.Now.AddDays(21), PatientId = patient3Id, DoctorId = doctor3Id, AssistantId = assistant3Id },
                new Reservation { Id = 4, Date = DateTime.Now.AddDays(28), PatientId = patient4Id, DoctorId = doctor4Id, AssistantId = assistant4Id },
                new Reservation { Id = 5, Date = DateTime.Now.AddDays(35), PatientId = patient5Id, DoctorId = doctor5Id, AssistantId = assistant5Id }
            );

            #endregion
        }
    }
}
