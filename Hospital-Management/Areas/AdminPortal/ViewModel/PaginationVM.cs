
using Hospital_Management.Models;

namespace Hospital_Management.Areas.AdminPortal.ViewModel
{
    public class PaginationVM<Type>
    {
       
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage => CurrentPage < TotalPages;
        public bool HasPreviousPage => CurrentPage > 1;
        public List<Type> Items { get; set; }

       
    }
}
