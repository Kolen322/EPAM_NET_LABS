namespace module_20.Web.Resources
{
    /// <summary>
    /// Mapper resource with student and attendance for StudentLecture model
    /// </summary>
    public class AttendanceOfStudentResource
    {
        /// <summary>
        /// Lecture
        /// </summary>
        public LectureResource Lecture { get; set; }
        /// <summary>
        /// Attendance of lecture
        /// </summary>
        public bool Attendance { get; set; }
    }
}
