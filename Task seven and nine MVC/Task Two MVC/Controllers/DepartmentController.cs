using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Task_Two_MVC.Data;
using Task_Two_MVC.Entities;
using Task_Two_MVC.ViewModel;

namespace Task_Two_MVC.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        // /Department/ShowAll
        public ActionResult ShowAll()
        {
            var departments = _context.Departments
                .OrderByDescending(d => d.Id) 
                .ToList();

            return View(departments);
        }

        // /Department/ShowDetails/1
        public ActionResult ShowDetails(int id)
        {
            var department = _context.Departments.SingleOrDefault(d => d.Id == id);
            if (department == null)
                return NotFound();

            return View(department);
        }

        // /Department/Add
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
        public ActionResult Add(Department department)
        {
            if (!ModelState.IsValid)
                return View(department);

            _context.Departments.Add(department);
            _context.SaveChanges();
            
            TempData["SuccessMessage"] = "Department added successfully.";
            TempData["AlertType"] = "success";

            return RedirectToAction("ShowAll");
        }


        // /Department/DepartmentDetails/1
        public ActionResult DepartmentDetails(int id)
        {
            var department = _context.Departments.SingleOrDefault(d => d.Id == id);
            if (department == null)
                return NotFound();

            var studentsAbove25 = _context.Students
                .Where(s => s.DepartmentId == id && s.Age > 25)
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
                .ToList();

            var departmentState = _context.Students.Count(s => s.DepartmentId == id)> 50 ? "Main" : "Branch";

            //string deptState;
            //if (departmentState > 50)
            //    deptState =  "Main";
            //else
            //    deptState = "Branch";


            var viewModel = new DepartmentViewModel
            {
                DepartmentName = department.Name,
                StudentsAbove25 = studentsAbove25,
                DepartmentState = departmentState
            };

            return View(viewModel);
        }

    }
}
