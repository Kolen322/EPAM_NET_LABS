using System.Collections.Generic;

namespace module_20.DAL.Entities
{
    /// <summary>
    /// Model that represent the lecturer
    /// </summary>
    public class Lecturer
    {
        /// <summary>
        /// Lecturer Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of lecturer
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email of lecturer
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Mobile of lecturer
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// Courses which lecturer giving a lecture
        /// </summary>
        public List<Course> Courses { get; set; }
    }
}
