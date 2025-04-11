using Microsoft.AspNetCore.Mvc.Rendering;

namespace Task_Two_MVC.ViewModel
{
    public class DepartmentViewModel
    {
        public string DepartmentName { get; set; }
        public List<SelectListItem> StudentsAbove25 { get; set; }
        public string DepartmentState { get; set; }
    }
}
