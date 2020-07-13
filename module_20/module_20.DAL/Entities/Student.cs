using System.Collections.Generic;

namespace module_20.DAL.Entities
{
    /// <summary>
    /// Model that represent the student
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Student Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of student
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email of student
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Mobile of student
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// Attendance at lectures of student
        /// </summary>
        public List<StudentLecture> StudentLectures { get; set; }
        /// <summary>
        /// Courses attended by student
        /// </summary>
        public List<StudentCourse> StudentCourses { get; set; }
        public Student()
        {
            StudentCourses = new List<StudentCourse>();
            StudentLectures = new List<StudentLecture>();
        }
    }
}
