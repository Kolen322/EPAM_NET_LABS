using System.Collections.Generic;

namespace module_20.Web.Resources
{
    /// <summary>
    /// Mapper resource with id, name, students for Course model
    /// </summary>
    public class CourseWithStudentsResource
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
        /// Students of course
        /// </summary>
        public IEnumerable<StudentResource> Students{get; set;}
    }
}
