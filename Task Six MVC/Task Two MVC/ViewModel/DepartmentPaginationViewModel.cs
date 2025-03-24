using Task_Two_MVC.Entities;

namespace Task_Two_MVC.ViewModel
{
    public class DepartmentPaginationViewModel
    {
        public List<Department> Departments { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
