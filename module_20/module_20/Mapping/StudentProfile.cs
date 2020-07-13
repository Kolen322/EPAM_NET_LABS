using AutoMapper;
using module_20.DAL.Entities;
using module_20.Web.Resources;

namespace module_20.Web.Mapping
{
    /// <summary>
    /// AutoMapper profile for student
    /// </summary>
    public class StudentProfile : Profile
    {
        /// <summary>
        /// Base constructor
        /// </summary>
        public StudentProfile()
        {
            CreateMap<Student, StudentResource>();
            CreateMap<Student, SaveStudentResource>();
            CreateMap<Student, StudentWithCoursesResource>();
            CreateMap<Student, StudentWithNameOnlyResource>();

            CreateMap<StudentResource, Student>();
            CreateMap<SaveStudentResource, Student>();
            CreateMap<StudentWithCoursesResource, Student>();
            CreateMap<StudentWithNameOnlyResource, Student>();
        }
    }
}
