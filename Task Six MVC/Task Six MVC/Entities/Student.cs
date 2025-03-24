using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Two_MVC.Entities
{
    public class Student
    {
        #region Properties

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Course Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(0, 100, ErrorMessage = "Age must be between 0 and 100")]
        public int Age { get; set; }

        [ForeignKey("DepartmentId")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public List<StuCrsRes> StuCrsRes { get; set; } = new();
        #endregion

    }
}
