namespace Hospital_Management.ViewModels
{
    public class PatientRecordsVM
    {
        public string PatientId { get; set; }

        public string ?PatientName { get; set; }
        
        public List<RecordViewModel> Records { get; set; }
    }
}
