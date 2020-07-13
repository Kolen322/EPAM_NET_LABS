using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using module_20.BLL.Interfaces;
using module_20.DAL.Entities;
using AutoMapper;
using module_20.Web.Resources;
using module_20.Web.Validators;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace module_20.Web.Controllers
{
    /// <summary>
    /// Controller to work with student
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [FormatFilter]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentController> _logger;

        /// <summary>
        /// Constructor with specified ICourseService, IMapper, ILogger
        /// </summary>
        /// <param name="studentService">Object that realize lecture service contract</param>
        /// <param name="mapper">Object that realize mapper contract</param>
        /// <param name="logger">Object that realize logger contract</param>
        public StudentController(IStudentService studentService, IMapper mapper, ILogger<StudentController> logger)
        {
            _mapper = mapper;
            _studentService = studentService;
            _logger = logger;
        }

        /// <summary>
        /// Get all students
        /// </summary>
        /// <returns>Collection of students</returns>
        /// <response code="200">Returns students collection</response>
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<StudentResource>>> GetAllStudents()
        {
            var students = await _studentService.GetAllStudents().ToListAsync();
            var studentsResource = _mapper.Map<IEnumerable<Student>, IEnumerable<StudentResource>>(students);

            return Ok(studentsResource);
        }

        /// <summary>
        /// Get all students with courses
        /// </summary>
        /// <returns>Collection of students with courses</returns>
        /// <response code="200">Returns students collection</response>
        [HttpGet("studentsWithCourses")]
        public async Task<ActionResult<IEnumerable<StudentWithCoursesResource>>> GetAllStudentsWithCourse()
        {
            var students = await _studentService.GetAllStudentsWithCourses().ToListAsync();
            var studentsResource = _mapper.Map<IEnumerable<Student>, IEnumerable<StudentWithCoursesResource>>(students);
            int i =0;
            foreach (var student in students)
            {
                var courses = student.StudentCourses.Select(c => c.Course);
                var coursesResource = _mapper.Map<IEnumerable<Course>,IEnumerable<CourseResource>>(courses);
                studentsResource.ElementAt(i).Courses = coursesResource;
                i++;
            }

            return Ok(studentsResource);
        }

        /// <summary>
        /// Get student by id
        /// </summary>
        /// <param name="id">Student Id</param>
        /// <returns>Student object</returns>
        /// <response code="200">Returns student object</response>
        /// <response code="404">If student with that id doesn't exist in database</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentResource>> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentById(id);
            var studentResource = _mapper.Map<Student, StudentResource>(student);

            return Ok(studentResource);
        }

        /// <summary>
        /// Get student with courses by id
        /// </summary>
        /// <param name="id">Student Id</param>
        /// <returns>Student with courses object</returns>
        /// <response code="200">Returns student object</response>
        /// <response code="404">If student with that id doesn't exist in database</response>
        [HttpGet("studentWithCourses/{id}")]
        public async Task<ActionResult<IEnumerable<StudentWithCoursesResource>>> GetStudentWithCoursesById(int id)
        {
            var student = await _studentService.GetStudentWithCoursesById(id);
            var studentResource = _mapper.Map<Student, StudentWithCoursesResource>(student);

            var courses = student.StudentCourses.Select(c => c.Course);
            var coursesResource = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseResource>>(courses);
            studentResource.Courses = coursesResource;

            return Ok(studentResource);
        }

        /// <summary>
        /// Get attendance of student by student name
        /// </summary>
        /// <param name="studentName">Student name</param>
        /// <returns>Attendance of student</returns>
        /// <remarks>
        /// Available two output formats: txt and json.
        /// For txt format put "txt". For json put "json"
        /// </remarks>
        /// <response code="200">Returns attendance collection</response>
        /// <response code="404">If student with that id doesn't exist in database</response>
        [HttpGet("getAttendanceByName/{studentName}.{format?}")]
        public async Task<ActionResult<IEnumerable<AttendanceOfStudentResource>>> GetAttendanceOfStudentByName(string studentName)
        {
            var student = await _studentService.GetAllStudentsWithLectures()
                .SingleOrDefaultAsync(s => s.Name == studentName);
            if (student == null)
                return NotFound();
            var attendance = student.StudentLectures;
            var attendaceResource = _mapper.Map<IEnumerable<StudentLecture>, IEnumerable<AttendanceOfStudentResource>>(attendance);
            _logger.LogInformation(DateTime.Now + $"\nGet attendance of {studentName} student");

            return Ok(attendaceResource);
        }

        /// <summary>
        /// Create a new student
        /// </summary>
        /// <param name="saveStudentResource">Information need to create a new lecture</param>
        /// <returns>A newly create lecture</returns>
        /// <remarks>Validation rules:
        /// 1. Name isn't empty and maximum length = 150
        /// 2. Email isn't empty
        /// 3. Mobile isn't empty and has +X (XXX) XXX-XX-XX format</remarks>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="400">If the item is null or validation isn't valid</response> 
        [HttpPost("")]
        public async Task<ActionResult<StudentResource>> CreateStudent([FromBody] SaveStudentResource saveStudentResource)
        {
            var validator = new SaveStudentResourceValidator();
            var validationResult = await validator.ValidateAsync(saveStudentResource);

            if (!validationResult.IsValid)
            {
                _logger.LogError(DateTime.Now + $"\nCreating student failed, validation isn't valid " + validationResult.Errors.ToString());
                return BadRequest(validationResult.Errors);
            }

            var studentToCreate = _mapper.Map<SaveStudentResource, Student>(saveStudentResource);

            var result = await _studentService.CreateStudent(studentToCreate);

            var createdStudent = _mapper.Map<Student, StudentResource>(result);

            return Ok(createdStudent);
        }

        /// <summary>
        /// Update a existing student
        /// </summary>
        /// <param name="id">Updated student id</param>
        /// <param name="saveStudentResource">Information need to update student</param>
        /// <returns>A updated student</returns>
        /// <remarks>Validation rules:
        /// 1. Name isn't empty and maximum length = 150
        /// 2. Email isn't empty
        /// 3. Mobile isn't empty and has +X (XXX) XXX-XX-XX format</remarks>
        /// <response code="200">Returns the updated item</response>
        /// <response code="400">If the item is null or validation isn't valid</response> 
        /// <response code="404">If student with that id doesn't exist in database</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<StudentResource>> UpdateStudent(int id, [FromBody] SaveStudentResource saveStudentResource)
        {
            var validator = new SaveStudentResourceValidator();
            var validationResult = await validator.ValidateAsync(saveStudentResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
            {
                _logger.LogError(DateTime.Now + $"\nUpdating student with {id} id failed, validation isn't valid " + validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            var studentToBeUpdate = await _studentService.GetStudentById(id);

            var student = _mapper.Map<SaveStudentResource, Student>(saveStudentResource);
            student.Id = id;

            await _studentService.UpdateStudent(student);

            var updatedStudent = await _studentService.GetStudentById(id);
            var updatedStudentResource = _mapper.Map<Student, StudentResource>(updatedStudent);

            return Ok(updatedStudentResource);
        }

        /// <summary>
        /// Delete a student
        /// </summary>
        /// <param name="id">The student that need to delete</param>
        /// <returns></returns>
        /// <response code="204">Student is successfully deleted</response>
        /// <response code="404">Student with that id doesn't exist in database</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _studentService.GetStudentById(id);

            await _studentService.DeleteStudent(student);

            return NoContent();
        }


    }
}