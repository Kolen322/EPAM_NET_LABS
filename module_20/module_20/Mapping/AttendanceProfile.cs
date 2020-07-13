using AutoMapper;
using module_20.DAL.Entities;
using module_20.Web.Resources;

namespace module_20.Web.Mapping
{
    /// <summary>
    /// AutoMapper profile for attendance
    /// </summary>
    public class AttendanceProfile : Profile
    {
        /// <summary>
        /// Base constructor
        /// </summary>
        public AttendanceProfile()
        {
            CreateMap<StudentLecture, AttendanceOfLectureResource>();
            CreateMap<StudentLecture, AttendanceOfStudentResource>();

            CreateMap<AttendanceOfLectureResource, StudentLecture>();
            CreateMap<AttendanceOfStudentResource, StudentLecture>();
        }
    }
}
