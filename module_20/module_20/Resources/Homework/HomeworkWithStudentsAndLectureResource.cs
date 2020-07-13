namespace module_20.Web.Resources
{
    /// <summary>
    /// Mapper resource with id, mark, task, lecture, student for Homework model
    /// </summary>
    public class HomeworkWithStudentsAndLectureResource
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Mark
        /// </summary>
        public int Mark { get; set; }
        /// <summary>
        /// Task of homework
        /// </summary>
        public string Task { get; set; }
        /// <summary>
        /// Student
        /// </summary>
        public StudentResource Student { get; set; }
        /// <summary>
        /// Lecture
        /// </summary>
        public LectureResource Lecture { get; set; }

    }
}
