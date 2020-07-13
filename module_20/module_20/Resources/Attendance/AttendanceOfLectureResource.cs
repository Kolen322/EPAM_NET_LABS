namespace module_20.Web.Resources
{
    /// <summary>
    /// Mapper resource with student and attendance for StudentLecture model
    /// </summary>
    public class AttendanceOfLectureResource
    {
        /// <summary>
        /// Student
        /// </summary>
        public StudentWithNameOnlyResource Student { get; set; }
        /// <summary>
        /// Attendance of student
        /// </summary>
        public bool Attendance { get; set; }
    }
}
