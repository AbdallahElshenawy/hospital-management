using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.ViewModels
{
    public class UserProfileDataViewModel
    {
            public string UserId { get; set; }
            [Remote(action: "VerifyUserName", controller: "Profile", ErrorMessage = "Email already in use.")]
            public string UserName { get; set; }
            [Remote(action: "VerifyUser", controller: "Profile", ErrorMessage = "Email already in use.")]
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string BirthDate { get; set; }
            public byte[]? Img { get; set; }
            public List<Reservation>? PatientReservation { get; set; }
 
            public List<Rate>? DoctorRates { get; set; }

    }
}
