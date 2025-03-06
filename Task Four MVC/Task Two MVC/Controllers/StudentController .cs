
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task_Two_MVC.Data;
using Task_Two_MVC.Entities;
using Task_Two_MVC.Models;

namespace Task_Two_MVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
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

        #region Create

        // GET: /Student/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new StudentViewModel
            {
                Departments = await _context.Departments
                            .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                            .ToListAsync()
            };
            return View(viewModel);
        }
        
        // POST: /Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    Name = model.Name,
                    Age = model.Age,
                    DepartmentId = model.DepartmentId
                };
                _context.Students.Add(student);
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
        // GET: /Student/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound();

            var viewModel = new StudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                DepartmentId = student.DepartmentId,
                Departments = await _context.Departments
                            .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                            .ToListAsync()
            };
            return View(viewModel);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var student = await _context.Students.FindAsync(id);
                if (student == null)
                    return NotFound();

                student.Name = model.Name;
                student.Age = model.Age;
                student.DepartmentId = model.DepartmentId;

                _context.Update(student);
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
        // GET: /Student/Delete/6
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.Include(s => s.Department).FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
                return NotFound();

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound();

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ShowAll));
        }
        #endregion

    }
}
