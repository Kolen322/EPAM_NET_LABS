using AutoMapper;
using module_20.DAL.Entities;
using module_20.Web.Resources;

namespace module_20.Web.Mapping
{
    /// <summary>
    /// AutoMapper profile for lecturer
    /// </summary>
    public class LecturerProfile : Profile
    {
        /// <summary>
        /// Base constructor
        /// </summary>
        public LecturerProfile()
        {
            CreateMap<Lecturer, LecturerResource>();
            CreateMap<Lecturer, SaveLecturerResource>();
            CreateMap<Lecturer, LecturerWithCourseResource>();

            CreateMap<LecturerResource, Lecturer>();
            CreateMap<SaveLecturerResource, Lecturer>();
            CreateMap<LecturerWithCourseResource, Lecturer>();
        }
    }
}
