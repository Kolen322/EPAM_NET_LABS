namespace module_20.Web.Resources
{
    /// <summary>
    /// Mapper resource for creating object of Course model
    /// </summary>
    public class SaveCourseResource
    {
        /// <summary>
        /// Name of course
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Lecturer Id
        /// </summary>
        public int LecturerId { get; set; }
    }
}
