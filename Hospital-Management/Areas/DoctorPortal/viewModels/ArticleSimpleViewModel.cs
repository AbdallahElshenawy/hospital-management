namespace Hospital_Management.ViewModels
{ 
public class ArticleSimpleViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    public string? DoctorId { get; set; }
}
}