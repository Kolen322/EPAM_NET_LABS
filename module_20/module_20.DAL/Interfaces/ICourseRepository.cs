using module_20.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace module_20.DAL.Interfaces
{
    /// <summary>
    /// Specifies the contract for a course repository
    /// </summary>
    public interface ICourseRepository : IRepository<Course>
    {
        /// <summary>
        /// Get the courses with lecturer
        /// </summary>
        /// <returns>IQueryable collection of courses with lecturer object</returns>
        public IQueryable<Course> GetCoursesWithLecturer();
        /// <summary>
        /// Get the course with specified id with lecturer
        /// </summary>
        /// <param name="id">Id of course</param>
        /// <returns>Task with the courses object as result</returns>
        public Task<Course> GetCourseWithLecturerById(int id);
        /// <summary>
        /// Get the courses with students
        /// </summary>
        /// <returns>IQueryable collection of courses with students collection</returns>
        public IQueryable<Course> GetCoursesWithStudents();
        /// <summary>
        /// Get the course with specified id with students
        /// </summary>
        /// <param name="id">Id of course</param>
        /// <returns>Task with the courses object as result</returns>
        public Task<Course> GetCourseWithStudentsById(int id);
        /// <summary>
        /// Get the courses with lectures
        /// </summary>
        /// <returns>IQueryable collection of courses with lectures collection</returns>
        public IQueryable<Course> GetCoursesWithLectures();
        /// <summary>
        /// Get the course with specified id with lectures
        /// </summary>
        /// <param name="id">Id of course</param>
        /// <returns>Task with the course object as result</returns>
        public Task<Course> GetCourseWithLecturesById(int id);

    }
}
