using module_20.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace module_20.DAL.Interfaces
{
    /// <summary>
    /// Specifies the contract for a lecturer repository
    /// </summary>
    public interface ILecturerRepository : IRepository<Lecturer>
    {
        /// <summary>
        /// Get the lecturers with courses
        /// </summary>
        /// <returns>IQueryable collection of lectures with courses collection</returns>
        IQueryable<Lecturer> GetLecturersWithCourses();
        /// <summary>
        /// Get the lecturer with specified id with courses
        /// </summary>
        /// <param name="id">Id of lecturer</param>
        /// <returns>Task with the lecturer object as result</returns>
        Task<Lecturer> GetLecturerWithCoursesByIdAsync(int id);
    }
}
