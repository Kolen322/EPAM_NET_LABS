using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using module_20.DAL.Entities;
using System;

namespace module_20.DAL.EntityFramework
{
    internal class LectureConfiguration : IEntityTypeConfiguration<Lecture>
    {
        public void Configure(EntityTypeBuilder<Lecture> builder)
        {
            builder.HasIndex(lecture => lecture.Id);

            builder.HasData(
                new Lecture[]
                {
                    new Lecture { Id = 1, CourseId = 1, Date = new DateTime(2019, 9, 1), Name = "How to find a derivative?" },
                    new Lecture { Id = 2, CourseId = 1, Date = new DateTime(2019, 9, 8), Name = "Logarithmic Derivative" },
                    new Lecture { Id = 3, CourseId = 2, Date = new DateTime(2019, 9, 2), Name = "Introduction to the C# Language" },
                    new Lecture { Id = 4, CourseId = 2, Date = new DateTime(2019, 9, 9), Name = "Creating types in C#" },
                    new Lecture { Id = 5, CourseId = 3, Date = new DateTime(2019, 9, 3), Name = "Introduction to the Assembler Language" },
                    new Lecture { Id = 6, CourseId = 3, Date = new DateTime(2019, 9, 10), Name = "What is Register?" },
                    new Lecture { Id = 7, CourseId = 4, Date = new DateTime(2019, 9, 4), Name = "Present Simple" }
                });
        }
    }
}
