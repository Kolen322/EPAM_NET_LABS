using module_20.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace module_20.BLL.Interfaces
{
    /// <summary>
    /// Specifies the contract for a course service
    /// </summary>
    public interface ICourseService
    {
        /// <summary>
        /// Get the courses
        /// </summary>
        /// <returns>IQueryable collection of courses</returns>
        IQueryable<Course> GetAllCourses();
        /// <summary>
        /// Get the courses with lecturer
        /// </summary>
        /// <returns>IQueryable collection of courses with lecturer object</returns>
        IQueryable<Course> GetAllCoursesWithLecturer();
        /// <summary>
        /// Get the courses with lectures
        /// </summary>
        /// <returns>IQueryable collection of courses with lectures collection</returns>
        IQueryable<Course> GetAllCoursesWithLectures();
        /// <summary>
        /// Get the course with specified id with students
        /// </summary>
        /// <param name="id">Id of course</param>
        /// <returns>Task with the courses object as result</returns>
        IQueryable<Course> GetAllCoursesWithStudents();
        /// <summary>
        /// Get the course with specified id
        /// </summary>
        /// <param name="id">Id of course</param>
        /// <returns>Task with the courses object as result</returns>
        Task<Course> GetCourseById(int id);
        /// <summary>
        /// Get the course with specified id with lecturer
        /// </summary>
        /// <param name="id">Id of course</param>
        /// <returns>Task with the courses object as result</returns>
        Task<Course> GetCourseWithLecturerById(int id);
        /// <summary>
        /// Get the course with specified id with lectures
        /// </summary>
        /// <param name="id">Id of course</param>
        /// <returns>Task with the course object as result</returns>
        Task<Course> GetCourseWithLecturesById(int id);
        /// <summary>
        /// Get the course with specified id with students
        /// </summary>
        /// <param name="id">Id of course</param>
        /// <returns>Task with the courses object as result</returns>
        Task<Course> GetCourseWithStudentsById(int id);
        /// <summary>
        /// Create a new course
        /// </summary>
        /// <param name="newCourse">New course that need to create</param>
        /// <returns>Task with new course object as a result</returns>
        Task<Course> CreateCourse(Course newCourse);
        /// <summary>
        /// Update a existing course
        /// </summary>
        /// <param name="course">The updated course</param>
        /// <returns>Task</returns>
        Task UpdateCourse(Course course);
        /// <summary>
        /// Delete a course
        /// </summary>
        /// <param name="course">The course that need to delete</param>
        /// <returns>Task</returns>
        Task DeleteCourse(Course course);
        /// <summary>
        /// Add student to the course
        /// </summary>
        /// <param name="courseId">Course Id</param>
        /// <param name="studentId">Student Id</param>
        /// <returns>Task</returns>
        Task AddStudentToCourse(int courseId, int studentId);
    }
}
