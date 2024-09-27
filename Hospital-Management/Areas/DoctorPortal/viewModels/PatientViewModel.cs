namespace Hospital_Management.ViewModels
{
    public class PatientViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string DoctorId { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}