namespace Hospital_Management.ViewModels
{
    public class RecordViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public string? PatientId { get; set; }
        public string ?DoctorId { get; set; }
        public string? PatientName { get; set; }

        
    }
}