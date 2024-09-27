namespace Hospital_Management.ViewModels
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? DateTime { get; set; }
        public string DoctorName { get; set; }
        public string DoctorId { get; set; }
        public byte[] DoctorImage { get; set; }
    }
}
