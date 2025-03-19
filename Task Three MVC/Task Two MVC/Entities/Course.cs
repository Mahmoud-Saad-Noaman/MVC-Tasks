using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Two_MVC.Entities
{
    public class Course
    {
        #region Properties

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }


        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
        public List<StuCrsRes> StuCrsRes { get; set; } = new List<StuCrsRes>();
        #endregion
    }
}
