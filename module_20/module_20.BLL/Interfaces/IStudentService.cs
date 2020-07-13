using module_20.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace module_20.BLL.Interfaces
{
    /// <summary>
    /// Specifies the contract for a student service
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// Get the students
        /// </summary>
        /// <returns>IQueryable collection of students</returns>
        IQueryable<Student> GetAllStudents();
        /// <summary>
        /// Get the students with courses
        /// </summary>
        /// <returns>IQueryable collection of students with courses collection</returns>
        IQueryable<Student> GetAllStudentsWithCourses();
        /// <summary>
        /// Get the students with lectures
        /// </summary>
        /// <returns>IQueryable collection of students with lectures collection</returns>
        IQueryable<Student> GetAllStudentsWithLectures();
        /// <summary>
        /// Get the students with courses and lectures
        /// </summary>
        /// <returns>IQueryable collection of students with lectures and courses collection</returns>
        IQueryable<Student> GetAllStudentsWithLecturesAndCourses();
        /// <summary>
        /// Get the student with specified id
        /// </summary>
        /// <param name="id">Id of student</param>
        /// <returns>Task with the student object as result</returns>
        Task<Student> GetStudentById(int id);
        /// <summary>
        /// Get the student with specified id with courses
        /// </summary>
        /// <param name="id">Id of student</param>
        /// <returns>Task with the student object as result</returns>
        Task<Student> GetStudentWithCoursesById(int id);
        /// <summary>
        /// Get the student with specified id with lectures
        /// </summary>
        /// <param name="id">Id of student</param>
        /// <returns>Task with the student object as result</returns>
        Task<Student> GetStudentWithLecturesById(int id);
        /// <summary>
        /// Get the student with specified id with courses and lectures
        /// </summary>
        /// <param name="id">Id of student</param>
        /// <returns>Task with the student object as result</returns>
        Task<Student> GetStudentWithLecturesAndCoursesById(int id);
        /// <summary>
        /// Create a new student
        /// </summary>
        /// <param name="newStudent">New student that need to create</param>
        /// <returns>Task with new student object as a result</returns>
        Task<Student> CreateStudent(Student newStudent);
        /// <summary>
        /// Update a existing student
        /// </summary>
        /// <param name="student">The updated student</param>
        /// <returns>Task</returns>
        Task UpdateStudent(Student student);
        /// <summary>
        /// Delete a student
        /// </summary>
        /// <param name="student">The student that need to delete</param>
        /// <returns>Task</returns>
        Task DeleteStudent(Student student);
    }
}
