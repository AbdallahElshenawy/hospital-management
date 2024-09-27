using Hospital_Management.Models;
using Hospital_Management.Repository;

namespace Hospital_Management.ViewModels
{
    public class HomeViewModel()
    {
        public List<Article> HomeArticles { get; set; }
        public List<Doctor> HomeDoctors { get; set; }
        public List<RateViewModel> HomeTestimonial { get; set; }
        public List<Speciality> Specialities { get; set; }
    }
}
