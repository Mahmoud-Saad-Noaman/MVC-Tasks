
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_Two_MVC.Data;
using Task_Two_MVC.Entities;

namespace Task_Two_MVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Show All
        [HttpGet]
        // /student/ShowAll
        public IActionResult ShowAll()
        {
            var std = _context.Students
                .Include(s => s.Department)
                .ToList();

            return View(std);

        }
        #endregion




        #region Show By Id
        [HttpGet]
        //[Route("Student/ShowById/{id}")]
        public  IActionResult ShowById(int id)
        {
            var student =  _context.Students
                .Include(s => s.Department)
                .Include(s => s.StuCrsRes)
                    .ThenInclude(cr => cr.Course)
                .FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        #endregion

    }
}
