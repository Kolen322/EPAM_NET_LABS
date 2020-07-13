using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using module_20.BLL.Interfaces;
using module_20.BLL.Services;
using module_20.DAL.Entities;
using AutoMapper;
using module_20.Web.Resources;
using module_20.Web.Validators;
using System.Linq;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.EntityFrameworkCore;

namespace module_20.Web.Controllers
{
    /// <summary>
    /// Controller to work with homework
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworkController : ControllerBase
    {
        private readonly IHomeworkService _homeworkService;
        private readonly IMapper _mapper;
        private readonly ILogger<HomeworkController> _logger;

        /// <summary>
        /// Constructor with specified ICourseService, IMapper, ILogger
        /// </summary>
        /// <param name="homeworkService">Object that realize homework service contract</param>
        /// <param name="mapper">Object that realize mapper contract</param>
        /// <param name="logger">Object that realize logger contract</param>
        public HomeworkController(IHomeworkService homeworkService, IMapper mapper, ILogger<HomeworkController> logger)
        {
            _homeworkService = homeworkService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all homeworks with student and lecture
        /// </summary>
        /// <returns>Collection of homeworks</returns>
        /// <response code="200">Returns homeworks collection</response>
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<HomeworkWithStudentsAndLectureResource>>> GetAllHomeworks()
        {
            var homeworks = await _homeworkService.GetAllHomeworksWithLectureAndStudent().ToListAsync();
            var homeworksResource = _mapper.Map<IEnumerable<Homework>, IEnumerable<HomeworkWithStudentsAndLectureResource>>(homeworks);

            return Ok(homeworksResource);
        }

        /// <summary>
        /// Get homework with student and lecture by id
        /// </summary>
        /// <param name="id">Homework Id</param>
        /// <returns>Homework object</returns>
        /// <response code="200">Returns homework object</response>
        /// <response code="404">If homework with that id doesn't exist in database</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<HomeworkWithStudentsAndLectureResource>> GetHomeworkById(int id)
        {
            var homework = await _homeworkService.GetHomeworkWithLectureAndStudentById(id);
            var homeworkResource = _mapper.Map<Homework, HomeworkWithStudentsAndLectureResource>(homework);

            return Ok(homeworkResource);
        }

        /// <summary>
        /// Get all homeworks of lecture
        /// </summary>
        /// <param name="lectureId">Lecture id</param>
        /// <returns>Collection of homeworks with student</returns>
        /// <response code="200">Returns homeworks collection</response>
        /// <response code="404">Lecture with that id doesn't exist in database</response>
        [HttpGet("getAllHomeworksOfLecture/{lectureId}")]
        public async Task<ActionResult<IEnumerable<HomeworkWitnStudentsResource>>> GetHomeworksByLectureId(int lectureId)
        {
            var homeworks = await _homeworkService.GetAllHomeworksWithLectureAndStudent()
                .Where(l => l.LectureId == lectureId)
                .ToListAsync();
            var homeworksResource = _mapper.Map<IEnumerable<Homework>, IEnumerable<HomeworkWitnStudentsResource>>(homeworks);

            return Ok(homeworksResource);
        }

        /// <summary>
        /// Get all homeworks of student
        /// </summary>
        /// <param name="studentId">Student id</param>
        /// <returns>Collection of homeworks with lecture</returns>
        /// <response code="200">Returns homeworks collection</response>
        /// <response code="404">Student with that id doesn't exist in database</response>
        [HttpGet("getAllHomeworksOfStudent/{studentId}")]
        public async Task<ActionResult<IEnumerable<HomeworkWithLectureResource>>> GetAllHomeworksByStudentId(int studentId)
        {
            var homeworks = await _homeworkService.GetAllHomeworksWithLectureAndStudent()
                .Where(s => s.StudentId == studentId)
                .ToListAsync();
            var homeworkResource = _mapper.Map<IEnumerable<Homework>, IEnumerable<HomeworkWithLectureResource>>(homeworks);

            return Ok(homeworkResource);
        }

        /// <summary>
        /// Get all homeworks of course
        /// </summary>
        /// <param name="courseId">Course id</param>
        /// <returns>Collection of homeworks with lecture and student</returns>
        /// <response code="200">Returns homeworks collection</response>
        /// <response code="404">Course with that id doesn't exist in database</response>
        [HttpGet("getAllHomeworksOfCourse/{courseId}")]
        public async Task<ActionResult<IEnumerable<HomeworkWithStudentsAndLectureResource>>> GetAllHomeworksByCourseId(int courseId)
        { 
            var homeworks = await _homeworkService.GetAllHomeworksWithLectureAndStudent()
                .Where(l => l.Lecture.CourseId == courseId)
                .ToListAsync();
            var homeworkResource = _mapper.Map<IEnumerable<Homework>, IEnumerable<HomeworkWithStudentsAndLectureResource>>(homeworks);

            return Ok(homeworkResource);
        }

        /// <summary>
        /// Get all homeworks of student at certain course
        /// </summary>
        /// <param name="courseId">Course id</param>
        /// <param name="studentId">Student id</param>
        /// <returns>Collection of homeworks with lecture</returns>
        /// <response code="200">Returns homeworks collection</response>
        /// <response code="404">Course or Student with that id doesn't exist in database</response>
        [HttpGet("getAllHomeworksCourseOfStudent/{courseId}/{studentId}")]
        public async Task<ActionResult<HomeworkWithLectureResource>> GetAllHomeworksByCourseIdAndStudentId(int courseId, int studentId)
        {
            var homeworks = await _homeworkService.GetAllHomeworksWithLectureAndStudent()
                .Where(l => l.Lecture.CourseId == courseId)
                .Where(s => s.StudentId == studentId)
                .ToListAsync();
            var homeworkResource = _mapper.Map<IEnumerable<Homework>, IEnumerable<HomeworkWithLectureResource>>(homeworks);

            return Ok(homeworkResource);
        }

        /// <summary>
        /// Create a new homework
        /// </summary>
        /// <param name="saveHomeworkResource">Information need to create a new homework</param>
        /// <returns>A newly create homework</returns>
        /// <remarks>Validation rules:
        /// 1. Mark is greater or equal than 0
        /// 2. Lecture id isn't empty and greater than 0
        /// 3. Student id isn't empty and greater than 0
        /// 4. Task isn't empty </remarks>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="400">If the item is null or validation isn't valid</response> 
        [HttpPost("")]
        public async Task<ActionResult<HomeworkWithStudentsAndLectureResource>> CreateHomework([FromBody] SaveHomeworkResource saveHomeworkResource)
        {
            var validator = new SaveHomeworkResourceValidator();
            var validationResult = await validator.ValidateAsync(saveHomeworkResource);

            if (!validationResult.IsValid)
            {
                _logger.LogError(DateTime.Now + $"\nCreating homework failed, validation isn't valid " + validationResult.Errors.ToString());
                return BadRequest(validationResult.Errors);
            }

            var homeworkToCreate = _mapper.Map<SaveHomeworkResource, Homework>(saveHomeworkResource);

            var result = await _homeworkService.CreateHomework(homeworkToCreate);

            var createdHomework = _mapper.Map<Homework, HomeworkWithStudentsAndLectureResource>(result);

            return Ok(createdHomework);
        }

        /// <summary>
        /// Update a existing homework
        /// </summary>
        /// <param name="id">Updated homework id</param>
        /// <param name="saveHomeworkResource">Information need to update homework</param>
        /// <returns>A updated homework</returns>
        /// <remarks>Validation rules:
        /// 1. Mark is greater or equal than 0
        /// 2. Lecture id isn't empty and greater than 0
        /// 3. Student id isn't empty and greater than 0
        /// 4. Task isn't empty </remarks>
        /// <response code="200">Returns the updated item</response>
        /// <response code="400">If the item is null or validation isn't valid</response> 
        /// <response code="404">If course with that id doesn't exist in database</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<HomeworkWithStudentsAndLectureResource>> UpdateHomework(int id, [FromBody] SaveHomeworkResource saveHomeworkResource)
        {
            var validator = new SaveHomeworkResourceValidator();
            var validationResult = await validator.ValidateAsync(saveHomeworkResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
            {
                _logger.LogError(DateTime.Now + $"\nUpdating homework with {id} id failed, validation isn't valid " + validationResult.Errors.ToString());
                return BadRequest(validationResult.Errors);
            }

            var homeworkToBeUpdate = await _homeworkService.GetHomeworkWithLectureAndStudentById(id);

            var homework = _mapper.Map<SaveHomeworkResource, Homework>(saveHomeworkResource);
            homework.Id = id;

            await _homeworkService.UpdateHomework(homework);

            await _homeworkService.CheckAverageMark(homework.StudentId, homework.Lecture.CourseId, new MobileSenderService());

            var updatedHomework = await _homeworkService.GetHomeworkWithLectureAndStudentById(id);
            var updatedHomeworkResource = _mapper.Map<Homework, HomeworkWithStudentsAndLectureResource>(updatedHomework);

            return Ok(updatedHomeworkResource);
        }

        /// <summary>
        /// Set mark to homeworks
        /// </summary>
        /// <param name="marks">Collection with homework id and mark</param>
        /// <returns>A updated homeworks</returns>
        /// <remarks> For test a business logic with send message to mobile about average student grade, set any mark to homework with id 5, and check logger
        /// </remarks>
        /// <response code="200">Returns the updated items</response>
        /// <response code="404">If course with that id doesn't exist in database</response>
        [HttpPut("setMark")]
        public async Task<ActionResult<HomeworkWithStudentsAndLectureResource>> SetMarkToHomeworks([FromBody] IEnumerable<SetMarkHomeworkResource> marks)
        {
            List<HomeworkWithStudentsAndLectureResource> result = new List<HomeworkWithStudentsAndLectureResource>();
            foreach (var mark in marks)
            {
                var homework = await _homeworkService.GetHomeworkWithLectureAndStudentById(mark.Id);
                homework.Mark = mark.Mark;
                await _homeworkService.UpdateHomework(homework);
                var homeworkResource = _mapper.Map<Homework, HomeworkWithStudentsAndLectureResource>(homework);
                result.Add(homeworkResource);
                await _homeworkService.CheckAverageMark(homework.StudentId, homework.Lecture.CourseId, new MobileSenderService());
            }

            return Ok(result);
        }

        /// <summary>
        /// Delete a homework
        /// </summary>
        /// <param name="id">The homework that need to delete</param>
        /// <returns></returns>
        /// <response code="204">Homework is successfully deleted</response>
        /// <response code="404">Homework with that id doesn't exist in database</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHomework(int id)
        {
            var homework = await _homeworkService.GetHomeworkWithLectureAndStudentById(id);

            await _homeworkService.DeleteHomework(homework);

            return NoContent();
        }
    }
}