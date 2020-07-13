namespace module_20.Web.Resources
{
    /// <summary>
    /// Mapper resource with id,name,lecturer for Course model
    /// </summary>
    public class CourseWithLecturerResource
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of course
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Lecturer of course
        /// </summary>
        public LecturerResource Lecturer { get; set; }
    }
}
