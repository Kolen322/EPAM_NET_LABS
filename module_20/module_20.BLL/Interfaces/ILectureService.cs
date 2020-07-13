using module_20.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace module_20.BLL.Interfaces
{
    /// <summary>
    /// Specifies the contract for a lecture service
    /// </summary>
    public interface ILectureService
    {
        /// <summary>
        /// Get the lectures
        /// </summary>
        /// <returns>IQueryable collection of lectures collection</returns>
        IQueryable<Lecture> GetAllLectures();
        /// <summary>
        /// Get the lectures with homeworks
        /// </summary>
        /// <returns>IQueryable collection of lectures with homeworks collection</returns>
        IQueryable<Lecture> GetAllLecturesWithHomeworks();
        /// <summary>
        /// Get the lectures with course
        /// </summary>
        /// <returns>IQueryable collection of lectures with course object</returns>
        IQueryable<Lecture> GetAllLecturesWithCourse();
        /// <summary>
        /// Get the lectures with students
        /// </summary>
        /// <returns>IQueryable collection of lectures with students collection</returns>
        IQueryable<Lecture> GetAllLecturesWithStudents();
        /// <summary>
        /// Get the lecture with specified id
        /// </summary>
        /// <param name="id">Id of lecture</param>
        /// <returns>Task with the lecture object as result</returns>
        Task<Lecture> GetLectureById(int id);
        /// <summary>
        /// Get the lecture with specified id with homeworks
        /// </summary>
        /// <param name="id">Id of lecture</param>
        /// <returns>Task with the lecture object as result</returns>
        Task<Lecture> GetLectureWithHomeworksById(int id);
        /// <summary>
        /// Get the lecture with specified id with course
        /// </summary>
        /// <param name="id">Id of lecture</param>
        /// <returns>Task with the lecture object as result</returns>
        Task<Lecture> GetLectureWithCourseById(int id);
        /// <summary>
        /// Get the lecture with specified id with lecture and students
        /// </summary>
        /// <param name="id">Id of lecture</param>
        /// <returns>Task with the lecture object as result</returns>
        Task<Lecture> GetLectureWithStudentsById(int id);
        /// <summary>
        /// Create a new lecture
        /// </summary>
        /// <param name="newLecture">New lecture that need to create</param>
        /// <returns>Task with new lecture object as a result</returns>
        Task<Lecture> CreateLecture(Lecture newLecture);
        /// <summary>
        /// Update a existing lecture
        /// </summary>
        /// <param name="lecture">The updated lecture</param>
        /// <returns>Task</returns>
        Task UpdateLecture(Lecture lecture);
        /// <summary>
        /// Delete a lecture
        /// </summary>
        /// <param name="lecture">The lecture that need to delete</param>
        /// <returns>Task</returns>
        Task DeleteLecture(Lecture lecture);
        /// <summary>
        /// Add homework to all students at the course of lecture
        /// </summary>
        /// <param name="lectureId">Lecture Id</param>
        /// <param name="task">Task of homework</param>
        /// <returns>Task</returns>
        Task AddHomeworkToLecture(int lectureId, string task);
        /// <summary>
        /// Mark attendance in the lecture, as a default all student's attendance is false
        /// </summary>
        /// <param name="lectureId">Lecture id</param>
        /// <param name="studentIds">Student ids that was at the lecture</param>
        /// <returns>Task</returns>
        Task MarkAttendance(int lectureId, IEnumerable<int> studentIds);
        /// <summary>
        /// Check how many lectures a student missed and send him a message if there are more than 3 passes
        /// </summary>
        /// <param name="lectureId">Last lecture id at course</param>
        /// <param name="messageSenderService"></param>
        /// <returns>Task</returns>
        Task CheckNumberOfStudentsLecturesMissed(int lectureId, IMessageSenderService messageSenderService);
        /// <summary>
        /// Mark absence in the lecture, as a default all student's attendance is false, that method need to fix mistake
        /// </summary>
        /// <param name="lectureId">Lecture id</param>
        /// <param name="studentIds">Student ids that wasn't at the lecture</param>
        /// <returns></returns>
        Task MarkAbsence(int lectureId, IEnumerable<int> studentIds);
        /// <summary>
        /// Get the number of lectures that missed a student at the course
        /// </summary>
        /// <param name="studentId">Student Id</param>
        /// <param name="courseId">Course Id</param>
        /// <returns>Task with int number as a result</returns>
        Task<int> GetNumberOfStudentLecturesMissed(int studentId, int courseId);
        /// <summary>
        /// Get the lecturer of lecture
        /// </summary>
        /// <param name="lectureId">Lecture Id</param>
        /// <returns>Task with lecturer object as a result</returns>
        Task<Lecturer> GetLecturerOfLecture(int lectureId);
    }
}
