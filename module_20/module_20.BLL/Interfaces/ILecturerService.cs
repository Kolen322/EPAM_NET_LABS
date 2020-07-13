using module_20.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace module_20.BLL.Interfaces
{
    /// <summary>
    /// Specifies the contract for a lecturer service
    /// </summary>
    public interface ILecturerService
    {
        /// <summary>
        /// Get the lecturers
        /// </summary>
        /// <returns>IQueryable collection of lecturers</returns>
        IQueryable<Lecturer> GetAllLecturers();
        /// <summary>
        /// Get the lecturers with courses
        /// </summary>
        /// <returns>IQueryable collection of lectures with courses collection</returns>
        IQueryable<Lecturer> GetAllLecturersWithCourses();
        /// <summary>
        /// Get the lecturer with specified id
        /// </summary>
        /// <param name="id">Id of lecturer</param>
        /// <returns>Task with the lecturer object as result</returns>
        Task<Lecturer> GetLecturerById(int id);
        /// <summary>
        /// Get the lecturer with specified id with courses
        /// </summary>
        /// <param name="id">Id of lecturer</param>
        /// <returns>Task with the lecturer object as result</returns>
        Task<Lecturer> GetLecturerWithCoursesById(int id);
        /// <summary>
        /// Create a new lecturer
        /// </summary>
        /// <param name="newLecturer">New lecturer that need to create</param>
        /// <returns>Task with new lecturer object as a result</returns>
        Task<Lecturer> CreateLecturer(Lecturer newLecturer);
        /// <summary>
        /// Update a existing lecturer
        /// </summary>
        /// <param name="lecturer">The updated lecturer</param>
        /// <returns>Task</returns>
        Task UpdateLecturer(Lecturer lecturer);
        /// <summary>
        /// Delete a lecturer
        /// </summary>
        /// <param name="lecturer">The lecturer that need to delete</param>
        /// <returns>Task</returns>
        Task DeleteLecturer(Lecturer lecturer);
    }
}
