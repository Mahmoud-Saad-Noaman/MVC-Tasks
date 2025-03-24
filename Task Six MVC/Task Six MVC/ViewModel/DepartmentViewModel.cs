using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Task_Two_MVC.ViewModel
{
    public class DepartmentViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters.")]
        public string SelectedDepartmentName { get; set; }

        [Required(ErrorMessage = "Manager name is required.")]
        [StringLength(100, ErrorMessage = "Manager name cannot exceed 100 characters.")]
        public string SelectedManagerName { get; set; }

        public List<SelectListItem> Managers { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Departments { get; set; } = new List<SelectListItem>();








        public string DepartmentName { get; set; }
        public List<SelectListItem> StudentsAbove25 { get; set; }
        public string DepartmentState { get; set; }
        

    }
}
