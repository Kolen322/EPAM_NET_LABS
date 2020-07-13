namespace module_20.DAL.Entities
{
    /// <summary>
    /// Model taht represent many-to-many relationship student and course tables
    /// </summary>
    public class StudentCourse
    {
        /// <summary>
        /// Student Id
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// Student object
        /// </summary>
        public Student Student { get; set; }
        /// <summary>
        /// Course Id
        /// </summary>
        public int CourseId { get; set; }
        /// <summary>
        /// Course object
        /// </summary>
        public Course Course { get; set; }
        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            int hash = StudentId + CourseId;
            return hash;
        }
        /// <summary>
        /// Determines whether two object instances are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                StudentCourse sc = (StudentCourse)obj;
                return CourseId == sc.CourseId && StudentId == sc.StudentId;
            }
        }
    }

}
