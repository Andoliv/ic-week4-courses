using ServiceBasedApplication.Models;
using Microsoft.EntityFrameworkCore;
using ServiceBasedApplication.ViewModels;
using AutoMapper;

namespace ServiceBasedApplication.Services
{
    public class CourseService : ICourseService
    {
        private readonly SchoolsDbContext _context;
        private readonly IMapper _mapper;

        public CourseService(SchoolsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CourseViewModel> AddCourse(CourseViewModel courseViewModel)
        {
            var course = _mapper.Map<Course>(courseViewModel);

            _context.Add(course);
            await _context.SaveChangesAsync();

            return courseViewModel;
        }

        public async Task<CourseViewModel> UpdateCourse(CourseViewModel courseViewModel)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(course => course.Id == courseViewModel.Id);

            if (course == null)
            {
                throw new Exception("The course you are looking for does not exist!");
            }

            if (CourseExists(courseViewModel.Id))
            {
                course.Title = courseViewModel.Title;
                course.Credits = courseViewModel.Credits;
            }

            _context.Update(course);
            await _context.SaveChangesAsync();

            return _mapper.Map<CourseViewModel>(course);
        }

        public async Task<IEnumerable<CourseViewModel>> GetCourses()
        {
            var courses = await _context.Courses.ToListAsync();

            return _mapper.Map<IEnumerable<CourseViewModel>>(courses);
        }

        public async Task<CourseViewModel> GetCourseById(int? id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(course => course.Id == id);

            return _mapper.Map<CourseViewModel>(course);
        }

        public async Task RemoveCourse(int id)
        {

            var course = await _context.Courses.FindAsync(id);

            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }

        public bool CourseExists(int id)
        {
            return (_context.Courses?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
