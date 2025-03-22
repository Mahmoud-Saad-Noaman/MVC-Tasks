using Task_Two_MVC.Entities;
using System.Collections.Generic;

namespace Task_Two_MVC.ViewModel
{
    public class StudentPaginationViewModel
    {
        public List<Student> Students { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
