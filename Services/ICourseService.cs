using ServiceBasedApplication.ViewModels;

namespace ServiceBasedApplication.Services
{
    public interface ICourseService
    {
        //note we don't need async in the interface
        Task<CourseViewModel> AddCourse(CourseViewModel course);

        Task<CourseViewModel> UpdateCourse(CourseViewModel course);

        Task<IEnumerable<CourseViewModel>> GetCourses();

        Task<CourseViewModel> GetCourseById(int? CourseId);

        Task RemoveCourse(int CourseId);

        bool CourseExists(int id);
    }
}
