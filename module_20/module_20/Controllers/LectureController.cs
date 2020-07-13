using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using module_20.BLL.Interfaces;
using module_20.BLL.Services;
using module_20.DAL.Entities;
using AutoMapper;
using module_20.Web.Resources;
using module_20.Web.Validators;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.EntityFrameworkCore;

namespace module_20.Web.Controllers
{
    /// <summary>
    /// Controller to work with lecture
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [FormatFilter]
    public class LectureController : ControllerBase
    {
        private readonly ILectureService _lectureService;
        private readonly IMapper _mapper;
        private readonly ILogger<LectureController> _logger;

        /// <summary>
        /// Constructor with specified ICourseService, IMapper, ILogger
        /// </summary>
        /// <param name="lectureService">Object that realize lecture service contract</param>
        /// <param name="mapper">Object that realize mapper contract</param>
        /// <param name="logger">Object that realize logger contract</param>
        public LectureController(ILectureService lectureService, IMapper mapper, ILogger<LectureController> logger)
        {
            _lectureService = lectureService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all lectures with course
        /// </summary>
        /// <returns>Collection of lectures</returns>
        /// <response code="200">Returns lectures collection</response>
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<LectureWithCourseResource>>> GetAllLectures()
        {
            var lectures = await _lectureService.GetAllLecturesWithCourse().ToListAsync();
            var lecturesResource = _mapper.Map<IEnumerable<Lecture>, IEnumerable<LectureWithCourseResource>>(lectures);

            return Ok(lecturesResource);
        }

        /// <summary>
        /// Get lecture with course by id
        /// </summary>
        /// <param name="id">Lecture Id</param>
        /// <returns>Lecture object</returns>
        /// <response code="200">Returns lecture object</response>
        /// <response code="404">If lecture with that id doesn't exist in database</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<LectureWithCourseResource>> GetLectureById(int id)
        {
            var lecture = await _lectureService.GetLectureWithCourseById(id);
            var lectureResource = _mapper.Map<Lecture, LectureWithCourseResource>(lecture);

            return Ok(lectureResource);
        }

        /// <summary>
        /// Get attendance of lecture by lecture name
        /// </summary>
        /// <param name="lectureName">Lecture name</param>
        /// <returns>Attendance of lecture</returns>
        /// <remarks>
        /// Available two output formats: txt and json.
        /// For txt format put "txt". For json put "json"
        /// </remarks>
        /// <response code="200">Returns attendance collection</response>
        /// <response code="404">If lecture with that id doesn't exist in database</response>
        [HttpGet("getAttendance/{lectureName}.{format?}")]
        public async Task<ActionResult<IEnumerable<AttendanceOfLectureResource>>> GetAttendanceOfLectureByName(string lectureName)
        {
            var lecture = await _lectureService.GetAllLecturesWithStudents()
                .SingleOrDefaultAsync(l => l.Name == lectureName);
            if (lecture == null)
                return NotFound();
            var attendance = lecture.StudentLectures;
            var attendanceResource = _mapper.Map<IEnumerable<StudentLecture>, IEnumerable<AttendanceOfLectureResource>>(attendance);

            return Ok(attendanceResource);
        }

        /// <summary>
        /// Create a new lecture, as a default students at lecture have a false attendance
        /// </summary>
        /// <param name="saveLectureResource">Information need to create a new lecture</param>
        /// <returns>A newly create lecture</returns>
        /// <remarks>Validation rules:
        /// 1. Name isn't empty and maximum length = 150
        /// 2. Date isn't empty
        /// 3. Course id isn't empty and greater than 0 </remarks>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="400">If the item is null or validation isn't valid</response> 
        [HttpPost("")]
        public async Task<ActionResult<LectureWithCourseResource>> CreateLecture([FromBody] SaveLectureResource saveLectureResource)
        {
            var validator = new SaveLectureResourceValidator();
            var validationResult = await validator.ValidateAsync(saveLectureResource);

            if (!validationResult.IsValid)
            {
                _logger.LogError(DateTime.Now + $"\nCreating lecture failed, validation isn't valid " + validationResult.Errors.ToString());
                return BadRequest(validationResult.Errors);
            }
                
            var lectureToCreate = _mapper.Map<SaveLectureResource, Lecture>(saveLectureResource);

            var result = await _lectureService.CreateLecture(lectureToCreate);

            var createdLecture = _mapper.Map<Lecture, LectureWithCourseResource>(result);

            return Ok(createdLecture);
        }

        /// <summary>
        /// Add homework to all student at course of lecture
        /// </summary>
        /// <param name="lectureId">Lecture Id</param>
        /// <param name="task">Task of homework</param>
        /// <returns>Collection of newly created homeworks</returns>
        [HttpPut("addHomeworkToLecture/{lectureId}")]
        public async Task<ActionResult<IEnumerable<HomeworkWithLectureResource>>> AddHomeworkToLecture(int lectureId, [FromBody] string task)
        {
            await _lectureService.AddHomeworkToLecture(lectureId, task);
            var updatedLecture = await _lectureService.GetLectureWithHomeworksById(lectureId);
            var homeworksOfUpdatedLectureResource = _mapper.Map<IEnumerable<Homework>, IEnumerable<HomeworkWithLectureResource>>(updatedLecture.Homeworks);

            return Ok(homeworksOfUpdatedLectureResource);
        }

        /// <summary>
        /// Mark attendance at lecture, as a default students have a false attendance
        /// </summary>
        /// <param name="lectureId">Lecture Id</param>
        /// <param name="studentIds">Collection of student ids that was at lecture</param>
        /// <returns>The update attendance</returns>
        /// <remarks> For test a business logic with send message to email about lectures missed, create two new lectures at course with id 1, and don't mark student with id 2, and check logger.
        /// </remarks>
        /// <response code="200">Returns the updated attendance</response>
        /// <response code="404">If the lecture with that id doesn't exist in database</response> 
        [HttpPut("markAttendance/{lectureId}")]
        public async Task<ActionResult<IEnumerable<AttendanceOfLectureResource>>> MarkAttendance(int lectureId, [FromBody] IEnumerable<int> studentIds)
        {
            await _lectureService.MarkAttendance(lectureId, studentIds);
            await _lectureService.CheckNumberOfStudentsLecturesMissed(lectureId, new EmailSenderService());

            var updatedLecture = await _lectureService.GetLectureWithStudentsById(lectureId);
            var attendance = updatedLecture.StudentLectures;
            var attendanceResource = _mapper.Map<IEnumerable<StudentLecture>, IEnumerable<AttendanceOfLectureResource>>(attendance);

            return Ok(attendanceResource);
        }

        /// <summary>
        /// Mark absence at lecture, this method need to fix your mistake as a default attendance in lecture is false
        /// </summary>
        /// <param name="lectureId">Lecture Id</param>
        /// <param name="studentIds">Collection of student ids that wasn't at lecture</param>
        /// <returns>The updated attendance</returns>
        /// <response code="200">Returns the updated attendance</response>
        /// <response code="404">If the lecture with that id doesn't exist in database</response> 
        [HttpPut("markAbsence/{lectureId}")]
        public async Task<ActionResult<IEnumerable<AttendanceOfLectureResource>>> MarkAbsence(int lectureId, [FromBody] IEnumerable<int> studentIds)
        {
            await _lectureService.MarkAbsence(lectureId, studentIds);
            await _lectureService.CheckNumberOfStudentsLecturesMissed(lectureId, new EmailSenderService());

            var updatedLecture = await _lectureService.GetLectureWithStudentsById(lectureId);
            var attendance = updatedLecture.StudentLectures;
            var attendanceResource = _mapper.Map<IEnumerable<StudentLecture>, IEnumerable<AttendanceOfLectureResource>>(attendance);

            return Ok(attendanceResource);
        }

        /// <summary>
        /// Update a existing lecture
        /// </summary>
        /// <param name="id">Updated lecture id</param>
        /// <param name="saveLectureResource">Information need to update lecture</param>
        /// <returns>A updated lecture</returns>
        /// <remarks>Validation rules:
        /// 1. Name isn't empty and maximum length = 150
        /// 2. Date isn't empty
        /// 3. Course id isn't empty and greater than 0 </remarks>
        /// <response code="200">Returns the updated item</response>
        /// <response code="400">If the item is null or validation isn't valid</response> 
        /// <response code="404">If lecture with that id doesn't exist in database</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<LectureWithCourseResource>> UpdateLecture(int id, [FromBody] SaveLectureResource saveLectureResource)
        {
            var validator = new SaveLectureResourceValidator();
            var validationResult = await validator.ValidateAsync(saveLectureResource);

            var requestIsInvalid = id <= 0 || !validationResult.IsValid;

            if (requestIsInvalid)
            {
                _logger.LogError(DateTime.Now + $"\nUpdating lecture with {id} id failed, validation isn't valid " + validationResult.Errors.ToString());
                return BadRequest(validationResult.Errors);
            }

            var lectureToBeUpdate = await _lectureService.GetLectureWithCourseById(id);

            var lecture = _mapper.Map<SaveLectureResource, Lecture>(saveLectureResource);
            lecture.Id = id;

            await _lectureService.UpdateLecture(lecture);

            var updatedLecture = await _lectureService.GetLectureWithCourseById(id);
            var updatedLectureResource = _mapper.Map<Lecture, LectureWithCourseResource>(updatedLecture);

            return Ok(updatedLectureResource);
        }

        /// <summary>
        /// Delete a lecture
        /// </summary>
        /// <param name="id">The lecture that need to delete</param>
        /// <returns></returns>
        /// <response code="204">Lecture is successfully deleted</response>
        /// <response code="404">Lecture with that id doesn't exist in database</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLecture(int id)
        {
            var lecture = await _lectureService.GetLectureWithCourseById(id);

            await _lectureService.DeleteLecture(lecture);

            return NoContent();
        }
    }
}