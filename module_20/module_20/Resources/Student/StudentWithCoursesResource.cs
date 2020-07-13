using System.Collections.Generic;

namespace module_20.Web.Resources
{
    /// <summary>
    /// Mapper resource with id, name, courses for student model
    /// </summary>
    public class StudentWithCoursesResource
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of student
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Courses of student
        /// </summary>
        public IEnumerable<CourseResource> Courses { get; set; }
    }
}
