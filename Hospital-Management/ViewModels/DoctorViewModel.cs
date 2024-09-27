namespace Hospital_Management.ViewModels
{
    public class DoctorViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public byte[] Img { get; set; }
        public string SpecialityName { get; set; }
        public int AvgRating { get; set; }
    }
}