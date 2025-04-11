using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Two_MVC.Entities
{
    public class Student
    {
        #region Properties

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Age { get; set; }

        [ForeignKey("DepartmentId")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public List<StuCrsRes> StuCrsRes { get; set; } = new();
        #endregion

    }
}
