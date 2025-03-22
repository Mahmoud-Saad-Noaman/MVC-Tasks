using Microsoft.AspNetCore.Mvc.Rendering;

namespace Task_Two_MVC.ViewModel
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int DepartmentId { get; set; }

        public IEnumerable<SelectListItem>? Departments { get; set; }
    }
}
