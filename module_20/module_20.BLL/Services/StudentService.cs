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
    /// Class that executes the contract of a student service
    /// </summary>
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<StudentService> _logger;

        /// <summary>
        /// Constructor with specified unit of work and logger
        /// </summary>
        /// <param name="unitOfWork">Object that executes a unit of work pattern</param>
        /// <param name="logger">Object that execute a ILogger contract</param>
        public StudentService(IUnitOfWork unitOfWork, ILogger<StudentService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger;
        }

        /// <summary>
        /// Create a new student
        /// </summary>
        /// <param name="newStudent">New student that need to create</param>
        /// <returns>Task with new student object as a result</returns>
        /// <exception cref="EntityNullException">If argument is null</exception>
        /// <exception cref="EntityNotFoundException">If object has a foreign keys and objects with that ids doesn't exist in database</exception>
        /// <exception cref="EntityAlreadyExistException">If object with that id already exist in database</exception>
        public async Task<Student> CreateStudent(Student newStudent)
        {
            if (newStudent == null)
                throw new EntityNullException(nameof(newStudent));
            if (await _unitOfWork.Students.Contains(newStudent))
                throw new EntityAlreadyExistException($"Student with {newStudent.Id} id already exist in database");
            var result = await _unitOfWork.Students.AddAsync(newStudent);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation(DateTime.Now + $"\nCreating student is successful");
            return result;
        }

        /// <summary>
        /// Delete a student
        /// </summary>
        /// <param name="student">The student that need to delete</param>
        /// <returns>Task</returns>
        /// <exception cref="EntityNullException">If argument is null</exception>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception> 
        public async Task DeleteStudent(Student student)
        {
            if (student == null)
                throw new EntityNullException(nameof(student));
            if (await _unitOfWork.Students.Contains(student) == false)
            {
                throw new EntityNotFoundException();
            }
            _unitOfWork.Students.Remove(student);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation(DateTime.Now + $"\nStudent with {student.Id} id deleted from database");
        }

        /// <summary>
        /// Get the students
        /// </summary>
        /// <returns>IQueryable collection of students</returns>
        public IQueryable<Student> GetAllStudents()
        {
            _logger.LogInformation(DateTime.Now + $"\nGet students");
            return _unitOfWork.Students.GetAll();
        }

        /// <summary>
        /// Get the students with courses
        /// </summary>
        /// <returns>IQueryable collection of students with courses collection</returns>
        public IQueryable<Student> GetAllStudentsWithCourses()
        {
            _logger.LogInformation(DateTime.Now + $"\nGet students with courses");
            return _unitOfWork.Students.GetAllWithCourses();
        }

        /// <summary>
        /// Get the students with lectures
        /// </summary>
        /// <returns>IQueryable collection of students with lectures collection</returns>
        public IQueryable<Student> GetAllStudentsWithLectures()
        {
            _logger.LogInformation(DateTime.Now + $"\nGet students with lectures");
            return _unitOfWork.Students.GetAllWithLectures();
        }

        /// <summary>
        /// Get the students with courses and lectures
        /// </summary>
        /// <returns>IQueryable collection of students with lectures and courses collection</returns>
        public IQueryable<Student> GetAllStudentsWithLecturesAndCourses()
        {
            _logger.LogInformation(DateTime.Now + $"\nGet students with courses and lectures");
            return _unitOfWork.Students.GetAllWithLecturesAndCourses();
        }

        /// <summary>
        /// Get the student with specified id
        /// </summary>
        /// <param name="id">Id of student</param>
        /// <returns>Task with the student object as result</returns>
        public async Task<Student> GetStudentById(int id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            if (student == null)
                throw new EntityNotFoundException($"Student with {id} id doesn't exist in database");
            _logger.LogInformation(DateTime.Now + $"\nGet student with {id} id");
            return student;
        }

        /// <summary>
        /// Get the student with specified id with courses
        /// </summary>
        /// <param name="id">Id of student</param>
        /// <returns>Task with the student object as result</returns>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        public async Task<Student> GetStudentWithCoursesById(int id)
        {
            var student = await _unitOfWork.Students.GetWithCoursesByIdAsync(id);
            if (student == null)
                throw new EntityNotFoundException($"Student with {id} id doesn't exist in database");
            _logger.LogInformation(DateTime.Now + $"\nGet student with {id} id with courses");
            return student;
        }

        /// <summary>
        /// Get the student with specified id with lectures
        /// </summary>
        /// <param name="id">Id of student</param>
        /// <returns>Task with the student object as result</returns>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        public async Task<Student> GetStudentWithLecturesById(int id)
        {
            var student = await _unitOfWork.Students.GetWithLecturesByIdAsync(id);
            if (student == null)
                throw new EntityNotFoundException($"Student with {id} id doesn't exist in database");
            _logger.LogInformation(DateTime.Now + $"\nGet student with {id} id with lectures");
            return student;
        }

        /// <summary>
        /// Get the student with specified id with courses and lectures
        /// </summary>
        /// <param name="id">Id of student</param>
        /// <returns>Task with the student object as result</returns>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        public async Task<Student> GetStudentWithLecturesAndCoursesById(int id)
        {
            var student = await _unitOfWork.Students.GetWithLecturesAndCoursesByIdAsync(id);
            if (student == null)
                throw new EntityNotFoundException($"Student with {id} id doesn't exist in database");
            _logger.LogInformation(DateTime.Now + $"\nGet student with {id} id with courses and lectures");
            return student;
        }

        /// <summary>
        /// Update a existing student
        /// </summary>
        /// <param name="student">The updated student</param>
        /// <returns>Task</returns>
        /// <exception cref="EntityNullException">If argument is null</exception>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database or object has a foreign keys and objects with that ids doesn't exist in database or </exception>
        public async Task UpdateStudent(Student student)
        {
            if (student == null)
                throw new EntityNullException(nameof(student));
            var oldStudent = await _unitOfWork.Students.GetByIdAsync(student.Id);
            if (oldStudent == null)
                throw new EntityNotFoundException($"Student with {student.Id} id doesn't exist in database");
           _unitOfWork.Students.Update(student);
           await _unitOfWork.SaveAsync();
            _logger.LogInformation(DateTime.Now + $"\nUpdating student with {student.Id} id is successful");
        }
    }
}
