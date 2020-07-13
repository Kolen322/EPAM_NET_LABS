using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using module_20.BLL.Interfaces;
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
    /// Controller to work with lecturer
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LecturerController : ControllerBase
    {
        private readonly ILecturerService _lecturerService;
        private readonly IMapper _mapper;
        private readonly ILogger<LecturerController> _logger;

        /// <summary>
        /// Constructor with specified ICourseService, IMapper, ILogger
        /// </summary>
        /// <param name="lecturerService">Object that realize lecturer service contract</param>
        /// <param name="mapper">Object that realize mapper contract</param>
        /// <param name="logger">Object that realize logger contract</param>
        public LecturerController(ILecturerService lecturerService, IMapper mapper, ILogger<LecturerController> logger)
        {
            _mapper = mapper;
            _lecturerService = lecturerService;
            _logger = logger;
        }

        /// <summary>
        /// Get all lecturers
        /// </summary>
        /// <returns>Collection of lecturers</returns>
        /// <response code="200">Returns lecturers collection</response>
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<LecturerResource>>> GetAllLecturers()
        {
            var lecturers = await _lecturerService.GetAllLecturers().ToListAsync();
            var lecturersResource = _mapper.Map<IEnumerable<Lecturer>, IEnumerable<LecturerResource>>(lecturers);

            return Ok(lecturersResource);
        }

        /// <summary>
        /// Get all lecturers with courses
        /// </summary>
        /// <returns>Collection of lecturers with courses</returns>
        /// <response code="200">Returns lecturers collection</response>
        [HttpGet("lecturersWithCourses")]
        public async Task<ActionResult<IEnumerable<LecturerWithCourseResource>>> GetAllLecturersWithCourses()
        {
            var lecturers = await _lecturerService.GetAllLecturersWithCourses().ToListAsync();
            var lecturersResource = _mapper.Map<IEnumerable<Lecturer>,IEnumerable< LecturerWithCourseResource >> (lecturers);

            return Ok(lecturersResource);
        }

        /// <summary>
        /// Get lecturer with by id
        /// </summary>
        /// <param name="id">Lecturer Id</param>
        /// <returns>Lecturer object</returns>
        /// <response code="200">Returns lecturer object</response>
        /// <response code="404">If lecturer with that id doesn't exist in database</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<LecturerResource>> GetLecturerById(int id)
        {
            var lecturer = await _lecturerService.GetLecturerById(id);
            var lecturerResource = _mapper.Map<Lecturer, LecturerResource>(lecturer);

            return Ok(lecturerResource);
        }

        /// <summary>
        /// Get lecturer with courses with by id
        /// </summary>
        /// <param name="id">Lecturer Id</param>
        /// <returns>Lecturer with courses object</returns>
        /// <response code="200">Returns lecturer object</response>
        /// <response code="404">If lecturer with that id doesn't exist in database</response>
        [HttpGet("lecturerWithCourses/{id}")]
        public async Task<ActionResult<LecturerWithCourseResource>> GetLecturerWithCourseById(int id)
        {
            var lecturer = await _lecturerService.GetLecturerWithCoursesById(id);
            var lecturerResource = _mapper.Map<Lecturer, LecturerWithCourseResource>(lecturer);

            return Ok(lecturerResource);
        }

        /// <summary>
        /// Create a new lecturer
        /// </summary>
        /// <param name="saveLecturerResource">Information need to create a new lecturer</param>
        /// <returns>A newly create lecturer</returns>
        /// <remarks>Validation rules:
        /// 1. Name isn't empty and maximum length = 150
        /// 2. Email isn't empty
        /// 3. Mobile isn't empty and has +X (XXX) XXX-XX-XX format</remarks>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="400">If the item is null or validation isn't valid</response> 
        [HttpPost("")]
        public async Task<ActionResult<LecturerResource>> CreateLecturer([FromBody] SaveLecturerResource saveLecturerResource)
        {
            var validator = new SaveLecturerResourceValidator();
            var validationResult = await validator.ValidateAsync(saveLecturerResource);

            if (!validationResult.IsValid)
            {
                _logger.LogError(DateTime.Now + $"\nCreating lecturer failed, validation isn't valid " + validationResult.Errors.ToString());
                return BadRequest(validationResult.Errors);
            }

            var lecturerToCreate = _mapper.Map<SaveLecturerResource, Lecturer>(saveLecturerResource);

            var result = await _lecturerService.CreateLecturer(lecturerToCreate);

            var createdLecturer = _mapper.Map<Lecturer, LecturerResource>(result);

            return Ok(result);
        }

        /// <summary>
        /// Update a existing lecturer
        /// </summary>
        /// <param name="id">Updated lecturer id</param>
        /// <param name="saveLecturerResource">Information need to update lecturer</param>
        /// <returns>A updated lecturer</returns>
        /// <remarks>Validation rules:
        /// 1. Name isn't empty and maximum length = 150
        /// 2. Email isn't empty
        /// 3. Mobile isn't empty and has +X (XXX) XXX-XX-XX format</remarks>
        /// <response code="200">Returns the updated item</response>
        /// <response code="400">If the item is null or validation isn't valid</response> 
        /// <response code="404">If lecturer with that id doesn't exist in database</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<LecturerResource>> UpdateLecturer(int id, [FromBody] SaveLecturerResource saveLecturerResource)
        {
            var validator = new SaveLecturerResourceValidator();
            var validationResult = await validator.ValidateAsync(saveLecturerResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
            {
                _logger.LogError(DateTime.Now + $"\nUpdating lecturer with {id} id failed, validation isn't valid " + validationResult.Errors.ToString());
                return BadRequest(validationResult.Errors);
            }

            var lecturerToBeUpdate = await _lecturerService.GetLecturerById(id);

            var lecturer = _mapper.Map<SaveLecturerResource, Lecturer>(saveLecturerResource);
            lecturer.Id = id;

            await _lecturerService.UpdateLecturer(lecturer);

            var updatedLecturer = await _lecturerService.GetLecturerById(id);
            var updatedLecturerResource = _mapper.Map<Lecturer, LecturerResource>(updatedLecturer);

            return Ok(updatedLecturerResource);
        }

        /// <summary>
        /// Delete a lecturer
        /// </summary>
        /// <param name="id">The lecturer that need to delete</param>
        /// <returns></returns>
        /// <response code="204">Lecturer is successfully deleted</response>
        /// <response code="404">Lecturer with that id doesn't exist in database</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLecturer(int id)
        {
            var lecturer = await _lecturerService.GetLecturerById(id);

            await _lecturerService.DeleteLecturer(lecturer);

            return NoContent();
        }
    }
}