using module_20.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace module_20.DAL.Interfaces
{
    /// <summary>
    /// Specifies the contract for a homework repository
    /// </summary>
    public interface IHomeworkRepository : IRepository<Homework>
    {
        /// <summary>
        /// Get the homeworks with lecture
        /// </summary>
        /// <returns>IQueryable collection of homeworks with lecture object</returns>
        public IQueryable<Homework> GetAllWithLecture();
        /// <summary>
        /// Get the homeworks with student
        /// </summary>
        /// <returns>IQueryable collection of homeworks with student object</returns>
        public IQueryable<Homework> GetAllWithStudent();
        /// <summary>
        /// Get the homeworks with lecture
        /// </summary>
        /// <returns>IQueryable collection of homeworks with student and lecture object</returns>
        public IQueryable<Homework> GetAllWithLectureAndStudent();
        /// <summary>
        /// Get the homework with specified id with lecture
        /// </summary>
        /// <param name="id">Id of homework</param>
        /// <returns>Task with the homework object as result</returns>
        public Task<Homework> GetHomeworkWithLectureById(int id);
        /// <summary>
        /// Get the homework with specified id with student
        /// </summary>
        /// <param name="id">Id of homework</param>
        /// <returns>Task with the homework object as result</returns>
        public Task<Homework> GetHomeworkWithStudentById(int id);
        /// <summary>
        /// Get the homework with specified id with lecture and student
        /// </summary>
        /// <param name="id">Id of homework</param>
        /// <returns>Task with the homework object as result</returns>
        public Task<Homework> GetHomeworkWithLectureAndStudentById(int id);
    }
}
