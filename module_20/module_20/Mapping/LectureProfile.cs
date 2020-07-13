using AutoMapper;
using module_20.DAL.Entities;
using module_20.Web.Resources;


namespace module_20.Web.Mapping
{
    /// <summary>
    /// AutoMapper profile for lecture
    /// </summary>
    public class LectureProfile : Profile
    {
        /// <summary>
        /// Base constructor
        /// </summary>
        public LectureProfile()
        {
            CreateMap<Lecture, LectureWithCourseResource>();
            CreateMap<Lecture, LectureResource>();
            CreateMap<Lecture, SaveLectureResource>();

            CreateMap<LectureWithCourseResource, Lecture>();
            CreateMap<LectureResource, Lecture>();
            CreateMap<SaveLectureResource, Lecture>();
        }
    }
}
