using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using module_20.DAL.Entities;

namespace module_20.DAL.EntityFramework
{
    internal class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasIndex(course => course.Id);

            builder.HasData(
               new Course[]
               {
                   new Course {Id=1, Name="Mathematics", LecturerId=1},
                   new Course {Id=2, Name="C#", LecturerId=2},
                   new Course {Id=3, Name="Assembler x86", LecturerId=3},
                   new Course {Id=4, Name="English Language", LecturerId=4}
               });
        }
    }
}
