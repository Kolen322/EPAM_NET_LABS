using module_20.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace module_20.BLL.Interfaces
{
    /// <summary>
    /// Specifies the contract for a homework service
    /// </summary>
    public interface IHomeworkService
    {
        /// <summary>
        /// Get the homeworks
        /// </summary>
        /// <returns>IQueryable collection of homeworks</returns>
        IQueryable<Homework> GetAllHomeworks();
        /// <summary>
        /// Get the homeworks with lecture
        /// </summary>
        /// <returns>IQueryable collection of homeworks with lecture object</returns>
        IQueryable<Homework> GetAllHomeworksWithLecture();
        /// <summary>
        /// Get the homeworks with student
        /// </summary>
        /// <returns>IQueryable collection of homeworks with student object</returns>
        IQueryable<Homework> GetAllHomeworksWithStudent();
        /// <summary>
        /// Get the homeworks with lecture
        /// </summary>
        /// <returns>IQueryable collection of homeworks with student and lecture object</returns>
        IQueryable<Homework> GetAllHomeworksWithLectureAndStudent();
        /// <summary>
        /// Get the homework with specified id
        /// </summary>
        /// <param name="id">Id of homework</param>
        /// <returns>Task with the homework object as result</returns>
        Task<Homework> GetHomeworkById(int id);
        /// <summary>
        /// Get the homework with specified id with lecture
        /// </summary>
        /// <param name="id">Id of homework</param>
        /// <returns>Task with the homework object as result</returns>
        Task<Homework> GetHomeworkWithLectureById(int id);
        /// <summary>
        /// Get the homework with specified id with student
        /// </summary>
        /// <param name="id">Id of homework</param>
        /// <returns>Task with the homework object as result</returns>
        Task<Homework> GetHomeworkWithStudentById(int id);
        /// <summary>
        /// Get the homework with specified id with lecture and student
        /// </summary>
        /// <param name="id">Id of homework</param>
        /// <returns>Task with the homework object as result</returns>
        Task<Homework> GetHomeworkWithLectureAndStudentById(int id);
        /// <summary>
        /// Create a new homework
        /// </summary>
        /// <param name="newHomework">New homework that need to create</param>
        /// <returns>Task with new homework object as a result</returns>
        Task<Homework> CreateHomework(Homework newHomework);
        /// <summary>
        /// Update a existing homework
        /// </summary>
        /// <param name="course">The updated homework</param>
        /// <returns>Task</returns>
        Task UpdateHomework(Homework homework);
        /// <summary>
        /// Delete a homework
        /// </summary>
        /// <param name="course">The homework that need to delete</param>
        /// <returns>Task</returns>
        Task DeleteHomework(Homework homework);
        /// <summary>
        /// Get the student average grade for the course
        /// </summary>
        /// <param name="studentId">Student id</param>
        /// <param name="courseId">Course Id</param>
        /// <returns>Task with double number as result</returns>
        Task<double> GetAverageMark(int studentId, int courseId);
        /// <summary>
        /// Check the student average grade for the course and send message, if it lower than four
        /// </summary>
        /// <param name="studentId">Student Id</param>
        /// <param name="courseId">Course Id</param>
        /// <param name="messageSenderService">Message sender</param>
        /// <returns>Task</returns>
        Task CheckAverageMark(int studentId, int courseId, IMessageSenderService messageSenderService);
    }
}
