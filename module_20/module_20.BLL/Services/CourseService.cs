using module_20.BLL.Interfaces;
using module_20.DAL.Entities;
using module_20.DAL.Interfaces;
using System.Threading.Tasks;
using System;
using System.Linq;
using module_20.BLL.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;

namespace module_20.BLL.Services
{
    /// <summary>
    /// Class that executes the contract of a course service
    /// </summary>
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CourseService> _logger;

        /// <summary>
        /// Constructor with specified unit of work and logger
        /// </summary>
        /// <param name="unitOfWork">Object that executes a unit of work pattern</param>
        /// <param name="logger">Object that execute a ILogger contract</param>
        public CourseService(IUnitOfWork unitOfWork, ILogger<CourseService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger;
        }

        /// <summary>
        /// Add student to the course
        /// </summary>
        /// <param name="courseId">Course Id</param>
        /// <param name="studentId">Student Id</param>
        /// <returns>Task</returns>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        /// <exception cref="EntityAlreadyExistException">If object with that id already exist in database</exception>
        public async Task AddStudentToCourse(int courseId, int studentId)
        {
            var course = await _unitOfWork.Courses.GetCourseWithStudentsById(courseId);
            if (course == null)
            {
                throw new EntityNotFoundException($"Course with {courseId} id doesn't exist in database");
            }
            var student = await _unitOfWork.Students.GetByIdAsync(studentId);
            if (student == null)
            {
                throw new EntityNotFoundException($"Student with {studentId} id doesn't exist in database");
            }
            StudentCourse studentCourse = new StudentCourse { CourseId = courseId, StudentId = studentId };
            if (course.StudentCourses.Contains(studentCourse))
                throw new EntityAlreadyExistException();
            course.StudentCourses.Add(studentCourse);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation(DateTime.Now + $"\nStudent with {studentId} id added in course with {courseId} id");
        }

        /// <summary>
        /// Create a new course
        /// </summary>
        /// <param name="newCourse">New course that need to create</param>
        /// <returns>Task with new course object as a result</returns>
        /// <exception cref="EntityNullException">If argument is null</exception>
        /// <exception cref="EntityNotFoundException">If object has a foreign keys and objects with that ids doesn't exist in database</exception>
        /// <exception cref="EntityAlreadyExistException">If object with that id already exist in database</exception>
        public async Task<Course> CreateCourse(Course newCourse)
        {
            if (newCourse == null)
                throw new EntityNullException(nameof(newCourse));
            var lecturer = await _unitOfWork.Lecturers.GetByIdAsync(newCourse.LecturerId);
            if (lecturer == null)
                throw new EntityNotFoundException($"Lecturer with {newCourse.LecturerId} id doesn't exist in database");
            if (await _unitOfWork.Courses.Contains(newCourse))
                throw new EntityAlreadyExistException($"Lecturer with {newCourse.Id} already exist in database");
            var result = await _unitOfWork.Courses.AddAsync(newCourse);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation(DateTime.Now + "\nCourse creation is successful");
            return result;
        }

        /// <summary>
        /// Delete a course
        /// </summary>
        /// <param name="course">The course that need to delete</param>
        /// <returns>Task</returns>
        /// <exception cref="EntityNullException">If argument is null</exception>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        public async Task DeleteCourse(Course course)
        {
            if (course == null)
                throw new EntityNullException(nameof(course));
            if (await _unitOfWork.Courses.Contains(course) == false)
                throw new EntityNotFoundException();
            _unitOfWork.Courses.Remove(course);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation(DateTime.Now + $"\nCourse with {course.Id} id deleted from database");
        }

        /// <summary>
        /// Get the courses
        /// </summary>
        /// <returns>IQueryable collection of courses</returns>
        public IQueryable<Course> GetAllCourses()
        {
            _logger.LogInformation(DateTime.Now + "\nGet courses");
            return _unitOfWork.Courses.GetAll();
        }

        /// <summary>
        /// Get the courses with lecturer
        /// </summary>
        /// <returns>IQueryable collection of courses with lecturer object</returns>
        public IQueryable<Course> GetAllCoursesWithLecturer()
        {
            _logger.LogInformation(DateTime.Now + "\nGet courses with lecturers");
            return _unitOfWork.Courses.GetCoursesWithLecturer();
        }

        /// <summary>
        /// Get the courses with lectures
        /// </summary>
        /// <returns>IQueryable collection of courses with lectures collection</returns>
        public IQueryable<Course> GetAllCoursesWithLectures()
        {
            _logger.LogInformation(DateTime.Now + "\nGet courses with lectures");
            return _unitOfWork.Courses.GetCoursesWithLectures();
        }

        /// <summary>
        /// Get the course with specified id with students
        /// </summary>
        /// <param name="id">Id of course</param>
        /// <returns>Task with the courses object as result</returns>
        public IQueryable<Course> GetAllCoursesWithStudents()
        {
            _logger.LogInformation(DateTime.Now + "\nGet courses with students");
            return _unitOfWork.Courses.GetCoursesWithStudents();
        }

        /// <summary>
        /// Get the course with specified id
        /// </summary>
        /// <param name="id">Id of course</param>
        /// <returns>Task with the courses object as result</returns>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        public async Task<Course> GetCourseById(int id)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);
            if (course == null)
                throw new EntityNotFoundException($"Course with {id} id doesn't exist in database");
            _logger.LogInformation(DateTime.Now + $"\nnGet course with {id} id");
            return course;
        }

        /// <summary>
        /// Get the course with specified id with lecturer
        /// </summary>
        /// <param name="id">Id of course</param>
        /// <returns>Task with the courses object as result</returns>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        public async Task<Course> GetCourseWithLecturerById(int id)
        {
            var course = await _unitOfWork.Courses.GetCourseWithLecturerById(id);
            if (course == null)
                throw new EntityNotFoundException($"Course with {id} id doesn't exist in database");
            _logger.LogInformation(DateTime.Now + $"\nGet course with {id} id with lecturer");
            return course;
        }

        /// <summary>
        /// Get the course with specified id with lectures
        /// </summary>
        /// <param name="id">Id of course</param>
        /// <returns>Task with the course object as result</returns>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        public async Task<Course> GetCourseWithLecturesById(int id)
        {
            var course = await _unitOfWork.Courses.GetCourseWithLecturesById(id);
            if (course == null)
                throw new EntityNotFoundException($"Course with {id} id doesn't exist in database");
            _logger.LogInformation(DateTime.Now + $"\nGet course with {id} id with lectures");
            return course;
        }

        /// <summary>
        /// Get the course with specified id with students
        /// </summary>
        /// <param name="id">Id of course</param>
        /// <returns>Task with the courses object as result</returns>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        public async Task<Course> GetCourseWithStudentsById(int id)
        {
            var course = await _unitOfWork.Courses.GetCourseWithStudentsById(id);
            if (course == null)
                throw new EntityNotFoundException($"Course with {id} id doesn't exist in database");
            _logger.LogInformation(DateTime.Now + $"\nGet course with {id} id with students");
            return course;
        }

        /// <summary>
        /// Update a existing course
        /// </summary>
        /// <param name="course">The updated course</param>
        /// <returns>Task</returns>
        /// <exception cref="EntityNullException">If argument is null</exception>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database or object has a foreign keys and objects with that ids doesn't exist in database or </exception>
        public async Task UpdateCourse(Course course)
        {
            if (course == null)
                throw new EntityNullException(nameof(course));
            var oldEntity = await _unitOfWork.Courses.GetByIdAsync(course.Id);
            var lecturerOfCourse = await _unitOfWork.Lecturers.GetByIdAsync(course.LecturerId);
            if (lecturerOfCourse == null)
                throw new EntityNotFoundException($"Lecturer with {course.LecturerId} id doesn't exist in database");
            if(oldEntity!=null)
            {
                _unitOfWork.Courses.Update(course);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                throw new EntityNotFoundException($"Course with {course.Id} id doesn't exist in database");
            }
            _logger.LogInformation(DateTime.Now + $"\nUpdating course with {course.Id} id is successful");
        }
    }
}
