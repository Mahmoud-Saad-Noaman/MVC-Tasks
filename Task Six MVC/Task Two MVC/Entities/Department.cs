using System.ComponentModel.DataAnnotations;

namespace Task_Two_MVC.Entities
{
    public class Department
    {
        #region Properties
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string MgrName { get; set; }


        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Course> Courses { get; set; } = new List<Course>();
        #endregion
    }
}
