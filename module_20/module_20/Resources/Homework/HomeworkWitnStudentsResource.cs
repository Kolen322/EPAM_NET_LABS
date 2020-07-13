namespace module_20.Web.Resources
{
    /// <summary>
    /// Mapper resource with id, mark, task, student for Homework model
    /// </summary>
    public class HomeworkWitnStudentsResource
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
        /// Task
        /// </summary>
        public string Task { get; set; }
        /// <summary>
        /// Student
        /// </summary>
        public StudentResource Student { get; set; }
    }
}
