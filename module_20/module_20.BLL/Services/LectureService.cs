using module_20.BLL.Interfaces;
using module_20.DAL.Entities;
using module_20.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using module_20.BLL.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;

namespace module_20.BLL.Services
{
    /// <summary>
    /// Class that executes the contract of a lecture service
    /// </summary>
    public class LectureService : ILectureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LectureService> _logger;

        /// <summary>
        /// Constructor with specified unit of work and logger
        /// </summary>
        /// <param name="unitOfWork">Object that executes a unit of work pattern</param>
        /// <param name="logger">Object that execute a ILogger contract</param>
        public LectureService(IUnitOfWork unitOfWork, ILogger<LectureService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger;
        }

        /// <summary>
        /// Add homework to all students at the course of lecture
        /// </summary>
        /// <param name="lectureId">Lecture Id</param>
        /// <param name="task">Task of homework</param>
        /// <returns>Task</returns>
        public async Task AddHomeworkToLecture(int lectureId, string task)
        {
            var lecture = await _unitOfWork.Lectures.GetLectureWithStudentsById(lectureId);
            if (lecture == null)
                throw new EntityNotFoundException($"Lecture with {lectureId} id doesn't exist in database");
            var students = lecture.StudentLectures.Select(s => s.Student);
            foreach (var student in students)
                await _unitOfWork.Homeworks.AddAsync(new Homework { LectureId = lectureId, Task = task, StudentId = student.Id, Mark = 0 });
            await _unitOfWork.SaveAsync();
            _logger.LogInformation(DateTime.Now + $"\nHomeworks add to lecture with {lectureId} id, students at course get homework");
        }

        /// <summary>
        /// Check how many lectures a student missed and send him a message if there are more than 3 passes
        /// </summary>
        /// <param name="lectureId">Last lecture id at course</param>
        /// <param name="messageSenderService"></param>
        /// <returns>Task</returns>
        public async Task CheckNumberOfStudentsLecturesMissed(int lectureId, IMessageSenderService messageSenderService)
        {
            if (messageSenderService == null)
                throw new EntityNullException(nameof(messageSenderService));
            var lecture = await _unitOfWork.Lectures.GetLectureWithStudentsById(lectureId);
            if (lecture == null)
                throw new EntityNotFoundException($"Lecture with {lectureId} id doesn't exist in database");
            var lecturer = await GetLecturerOfLecture(lectureId);

            foreach (var student in lecture.StudentLectures)
            {
                // There is no need to send a message to the student if he is in class
                if (student.Attendance == false)
                {
                    var countOfMissedLectures = await GetNumberOfStudentLecturesMissed(student.StudentId, lecture.CourseId);
                    if (countOfMissedLectures > 3)
                    {
                        _logger.LogInformation(DateTime.Now + $"\nStudent with {student.StudentId} id has missed more than 3 lecture, sending mail to student and lecturer email");
                        messageSenderService.Send("You have missed more than 3 lectures", student.Student.Email);
                        messageSenderService.Send($"Student {student.Student.Name} has missed more than 3 lectures", lecturer.Email);
                    }
                }
            }
        }

        /// <summary>
        /// Create a new lecture, already contains the students at the course of lecture with attendance = false
        /// </summary>
        /// <param name="newLecture">New lecture that need to create</param>
        /// <returns>Task with new lecture object as a result</returns>
        /// <exception cref="EntityNullException">If argument is null</exception>
        /// <exception cref="EntityNotFoundException">If object has a foreign keys and objects with that ids doesn't exist in database</exception>
        /// <exception cref="EntityAlreadyExistException">If object with that id already exist in database</exception>
        public async Task<Lecture> CreateLecture(Lecture newLecture)
        {
            if (newLecture == null)
                throw new EntityNullException(nameof(newLecture));
            var course = await _unitOfWork.Courses.GetByIdAsync(newLecture.CourseId);
            if (course == null)
                throw new EntityNotFoundException($"Course with {newLecture.CourseId} id doesn't exist in database");
            if (await _unitOfWork.Lectures.Contains(newLecture))
                throw new EntityAlreadyExistException($"Lecture with {newLecture.Id} id already exist in database");
            var courseOfLecture = _unitOfWork.Courses.GetCoursesWithStudents().FirstOrDefault(c => c.Id == newLecture.CourseId);
            var studentsOfCourse = courseOfLecture.StudentCourses.Select(s => s.Student);
            foreach (var student in studentsOfCourse)
            {
                newLecture.StudentLectures.Add(new StudentLecture { StudentId = student.Id, LectureId = newLecture.Id });
            }
            var result = await _unitOfWork.Lectures.AddAsync(newLecture);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation(DateTime.Now + $"\nCreating lecture is successful");
            return result;
        }

        /// <summary>
        /// Delete a lecture
        /// </summary>
        /// <param name="lecture">The lecture that need to delete</param>
        /// <returns>Task</returns>
        /// <exception cref="EntityNullException">If argument is null</exception>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception> 
        public async Task DeleteLecture(Lecture lecture)
        {
            if (lecture == null)
            {
                throw new EntityNullException();
            }
            if (await _unitOfWork.Lectures.Contains(lecture) == false)
            {
                throw new EntityNotFoundException($"Lecture with {lecture.Id} id doesn't exist in database");
            }
            _unitOfWork.Lectures.Remove(lecture);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation(DateTime.Now + $"\nLecture with {lecture.Id} id deleted from database");
        }

        /// <summary>
        /// Get the lectures
        /// </summary>
        /// <returns>IQueryable collection of lectures collection</returns>
        public IQueryable<Lecture> GetAllLectures()
        {
            _logger.LogInformation(DateTime.Now + $"\nGet lectures");
            return _unitOfWork.Lectures.GetAll();
        }

        /// <summary>
        /// Get the lectures with course
        /// </summary>
        /// <returns>IQueryable collection of lectures with course object</returns>
        public IQueryable<Lecture> GetAllLecturesWithCourse()
        {
            _logger.LogInformation(DateTime.Now + $"\nGet lectures with course");
            return _unitOfWork.Lectures.GetAllWithCourse();
        }

        /// <summary>
        /// Get the lectures with homeworks
        /// </summary>
        /// <returns>IQueryable collection of lectures with homeworks collection</returns>
        public IQueryable<Lecture> GetAllLecturesWithHomeworks()
        {
            _logger.LogInformation(DateTime.Now + $"\nGet lectures with homeworks");
            return _unitOfWork.Lectures.GetAllWithHomeworks();
        }

        /// <summary>
        /// Get the lectures with students
        /// </summary>
        /// <returns>IQueryable collection of lectures with students collection</returns>
        public IQueryable<Lecture> GetAllLecturesWithStudents()
        {
            _logger.LogInformation(DateTime.Now + $"\nGet lectures with students");
            return _unitOfWork.Lectures.GetAllWithStudents();
        }

        /// <summary>
        /// Get the lecture with specified id
        /// </summary>
        /// <param name="id">Id of lecture</param>
        /// <returns>Task with the lecture object as result</returns>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        public async Task<Lecture> GetLectureById(int id)
        {
            var lecture = await _unitOfWork.Lectures.GetByIdAsync(id);
            if (lecture == null)
                throw new EntityNotFoundException($"Lecture with {id} id doesn't exist in database");
            _logger.LogInformation(DateTime.Now + $"\nGet lecture with {id} id");
            return lecture;
        }

        /// <summary>
        /// Get the lecturer of lecture
        /// </summary>
        /// <param name="lectureId">Lecture Id</param>
        /// <returns>Task with lecturer object as a result</returns>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        public async Task<Lecturer> GetLecturerOfLecture(int lectureId)
        {
            var lecture = await _unitOfWork.Lectures.GetLectureWithCourseById(lectureId);
            if (lecture == null)
                throw new EntityNotFoundException($"Lecture with {lectureId} id doesn't exist in database");
            var lecturer = await _unitOfWork.Lecturers.GetByIdAsync(lecture.Course.LecturerId);
            if (lecturer == null)
                throw new EntityNotFoundException($"Lecturer of lecture with {lectureId} id doesn't exist");
            _logger.LogInformation(DateTime.Now + $"\nGet lecturer of lecture with {lectureId} id");
            return lecturer;
        }

        /// <summary>
        /// Get the lecture with specified id with course
        /// </summary>
        /// <param name="id">Id of lecture</param>
        /// <returns>Task with the lecture object as result</returns>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        public async Task<Lecture> GetLectureWithCourseById(int id)
        {
            var lecture = await _unitOfWork.Lectures.GetLectureWithCourseById(id); ;
            if (lecture == null)
                throw new EntityNotFoundException($"Lecture with {id} id doesn't exist in database");
            _logger.LogInformation(DateTime.Now + $"\nGet lecture with {id} id with course");
            return lecture;
        }

        /// <summary>
        /// Get the lecture with specified id with homeworks
        /// </summary>
        /// <param name="id">Id of lecture</param>
        /// <returns>Task with the lecture object as result</returns>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        public async Task<Lecture> GetLectureWithHomeworksById(int id)
        {
            var lecture = await _unitOfWork.Lectures.GetLectureWithHomeworksById(id);
            if (lecture == null)
                throw new EntityNotFoundException($"Lecture with {id} id doesn't exist in database");
            _logger.LogInformation(DateTime.Now + $"\nGet lecture with {id} id with homeworks");
            return lecture;
        }

        /// <summary>
        /// Get the lecture with specified id with lecture and students
        /// </summary>
        /// <param name="id">Id of lecture</param>
        /// <returns>Task with the lecture object as result</returns>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        public async Task<Lecture> GetLectureWithStudentsById(int id)
        {
            var lecture = await _unitOfWork.Lectures.GetLectureWithStudentsById(id);
            if (lecture == null)
                throw new EntityNotFoundException($"Lecture with {id} id doesn't exist in database");
            _logger.LogInformation(DateTime.Now + $"\nGet lecture with {id} id with students");
            return lecture;
        }

        /// <summary>
        /// Get the number of lectures that missed a student at the course
        /// </summary>
        /// <param name="studentId">Student Id</param>
        /// <param name="courseId">Course Id</param>
        /// <returns>Task with int number as a result</returns>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        public async Task<int> GetNumberOfStudentLecturesMissed(int studentId,int courseId)
        {
            var student = await _unitOfWork.Students.GetWithLecturesByIdAsync(studentId);
            if (student == null)
                throw new EntityNotFoundException($"Student with {studentId} id doesn't exist in database");
            var course = await _unitOfWork.Courses.GetByIdAsync(courseId);
            if (course == null)
                throw new EntityNotFoundException($"Course with {courseId} id doesn't exist in database");
            var lecturesOfStudent = student.StudentLectures.Where(c => c.Lecture.CourseId == courseId);
            var countOfMissedLectures = 0;
            foreach (var lecture in lecturesOfStudent)
            {
                if (lecture.Attendance == false)
                    countOfMissedLectures++;
            }
            _logger.LogInformation(DateTime.Now + $"\nGet number of lectures that the student with {studentId} id has missed at course with {courseId}, it's {countOfMissedLectures}");

            return countOfMissedLectures;
        }

        /// <summary>
        /// Mark absence in the lecture, as a default all student's attendance is false, that method need to fix mistake
        /// </summary>
        /// <param name="lectureId">Lecture id</param>
        /// <param name="studentIds">Student ids that wasn't at the lecture</param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        public async Task MarkAbsence(int lectureId, IEnumerable<int> studentIds)
        {
            var lecture = await _unitOfWork.Lectures.GetLectureWithStudentsById(lectureId);
            if (lecture == null)
                throw new EntityNotFoundException($"Lecture with {lectureId} id doesn't exist in database");
            foreach (var studentId in studentIds)
            {
                var attendance = lecture.StudentLectures.FirstOrDefault(s => s.StudentId == studentId);
                if (attendance == null)
                    throw new EntityNotFoundException($"Student with {studentId} id doesn't attend at course of lecture with id {lectureId}");
                attendance.Attendance = false;
            }
            await _unitOfWork.SaveAsync();
            _logger.LogInformation(DateTime.Now + $"\nAbsence for lecture with {lectureId} noted");
        }

        /// <summary>
        /// Mark attendance in the lecture, as a default all student's attendance is false
        /// </summary>
        /// <param name="lectureId">Lecture id</param>
        /// <param name="studentIds">Student ids that was at the lecture</param>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database</exception>
        public async Task MarkAttendance(int lectureId, IEnumerable<int> studentIds)
        {
            var lecture = await _unitOfWork.Lectures.GetLectureWithStudentsById(lectureId);
            if (lecture == null)
                throw new EntityNotFoundException($"Lecture with {lectureId} id doesn't exist in database");
            foreach (var studentId in studentIds)
            {
                var attendance = lecture.StudentLectures.FirstOrDefault(s => s.StudentId == studentId);
                if (attendance == null)
                    throw new EntityNotFoundException($"Student with {studentId} id doesn't attend at course of lecture with id {lectureId}");
                attendance.Attendance = true;
            }
            await _unitOfWork.SaveAsync();
            _logger.LogInformation(DateTime.Now + $"\nAttendance for lecture with {lectureId} noted");
        }

        /// <summary>
        /// Update a existing lecture
        /// </summary>
        /// <param name="lecture">The updated lecture</param>
        /// <returns>Task</returns>
        /// <exception cref="EntityNullException">If argument is null</exception>
        /// <exception cref="EntityNotFoundException">If object with that id doesn't exist in database or object has a foreign keys and objects with that ids doesn't exist in database or </exception>
        public async Task UpdateLecture(Lecture lecture)
        {
            if (lecture == null)
                throw new EntityNullException(nameof(lecture));
            var course = await _unitOfWork.Courses.GetByIdAsync(lecture.CourseId);
            if (course == null)
                throw new EntityNotFoundException($"Course with {lecture.CourseId} id doesn't exist in database");
            var oldEntity = await _unitOfWork.Lectures.GetByIdAsync(lecture.Id);
            if (oldEntity == null)
                throw new EntityNotFoundException($"Lecture with {lecture.Id} id doesn't exist in database");
            _unitOfWork.Lectures.Update(lecture);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation(DateTime.Now + $"\nUpdating lecture with {lecture.Id} id is successful");

        }
    }
}
