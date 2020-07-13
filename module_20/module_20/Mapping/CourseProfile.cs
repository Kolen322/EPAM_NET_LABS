using AutoMapper;
using module_20.DAL.Entities;
using module_20.Web.Resources;

namespace module_20.Web.Mapping
{
    /// <summary>
    /// AutoMapper profile for course
    /// </summary>
    public class CourseProfile : Profile
    {
        /// <summary>
        /// Base constructor
        /// </summary>
        public CourseProfile()
        {
            CreateMap<Course, CourseResource>();
            CreateMap<Course, SaveCourseResource>();
            CreateMap<Course, CourseWithStudentsResource>();
            CreateMap<Course, CourseWithLecturerResource>();

            CreateMap<CourseResource, Course>();
            CreateMap<SaveCourseResource, Course>();
            CreateMap<CourseWithLecturerResource, Course>();
            CreateMap<CourseWithStudentsResource, Course>();
        }
    }
}
