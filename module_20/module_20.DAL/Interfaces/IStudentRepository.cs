using module_20.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace module_20.DAL.Interfaces
{
    /// <summary>
    /// Specifies the contract for a student repository
    /// </summary>
    public interface IStudentRepository : IRepository<Student>
    {
        /// <summary>
        /// Get the students with courses
        /// </summary>
        /// <returns>IQueryable collection of students with courses collection</returns>
        IQueryable<Student> GetAllWithCourses();
        /// <summary>
        /// Get the students with lectures
        /// </summary>
        /// <returns>IQueryable collection of students with lectures collection</returns>
        IQueryable<Student> GetAllWithLectures();
        /// <summary>
        /// Get the students with courses and lectures
        /// </summary>
        /// <returns>IQueryable collection of students with lectures and courses collection</returns>
        IQueryable<Student> GetAllWithLecturesAndCourses();
        /// <summary>
        /// Get the student with specified id with courses
        /// </summary>
        /// <param name="id">Id of student</param>
        /// <returns>Task with the student object as result</returns>
        Task<Student> GetWithCoursesByIdAsync(int id);
        /// <summary>
        /// Get the student with specified id with lectures
        /// </summary>
        /// <param name="id">Id of student</param>
        /// <returns>Task with the student object as result</returns>
        Task<Student> GetWithLecturesByIdAsync(int id);
        /// <summary>
        /// Get the student with specified id with courses and lectures
        /// </summary>
        /// <param name="id">Id of student</param>
        /// <returns>Task with the student object as result</returns>
        Task<Student> GetWithLecturesAndCoursesByIdAsync(int id);

    }
}
