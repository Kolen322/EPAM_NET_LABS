using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using module_20.BLL.Interfaces;
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
    /// Controller to work with course
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;
        private readonly ILogger<CourseController> _logger;

        /// <summary>
        /// Constructor with specified ICourseService, IMapper, ILogger
        /// </summary>
        /// <param name="courseService">Object that realize course service contract</param>
        /// <param name="mapper">Object that realize mapper contract</param>
        /// <param name="logger">Object that realize logger contract</param>
        public CourseController(ICourseService courseService, IMapper mapper, ILogger<CourseController> logger)
        {
            _courseService = courseService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all courses
        /// </summary>
        /// <returns>Collection of courses</returns>
        /// <response code="200">Returns courses collection</response>
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<CourseResource>>> GetAllCourses()
        {
            var courses = await _courseService.GetAllCourses().ToListAsync();
            
            var coursesResource = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseResource>>(courses);

            return Ok(coursesResource);
        }

        /// <summary>
        /// Get all courses with students
        /// </summary>
        /// <returns>Collection of courses with students</returns>
        /// <response code="200">Returns courses collection</response>
        [HttpGet("coursesWithStudents")]
        public async Task<ActionResult<IEnumerable<CourseWithStudentsResource>>> GetAllWithStudents()
        {
            var courses = await _courseService.GetAllCoursesWithStudents().ToListAsync();
            var coursesResource = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseWithStudentsResource>>(courses);
            int i = 0;
            foreach (var course in courses)
            {
                var students = course.StudentCourses.Select(s => s.Student);
                var studentsResource = _mapper.Map<IEnumerable<Student>, IEnumerable<StudentResource>>(students);
                coursesResource.ElementAt(i).Students = studentsResource;
                i++;
            }

            return Ok(coursesResource);
        }

        /// <summary>
        /// Get all courses with lecturer
        /// </summary>
        /// <returns>Collection of courses with lecturer</returns>
        /// <response code="200">Returns courses collection</response>
        [HttpGet("coursesWithLecturer")]
        public async Task<ActionResult<IEnumerable<CourseWithLecturerResource>>> GetAllWithLecturer()
        {
            var courses = await _courseService.GetAllCoursesWithLecturer().ToListAsync();
            var coursesResource = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseWithLecturerResource>>(courses);

            return Ok(coursesResource);
        }

        /// <summary>
        /// Get course by id
        /// </summary>
        /// <param name="id">Course Id</param>
        /// <returns>Course object</returns>
        /// <response code="200">Returns course object</response>
        /// <response code="404">If course with that id doesn't exist in database</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseResource>> GetCourseById(int id)
        {
            var course = await _courseService.GetCourseById(id);
            var courseResource = _mapper.Map<Course, CourseResource>(course);

            return Ok(courseResource);
        }

        /// <summary>
        /// Get course with students by id
        /// </summary>
        /// <param name="id">Course Id</param>
        /// <returns>Course object with students collection</returns>
        /// <response code="200">Returns course object</response>
        /// <response code="404">If course with that id doesn't exist in database</response>
        [HttpGet("courseWithStudents/{id}")]
        public async Task<ActionResult<CourseWithStudentsResource>> GetCourseWithStudentsById(int id)
        {
            var course = await _courseService.GetCourseWithStudentsById(id);
            var courseResource = _mapper.Map<Course, CourseWithStudentsResource>(course);

            var students = course.StudentCourses.Select(s => s.Student);
            var studentsResource = _mapper.Map<IEnumerable<Student>, IEnumerable<StudentResource>>(students);

            courseResource.Students = studentsResource;

            return Ok(courseResource);
        }

        /// <summary>
        /// Get course with lecturer by id
        /// </summary>
        /// <param name="id">Course Id</param>
        /// <returns>Course object with lecturer object</returns>
        /// <response code="200">Returns course object</response>
        /// <response code="404">If course with that id doesn't exist in database</response>
        [HttpGet("courseWithLecturer/{id}")]
        public async Task<ActionResult<CourseWithLecturerResource>> GetCourseWithLecturerById(int id)
        {
            var course = await _courseService.GetCourseWithLecturerById(id);

            var courseResource = _mapper.Map<Course, CourseWithLecturerResource>(course);

            return Ok(courseResource);
        }

        /// <summary>
        /// Create a new course
        /// </summary>
        /// <param name="saveCourseResource">Information need to create a new course</param>
        /// <returns>A newly create course</returns>
        /// <remarks>Validation rules:
        /// 1. Course isn't empty and maximum length = 150
        /// 2. Lecturer id isn't empty and greater than 0</remarks>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="400">If the item is null or validation isn't valid</response> 
        [HttpPost("")]
        public async Task<ActionResult<CourseResource>> CreateCourse([FromBody] SaveCourseResource saveCourseResource)
        {
            var validator = new SaveCourseResourceValidator();
            var validationResult = await validator.ValidateAsync(saveCourseResource);

            if (!validationResult.IsValid)
            {
                _logger.LogError(DateTime.Now + "\nCreating course failed, validation isn't valid\n" + validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            var courseToCreate = _mapper.Map<SaveCourseResource, Course>(saveCourseResource);

            var result = await _courseService.CreateCourse(courseToCreate);

            var createdCourse = _mapper.Map<Course, CourseResource>(result);

            return Ok(createdCourse);
        }

        /// <summary>
        /// Update a existing course
        /// </summary>
        /// <param name="id">Updated course id</param>
        /// <param name="saveCourseResource">Information need to update a course object</param>
        /// <returns>A updated course</returns>
        ///  <remarks>Validation rules:
        /// 1. Course isn't empty and maximum length = 150
        /// 2. Lecturer id isn't empty and greater than 0</remarks>
        /// <response code="200">Returns the updated course</response>
        /// <response code="400">If the item is null or validation isn't valid</response> 
        /// <response code="404">If course with that id doesn't exist in database</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<CourseResource>> UpdateCourse(int id, [FromBody] SaveCourseResource saveCourseResource)
        {
            var validator = new SaveCourseResourceValidator();
            var validationResult = await validator.ValidateAsync(saveCourseResource);

            var requestIsInvalid = id <= 0 || !validationResult.IsValid;

            if (requestIsInvalid)
            {
                _logger.LogError(DateTime.Now + "\nUpdating course failed, validation isn't valid\n" + validationResult.Errors.ToString());
                return BadRequest(validationResult.Errors);
            }
                
            var courseToBeUpdate = await _courseService.GetCourseWithLecturerById(id);

            var course = _mapper.Map<SaveCourseResource, Course>(saveCourseResource);
            course.Id = id;

            await _courseService.UpdateCourse(course);

            var updatedCourse = await _courseService.GetCourseWithLecturerById(id);
            var updatedCourseResource = _mapper.Map<Course, SaveCourseResource>(updatedCourse);

            return Ok(updatedCourseResource);
        }

        /// <summary>
        /// Add new student to course
        /// </summary>
        /// <param name="courseId">Course Id</param>
        /// <param name="studentId">Student Id</param>
        /// <returns>A updated course with students</returns>
        /// <response code="200">Returns the updated course</response>
        /// <response code="409">If the student already study in course</response> 
        /// <response code="404">If the student or course with that id doesn't exist in database</response> 
        [HttpPut("addStudent/{courseId}/{studentId}")]
        public async Task<ActionResult<CourseWithStudentsResource>> AddStudentToCourse(int courseId, int studentId)
        {
            await _courseService.AddStudentToCourse(courseId, studentId);

            var updatedCourse = await _courseService.GetCourseWithStudentsById(courseId);
            var updatedCourseResource = _mapper.Map<Course, CourseWithStudentsResource>(updatedCourse);

            var students = updatedCourse.StudentCourses.Select(s => s.Student);
            var studentsResource = _mapper.Map<IEnumerable<Student>, IEnumerable<StudentResource>>(students);

            updatedCourseResource.Students = studentsResource;

            return Ok(updatedCourseResource);
        }

        /// <summary>
        /// Delete a course
        /// </summary>
        /// <param name="id">The course that need to delete</param>
        /// <returns></returns>
        /// <response code="204">Course is successfully deleted</response>
        /// <response code="404">Course with that id doesn't exist in database</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _courseService.GetCourseById(id);

            await _courseService.DeleteCourse(course);

            return NoContent();
        }
    }
}