using Task_Two_MVC.Entities;

namespace Task_Two_MVC.ViewModel
{
    public class CoursePaginationViewModel
    {
        public List<Course> Courses { get; set; } = new List<Course>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
