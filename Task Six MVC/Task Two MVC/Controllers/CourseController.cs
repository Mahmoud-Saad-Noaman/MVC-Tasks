using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task_Two_MVC.Data;
using Task_Two_MVC.Entities;
using Task_Two_MVC.ViewModel;

namespace Task_Two_MVC.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private const int PageSize = 3;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        #region Show All
        public IActionResult ShowAll(int pageNumber = 1)
        {
            var coursesQuery = _context.Courses
                .Include(c => c.Department)
                .OrderBy(c => c.Id);

            int totalCourses = coursesQuery.Count();
            int totalPages = (int)Math.Ceiling(totalCourses / (double)PageSize);

            var courses = coursesQuery
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            var viewModel = new CoursePaginationViewModel
            {
                Courses = courses,
                CurrentPage = pageNumber,
                TotalPages = totalPages
            };

            return View(viewModel);
        }
        #endregion

        #region Show By Id
        public IActionResult ShowById(int id)
        {
            var course = _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Teachers)
                .Include(c => c.StuCrsRes)
                .FirstOrDefault(c => c.Id == id);

            if (course == null)
                return NotFound();

            return View(course);
        }
        #endregion

        #region Create
        //  /Course/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new CourseViewModel
            {
                Departments = await _context.Departments
                              .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                              .ToListAsync()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var course = new Course
                {
                    Name = model.Name,
                    Degree = model.Degree,
                    MinDegree = model.MinDegree,
                    DepartmentId = model.DepartmentId
                };
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ShowAll));
            }

            model.Departments = await _context.Departments
                              .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                              .ToListAsync();

            return View(model);
        }
        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return NotFound();

            var viewModel = new CourseViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Degree = course.Degree,
                MinDegree = course.MinDegree,
                DepartmentId = course.DepartmentId,
                Departments = await _context.Departments
                              .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                              .ToListAsync()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var course = await _context.Courses.FindAsync(id);
                if (course == null)
                    return NotFound();

                course.Name = model.Name;
                course.Degree = model.Degree;
                course.MinDegree = model.MinDegree;
                course.DepartmentId = model.DepartmentId;

                _context.Update(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ShowAll));
            }

            model.Departments = await _context.Departments
                              .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                              .ToListAsync();

            return View(model);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses.Include(c => c.Department).FirstOrDefaultAsync(c => c.Id == id);
            if (course == null)
                return NotFound();

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ShowAll));
        }
        #endregion
    }
}
