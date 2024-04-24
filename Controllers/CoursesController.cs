using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceBasedApplication.Services;
using ServiceBasedApplication.ViewModels;

namespace ServiceBasedApplication.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var courseViewModels = await _courseService.GetCourses();

            return courseViewModels != null ?
                        View(courseViewModels) : Problem("Entity set 'SchoolsDbContext.Courses'  is null.");
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (_courseService.GetCourses == null)
            {
                return NotFound();
            }

            var courseViewModel = await _courseService.GetCourseById(id);

            if (courseViewModel == null)
            {
                return NotFound();
            }

            return View(courseViewModel);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel courseViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(courseViewModel);
            }

            await _courseService.AddCourse(courseViewModel);

            return RedirectToAction(nameof(Index));
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _courseService.GetCourses() == null)
            {
                return NotFound();
            }

            var courseViewModel = await _courseService.GetCourseById(id);

            if (courseViewModel == null)
            {
                return NotFound();
            }

            return View(courseViewModel);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseViewModel courseViewModel)
        {
            if (id != courseViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(courseViewModel);
            }

            try
            {
                courseViewModel = await _courseService.UpdateCourse(courseViewModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_courseService.CourseExists(courseViewModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw new Exception();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _courseService.GetCourses == null)
            {
                return NotFound();
            }

            var courseViewModel = await _courseService.GetCourseById(id);
            if (courseViewModel == null)
            {
                return NotFound();
            }

            return View(courseViewModel);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_courseService.GetCourses == null)
            {
                return Problem("Entity set 'SchoolsDbContext.Courses'  is null.");
            }
            var courseViewModel = await _courseService.GetCourseById(id);

            if (courseViewModel != null)
            {
                await _courseService.RemoveCourse(courseViewModel.Id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
