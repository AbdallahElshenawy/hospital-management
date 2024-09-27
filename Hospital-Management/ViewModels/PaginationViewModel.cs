namespace Hospital_Management.ViewModels
{
    public class PaginationViewModel<Type>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage => CurrentPage < TotalPages;
        public bool HasPreviousPage => CurrentPage > 1;
        public List<Type> Items { get; set; }
    }
}
