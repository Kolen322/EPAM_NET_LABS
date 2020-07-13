using module_20.BLL.Interfaces;
using module_20.DAL.Entities;
using module_20.DAL.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using module_20.BLL.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace module_20.BLL.Services
{
    /// <summary>
    /// Class that executes the contract of a homework service
    /// </summary>
    public class HomeworkService : IHomeworkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeworkService> _logger;

        /// <summary>
        /// Constructor with specified unit of work and logger
        /// </summary>
        /// <param name="unitOfWork">Object that executes a unit of work pattern</param>
        /// <param name="logger">Object that execute a ILogger contract</param>
        public HomeworkService(IUnitOfWork unitOfWork, ILogger<HomeworkService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger;
        }

        /// <summary>
        /// Check the student average grade for the course and send message, if it lower than four
        /// </summary>
        /// <param name="studentId">Student Id</param>
        /// <param name="courseId">Course Id</param>
        /// <param name="messageSenderService">Message sender</param>
        /// <returns>Task</returns>
        public async Task CheckAverageMark(int studentId, int courseId, IMessageSenderService messageSenderService)
        {
            if (messageSenderService == null)
                throw new EntityNullException(nameof(messageSenderService));
            var student = await _unitOfWork.Students.GetByIdAsync(studentId);
            if (student == null)
                throw new EntityNotFoundException($"Student with {studentId} id doesn't exist in database");
            var course = await _unitOfWork.Courses.GetByIdAsync(courseId);
            if (student == null)
                throw new EntityNotFoundException($"Course with {courseId} id doesn't exist in database");

            var averageMark = await GetAverageMark(studentId, courseId);

            if (averageMark < 4)
            {
                _logger.LogInformation(DateTime.Now
                    + $"\nStudent's mark with {studentId} id is lower than 4 at course {course.Name}, sending message to student");
                messageSenderService.Send($"Your {course.Name}'s mark is lower than 4", student.Mobile);
            }
        }

        /// <summary>
        /// Create a new homework
        /// </summary>
        /// <param name="newHomework">New homework that need to create</param>
        /// <returns>Task with new homework object as a result</returns>
        /// <exception cref="EntityNullException">If argument is null</exception>
        /// <exception cref="EntityNotFoundException">If object has a foreign keys and objects with that ids doesn't exist in database</exception>
        /// <exception cref="EntityAlreadyExistException">If object with that id already exist in database</exception>
        public async Task<Homework> CreateHomework(Homework newHomework)
        {
            if (newHomework == null)
                throw new EntityNullException(nameof(newHomework));
            var student = await _unitOfWork.Students.GetByIdAsync(newHomework.StudentId);
            if (student == null)
                throw new EntityNotFoundException($"Student with {newHomework.StudentId} id doesn't exist in database");
            var lecture = await _unitOfWork.Lectures.GetByIdAsync(newHomework.LectureId);
            if (lecture == null)
                throw new EntityNotFoundException($"Lecture with {newHomework.LectureId} id doesn't exist in database");
            if (await _unitOfWork.Homeworks.Contains(newHomework))
                throw new EntityAlreadyExistException($"Homework with {newHomework.Id} already exist in database");
            var result = await _unitOfWork.Homeworks.AddAsync(newHomework);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation(DateTime.Now + $"\nCreating homework is successful");
            return result;
        }

        /// <summary>
        /// Delete a homework
        /// </summary>
        /// <param name="course">The homework that need to delete</param>
        /// <returns>Task</returns>
        /// <exception cref="EntityNullException">If argument is null</exception>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception> 
        public async Task DeleteHomework(Homework homework)
        {
            if (homework == null)
            {
                throw new EntityNullException(nameof(homework));
            }
            if (await _unitOfWork.Homeworks.Contains(homework) == false)
            {
                throw new EntityNotFoundException($"Homework with {homework.Id} id doesn't exist in database");
            }
            _unitOfWork.Homeworks.Remove(homework);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation(DateTime.Now + $"\nHomework with {homework.Id} id deleted from database");
        }

        /// <summary>
        /// Get the homeworks
        /// </summary>
        /// <returns>IQueryable collection of homeworks</returns>
        public IQueryable<Homework> GetAllHomeworks()
        {
            _logger.LogInformation(DateTime.Now + $"\nGet homeworks");
            return _unitOfWork.Homeworks.GetAll();
        }

        /// <summary>
        /// Get the homeworks with lecture
        /// </summary>
        /// <returns>IQueryable collection of homeworks with lecture object</returns>
        public IQueryable<Homework> GetAllHomeworksWithLecture()
        {
            _logger.LogInformation(DateTime.Now + $"\nGet homeworks with lecture");
            return _unitOfWork.Homeworks.GetAllWithLecture();
        }

        /// <summary>
        /// Get the homeworks with lecture
        /// </summary>
        /// <returns>IQueryable collection of homeworks with student and lecture object</returns>
        public IQueryable<Homework> GetAllHomeworksWithLectureAndStudent()
        {
            _logger.LogInformation(DateTime.Now + $"\nGet homeworks with lecture and student");
            return _unitOfWork.Homeworks.GetAllWithLectureAndStudent();
        }

        /// <summary>
        /// Get the homeworks with student
        /// </summary>
        /// <returns>IQueryable collection of homeworks with student object</returns>
        public IQueryable<Homework> GetAllHomeworksWithStudent()
        {
            _logger.LogInformation(DateTime.Now + $"\nGet homeworks with student");
            return _unitOfWork.Homeworks.GetAllWithStudent();
        }

        /// <summary>
        /// Get the student average grade for the course
        /// </summary>
        /// <param name="studentId">Student id</param>
        /// <param name="courseId">Course Id</param>
        /// <returns>Task with double number as result</returns>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception> 
        public async Task<double> GetAverageMark(int studentId, int courseId)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(studentId);
            if (student == null)
                throw new EntityNotFoundException($"Student with {studentId} id doesn't exist in database");
            var course = await _unitOfWork.Courses.GetByIdAsync(courseId);
            if (course == null)
                throw new EntityNotFoundException($"Course with {courseId} id doesn't exist in database");
            var homeworks = await _unitOfWork.Homeworks
                .GetAllWithLecture()
                .Where(h => h.StudentId == studentId)
                .Where(c => c.Lecture.CourseId == courseId)
                .ToListAsync();
            double countOfMarks = 0;
            double sumOfMarks = 0;
            foreach (var homework in homeworks)
            {
                sumOfMarks += homework.Mark;
                countOfMarks++;
            }
            var averageMark = sumOfMarks / countOfMarks;
            _logger.LogInformation(DateTime.Now + $"\nGet average mark of student with {studentId} id at course with {courseId} id, it's {averageMark}");
            return averageMark;
        }

        /// <summary>
        /// Get the homework with specified id
        /// </summary>
        /// <param name="id">Id of homework</param>
        /// <returns>Task with the homework object as result</returns>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        public async Task<Homework> GetHomeworkById(int id)
        {
            var homework = await _unitOfWork.Homeworks.GetByIdAsync(id);
            if (homework == null)
                throw new EntityNotFoundException($"Homework with {id} id doesn't exist in database");
            _logger.LogInformation(DateTime.Now + $"\nGet homework with {id} id");
            return homework;
        }

        /// <summary>
        /// Get the homework with specified id with lecture and student
        /// </summary>
        /// <param name="id">Id of homework</param>
        /// <returns>Task with the homework object as result</returns>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        public async Task<Homework> GetHomeworkWithLectureAndStudentById(int id)
        {
            var homework = await _unitOfWork.Homeworks.GetHomeworkWithLectureAndStudentById(id);
            if (homework == null)
                throw new EntityNotFoundException($"Homework with {id} id doesn't exist in database");
            _logger.LogInformation(DateTime.Now + $"\nGet homework with {id} id with lecture and student");
            return homework;
        }

        /// <summary>
        /// Get the homework with specified id with lecture
        /// </summary>
        /// <param name="id">Id of homework</param>
        /// <returns>Task with the homework object as result</returns>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        public async Task<Homework> GetHomeworkWithLectureById(int id)
        {
            var homework = await _unitOfWork.Homeworks.GetHomeworkWithLectureById(id);
            if (homework == null)
                throw new EntityNotFoundException($"Homework with {id} id doesn't exist in database");
            _logger.LogInformation(DateTime.Now + $"\nGet homework with {id} id with lecture");
            return homework;
        }

        /// <summary>
        /// Get the homework with specified id with student
        /// </summary>
        /// <param name="id">Id of homework</param>
        /// <returns>Task with the homework object as result</returns>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        public async Task<Homework> GetHomeworkWithStudentById(int id)
        {
            var homework = await _unitOfWork.Homeworks.GetHomeworkWithStudentById(id);
            if (homework == null)
                throw new EntityNotFoundException($"Homework with {id} id doesn't exist in database");
            _logger.LogInformation(DateTime.Now + $"\nGet homework with {id} id with student");
            return homework;
        }

        /// <summary>
        /// Update a existing homework
        /// </summary>
        /// <param name="course">The updated homework</param>
        /// <returns>Task</returns>
        /// <exception cref="EntityNullException">If argument is null</exception>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database or object has a foreign keys and objects with that ids doesn't exist in database or </exception>
        public async Task UpdateHomework(Homework homework)
        {
            if (homework == null)
                throw new EntityNullException(nameof(homework));
            var student = await _unitOfWork.Students.GetByIdAsync(homework.StudentId);
            if (student == null)
                throw new EntityNotFoundException($"Student with {homework.StudentId} id doesn't exist in database");
            var lecture = await _unitOfWork.Lectures.GetByIdAsync(homework.LectureId);
            if (lecture == null)
                throw new EntityNotFoundException($"Lecture with {homework.LectureId} id doesn't exist in database");
            var oldEntity = await _unitOfWork.Homeworks.GetByIdAsync(homework.Id);
            if(oldEntity != null)
            {
                _unitOfWork.Homeworks.Update(homework);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                throw new EntityNotFoundException($"Homework with {homework.Id} doesn't exist in database");
            }
            _logger.LogInformation(DateTime.Now + $"\nUpdating homework with {homework.Id} id is successful");
        }
    }
}
