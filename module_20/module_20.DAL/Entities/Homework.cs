

namespace module_20.DAL.Entities
{
    /// <summary>
    /// Model that represent the homework
    /// </summary>
    public class Homework
    {
        /// <summary>
        /// Homework Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Assessment of homework
        /// </summary>
        public int Mark { get; set; }
        /// <summary>
        /// Task of homework
        /// </summary>
        public string Task { get; set; }
        /// <summary>
        /// Lecture Id
        /// </summary>
        public int LectureId { get; set; }
        /// <summary>
        /// Lecture on which homework was set 
        /// </summary>
        public Lecture Lecture { get; set; }
        /// <summary>
        /// Student Id
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// Student who does homework
        /// </summary>
        public Student Student { get; set; }
    }
}
