using System;
using System.Threading.Tasks;

namespace module_20.DAL.Interfaces
{
    /// <summary>
    /// Specifies the contract for a unit of work pattern
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Course repository
        /// </summary>
        ICourseRepository Courses { get; }
        /// <summary>
        /// Homework repository
        /// </summary>
        IHomeworkRepository Homeworks { get; }
        /// <summary>
        /// Lecture repository
        /// </summary>
        ILectureRepository Lectures { get; }
        /// <summary>
        /// Lecturer repository
        /// </summary>
        ILecturerRepository Lecturers { get; }
        /// <summary>
        /// Student repository
        /// </summary>
        IStudentRepository Students { get; }
        /// <summary>
        /// Save changes in repositories
        /// </summary>
        /// <returns>Task</returns>
        Task<int> SaveAsync();
        /// <summary>
        /// Save changes in repositories
        /// </summary>
        void Save();
    }
}
