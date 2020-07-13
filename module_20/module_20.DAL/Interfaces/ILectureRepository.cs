using module_20.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace module_20.DAL.Interfaces
{
    /// <summary>
    /// Specifies the contract for a lecture repository
    /// </summary>
    public interface ILectureRepository : IRepository<Lecture>
    {
        /// <summary>
        /// Get the lectures with homeworks
        /// </summary>
        /// <returns>IQueryable collection of lectures with homeworks collection</returns>
        public IQueryable<Lecture> GetAllWithHomeworks();
        /// <summary>
        /// Get the lectures with course
        /// </summary>
        /// <returns>IQueryable collection of lectures with course object</returns>
        public IQueryable<Lecture> GetAllWithCourse();
        /// <summary>
        /// Get the lectures with students
        /// </summary>
        /// <returns>IQueryable collection of lectures with students collection</returns>
        public IQueryable<Lecture> GetAllWithStudents();
        /// <summary>
        /// Get the lecture with specified id with homeworks
        /// </summary>
        /// <param name="id">Id of lecture</param>
        /// <returns>Task with the lecture object as result</returns>
        public Task<Lecture> GetLectureWithHomeworksById(int id);
        /// <summary>
        /// Get the lecture with specified id with course
        /// </summary>
        /// <param name="id">Id of lecture</param>
        /// <returns>Task with the lecture object as result</returns>
        public Task<Lecture> GetLectureWithCourseById(int id);
        /// <summary>
        /// Get the lecture with specified id with lecture and students
        /// </summary>
        /// <param name="id">Id of lecture</param>
        /// <returns>Task with the lecture object as result</returns>
        public Task<Lecture> GetLectureWithStudentsById(int id);

    }
}
