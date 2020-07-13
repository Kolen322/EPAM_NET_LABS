using System;

namespace module_20.Web.Resources
{
    /// <summary>
    /// Mapper resource for creating a object of lecture model
    /// </summary>
    public class SaveLectureResource
    {
        /// <summary>
        /// Name of lecture
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Date of lecture
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Course Id
        /// </summary>
        public int CourseId { get; set; }
    }
}
