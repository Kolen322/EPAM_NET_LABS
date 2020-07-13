namespace module_20.DAL.Entities
{
    /// <summary>
    /// Model taht represent many-to-many relationship student and lecture tables
    /// </summary>
    public class StudentLecture
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
        /// Lecture Id
        /// </summary>
        public int LectureId { get; set; }
        /// <summary>
        /// Lecture object
        /// </summary>
        public Lecture Lecture { get; set; }
        /// <summary>
        /// Attendance of student at lecture
        /// </summary>
        public bool Attendance { get; set; }
        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            int hash = StudentId + LectureId;
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
                StudentLecture sl = (StudentLecture)obj;
                return LectureId == sl.LectureId
                    && StudentId == sl.StudentId
                    && Attendance == sl.Attendance;
            }
        }
    }
}
