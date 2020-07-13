using module_20.BLL.Interfaces;
using module_20.DAL.Entities;
using module_20.DAL.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using module_20.BLL.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;

namespace module_20.BLL.Services
{
    /// <summary>
    /// Class that executes the contract of a lecturer service
    /// </summary>
    public class LecturerService : ILecturerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LecturerService> _logger;

        /// <summary>
        /// Constructor with specified unit of work and logger
        /// </summary>
        /// <param name="unitOfWork">Object that executes a unit of work pattern</param>
        /// <param name="logger">Object that execute a ILogger contract</param>
        public LecturerService(IUnitOfWork unitOfWork, ILogger<LecturerService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger;
        }

        /// <summary>
        /// Create a new lecturer
        /// </summary>
        /// <param name="newLecturer">New lecturer that need to create</param>
        /// <returns>Task with new lecturer object as a result</returns>
        /// <exception cref="EntityNullException">If argument is null</exception>
        /// <exception cref="EntityNotFoundException">If object has a foreign keys and objects with that ids doesn't exist in database</exception>
        /// <exception cref="EntityAlreadyExistException">If object with that id already exist in database</exception>
        public async Task<Lecturer> CreateLecturer(Lecturer newLecturer)
        {
            if (newLecturer == null)
                throw new EntityNullException(nameof(newLecturer));
            if (await _unitOfWork.Lecturers.Contains(newLecturer))
                throw new EntityAlreadyExistException($"Lecturer with {newLecturer.Id} id already exist in database");
            var result = await _unitOfWork.Lecturers.AddAsync(newLecturer);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation(DateTime.Now + $"\nCreating lecturer is successful");
            return result;
        }

        /// <summary>
        /// Delete a lecturer
        /// </summary>
        /// <param name="lecturer">The lecturer that need to delete</param>
        /// <returns>Task</returns>
        /// <exception cref="EntityNullException">If argument is null</exception>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception> 
        public async Task DeleteLecturer(Lecturer lecturer)
        {
            if (lecturer == null)
            {
                throw new EntityNullException(nameof(lecturer));
            }
            if (await _unitOfWork.Lecturers.Contains(lecturer) == false)
            {
                throw new EntityNotFoundException($"Lecturer with {lecturer.Id} id doesn't exist in database");
            }
            _unitOfWork.Lecturers.Remove(lecturer);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation(DateTime.Now + $"\nLecturer with {lecturer.Id} id deleted from database");
        }

        /// <summary>
        /// Get the lecturers
        /// </summary>
        /// <returns>IQueryable collection of lecturers</returns>
        public IQueryable<Lecturer> GetAllLecturers()
        {
            _logger.LogInformation(DateTime.Now + $"\nGet lecturers");
            return _unitOfWork.Lecturers.GetAll();
        }

        /// <summary>
        /// Get the lecturers with courses
        /// </summary>
        /// <returns>IQueryable collection of lectures with courses collection</returns>
        public IQueryable<Lecturer> GetAllLecturersWithCourses()
        {
            _logger.LogInformation(DateTime.Now + $"\nGet lecturers with courses");
            return _unitOfWork.Lecturers.GetLecturersWithCourses();
        }

        /// <summary>
        /// Get the lecturer with specified id
        /// </summary>
        /// <param name="id">Id of lecturer</param>
        /// <returns>Task with the lecturer object as result</returns>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        public async Task<Lecturer> GetLecturerById(int id)
        {
            var lecturer = await _unitOfWork.Lecturers.GetByIdAsync(id);
            if (lecturer == null)
                throw new EntityNotFoundException($"Lecturer with {id} id doesn't exist in database");
            _logger.LogInformation(DateTime.Now + $"\nGet lecturer with {id} id");
            return lecturer;
        }

        /// <summary>
        /// Get the lecturer with specified id with courses
        /// </summary>
        /// <param name="id">Id of lecturer</param>
        /// <returns>Task with the lecturer object as result</returns>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        public async Task<Lecturer> GetLecturerWithCoursesById(int id)
        {
            var lecturer = await _unitOfWork.Lecturers.GetLecturerWithCoursesByIdAsync(id);
            if (lecturer == null)
                throw new EntityNotFoundException($"Lecturer with {id} id doesn't exist in database");
            _logger.LogInformation(DateTime.Now + $"\nGet lecturer with {id} id with courses");
            return lecturer;
        }

        /// <summary>
        /// Update a existing lecturer
        /// </summary>
        /// <param name="lecturer">The updated lecturer</param>
        /// <returns>Task</returns>
        /// <exception cref="EntityNullException">If argument is null</exception>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database or object has a foreign keys and objects with that ids doesn't exist in database or </exception>
        public async Task UpdateLecturer(Lecturer lecturer)
        {
            if (lecturer == null)
                throw new EntityNullException(nameof(lecturer));
            var oldLecturer = await _unitOfWork.Lecturers.GetByIdAsync(lecturer.Id);
            if(oldLecturer != null)
            {
                _unitOfWork.Lecturers.Update(lecturer);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                throw new EntityNotFoundException($"Lecturer with {lecturer.Id} id doesn't exist in database");
            }
            _logger.LogInformation(DateTime.Now + $"\nUpdating lecturer with {lecturer.Id} id is successful");
        }
    }
}
