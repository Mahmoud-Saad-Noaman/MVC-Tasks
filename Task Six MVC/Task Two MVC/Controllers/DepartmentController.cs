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
        public IActionResult ShowAll(int pageNumber = 1, int pageSize = 3)
        {
            var totalDepartments = _context.Departments.Count();
            var departments = _context.Departments
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new DepartmentPaginationViewModel
            {
                Departments = departments,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalDepartments / (double)pageSize)
            };

            return View(viewModel);
        }


        // /Department/ShowDetails/1
        public ActionResult ShowDetails(int id)
        {
            var department = _context.Departments.SingleOrDefault(d => d.Id == id);
            if (department == null)
                return NotFound();

            return View(department);
        }
        #region Add

        // Department/Add
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Department department)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is NOT valid. Errors:");

                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"- {error.ErrorMessage}");
                }

                return View(department);
            }

            // ✅ التحقق من عدم وجود قسم بنفس الاسم مسبقًا
            if (_context.Departments.Any(d => d.Name == department.Name))
            {
                ModelState.AddModelError("Name", "A department with this name already exists.");
                return View(department);
            }

            _context.Departments.Add(department);
            _context.SaveChanges();
            return RedirectToAction("ShowAll");
        }

        //[HttpPost]
        //public ActionResult Add(Department department)
        //{
        //    if (!ModelState.IsValid)
        //        return View(department);

        //    _context.Departments.Add(department);
        //    _context.SaveChanges();
        //    return RedirectToAction("ShowAll");
        //}

        #endregion

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

            var departmentState = _context.Students.Count(s => s.DepartmentId == id) > 50 ? "Main" : "Branch";

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

        #region Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var department = _context.Departments.FirstOrDefault(u => u.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            var model = new DepartmentViewModel
            {
                Id = department.Id,
                SelectedDepartmentName = department.Name,
                SelectedManagerName = department.MgrName,
                Managers = _context.Teachers.Select(t => new SelectListItem
                {
                    Value = t.Name,
                    Text = t.Name
                }).ToList(),
                Departments = _context.Departments.Select(d => new SelectListItem
                {
                    Value = d.Name,
                    Text = d.Name
                }).ToList()
            };

            return View(model);
        }

        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    var department = _context.Departments.FirstOrDefault(u => u.Id == id);
        //    if (department == null)
        //    {
        //        return NotFound();
        //    }

        //    var model = new DepartmentViewModel
        //    {
        //        Id = department.Id,
        //        SelectedDepartmentName = department.Name, // تحميل اسم القسم الحالي
        //        SelectedManagerName = department.MgrName, // تحميل اسم المدير الحالي
        //        Managers = _context.Teachers.Select(t => new SelectListItem
        //        {
        //            Value = t.Name,
        //            Text = t.Name
        //        }).ToList(),
        //        Departments = _context.Departments.Select(d => new SelectListItem
        //        {
        //            Value = d.Name,
        //            Text = d.Name
        //        }).ToList()
        //    };

        //    // طباعة البيانات للتحقق من صحتها
        //    Console.WriteLine($"[GET] Edit - Department: {model.SelectedDepartmentName}, Manager: {model.SelectedManagerName}");

        //    return View(model);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DepartmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is NOT valid. Errors:");

                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"- {error.ErrorMessage}");
                }

                model.Managers = _context.Teachers.Select(t => new SelectListItem
                {
                    Value = t.Name,
                    Text = t.Name
                }).ToList();

                model.Departments = _context.Departments.Select(d => new SelectListItem
                {
                    Value = d.Name,
                    Text = d.Name
                }).ToList();

                return View(model);
            }

            var department = _context.Departments.FirstOrDefault(u => u.Id == model.Id);
            if (department == null)
            {
                return NotFound();
            }

            // ✅ التأكد من أن الاسم الجديد غير مكرر في قاعدة البيانات
            if (_context.Departments.Any(d => d.Name == model.SelectedDepartmentName.Trim() && d.Id != model.Id))
            {
                ModelState.AddModelError("SelectedDepartmentName", "A department with this name already exists.");
                return View(model);
            }

            department.Name = model.SelectedDepartmentName.Trim();
            department.MgrName = model.SelectedManagerName.Trim();

            _context.Departments.Update(department);
            _context.SaveChanges();

            return RedirectToAction("ShowAll");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(DepartmentViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        Console.WriteLine("ModelState is NOT valid. Errors:");

        //        // طباعة جميع الأخطاء الموجودة في ModelState
        //        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        //        {
        //            Console.WriteLine($"- {error.ErrorMessage}");
        //        }

        //        // إعادة تحميل القوائم عند وجود خطأ في النموذج
        //        model.Managers = _context.Teachers.Select(t => new SelectListItem
        //        {
        //            Value = t.Name,
        //            Text = t.Name
        //        }).ToList();

        //        model.Departments = _context.Departments.Select(d => new SelectListItem
        //        {
        //            Value = d.Name,
        //            Text = d.Name
        //        }).ToList();

        //        return View(model);
        //    }

        //    var department = _context.Departments.FirstOrDefault(u => u.Id == model.Id);
        //    if (department == null)
        //    {
        //        return NotFound();
        //    }

        //    // تحديث بيانات القسم
        //    department.Name = model.SelectedDepartmentName.Trim();
        //    department.MgrName = model.SelectedManagerName.Trim();

        //    _context.Departments.Update(department);
        //    _context.SaveChanges();

        //    return RedirectToAction("ShowAll");
        //}

        #endregion

    }
}
