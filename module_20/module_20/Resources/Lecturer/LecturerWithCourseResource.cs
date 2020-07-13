using module_20.Web.Resources;
using System.Collections.Generic;

namespace module_20.Web.Resources
{
    /// <summary>
    /// Mapper resource with id, name, courses for lecturer model
    /// </summary>
    public class LecturerWithCourseResource
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of lecturer
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Course of lecturer
        /// </summary>
        public IEnumerable<CourseResource> Courses { get; set; }
    }
}
