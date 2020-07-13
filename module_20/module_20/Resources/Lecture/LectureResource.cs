using System;

namespace module_20.Web.Resources
{
    /// <summary>
    /// Mapper resource with id, name, date for lecture model
    /// </summary>
    public class LectureResource
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of lecture
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Date of lecture
        /// </summary>
        public DateTime Date { get; set; }
    }
}
