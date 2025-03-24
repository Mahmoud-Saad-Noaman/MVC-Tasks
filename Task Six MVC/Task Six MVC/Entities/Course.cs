using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Two_MVC.Entities
{
    public class Course
    {
        #region Properties

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Course Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Degree is required")]
        [Range(0, 100, ErrorMessage = "Degree must be between 0 and 100")]
        public int Degree { get; set; }

        [Required(ErrorMessage = "Minimum Degree is required")]
        [Range(0, 100, ErrorMessage = "Minimum Degree must be between 0 and 100")]
        public int MinDegree { get; set; }


        [ForeignKey("DepartmentId")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Department is required")]
        public Department Department { get; set; }
        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
        public List<StuCrsRes> StuCrsRes { get; set; } = new List<StuCrsRes>();
        #endregion
    }
}
