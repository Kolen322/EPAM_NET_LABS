namespace module_20.Web.Resources
{
    /// <summary>
    /// Mapper resource for creating a object of Homework model
    /// </summary>
    public class SaveHomeworkResource
    {
        /// <summary>
        /// Mark
        /// </summary>
        public int Mark { get; set; }
        /// <summary>
        /// Task
        /// </summary>
        public string Task { get; set; }
        /// <summary>
        /// Student Id
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// Lecture Id
        /// </summary>
        public int LectureId { get; set; }
    }
}
