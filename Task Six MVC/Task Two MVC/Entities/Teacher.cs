using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Two_MVC.Entities
{
    public class Teacher
    {
        #region Properties

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Address { get; set; }

        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }


        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        #endregion
    }
}
