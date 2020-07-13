using AutoMapper;
using module_20.DAL.Entities;
using module_20.Web.Resources;

namespace module_20.Web.Mapping
{
    /// <summary>
    /// AutoMapper profile for homework
    /// </summary>
    public class HomeworkProfile : Profile
    {
        /// <summary>
        /// Base constructor
        /// </summary>
        public HomeworkProfile()
        {
            CreateMap<Homework, HomeworkWithStudentsAndLectureResource>();
            CreateMap<Homework, SaveHomeworkResource>();
            CreateMap<Homework, HomeworkWithLectureResource>();
            CreateMap<Homework, HomeworkWitnStudentsResource>();
            CreateMap<Homework, SetMarkHomeworkResource>();

            CreateMap<HomeworkWithStudentsAndLectureResource, Homework>();
            CreateMap<SaveHomeworkResource, Homework>();
            CreateMap<HomeworkWithLectureResource, Homework>();
            CreateMap<HomeworkWitnStudentsResource, Homework>();
            CreateMap<SetMarkHomeworkResource, Homework>();
        }
    }

}
