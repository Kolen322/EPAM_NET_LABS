using System.Collections.Generic;

namespace module_20.DAL.Entities
{
    /// <summary>
    /// Model that represent the course
    /// </summary>
    public class Course
    {
        /// <summary>
        /// Course Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of course
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Lecturer id
        /// </summary>
        public int LecturerId { get; set; }
        /// <summary>
        /// Lecturer of course
        /// </summary>
        public Lecturer Lecturer { get; set; }
        /// <summary>
        /// Students at the course
        /// </summary>
        public List<StudentCourse> StudentCourses { get; set; }
        /// <summary>
        /// Lectures that took place on the course
        /// </summary>
        public List<Lecture> Lectures { get; set; }
        public Course()
        {
            StudentCourses = new List<StudentCourse>();
        }
    }
}
