using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using module_20.DAL.Entities;

namespace module_20.DAL.EntityFramework
{
    internal class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder
                .HasKey(t => new { t.StudentId, t.CourseId });

            builder
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);

            builder
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);
            builder.HasData(
                new StudentCourse[]
                {
                    new StudentCourse {CourseId = 1, StudentId = 1},
                    new StudentCourse {CourseId = 2, StudentId = 1},
                    new StudentCourse {CourseId = 1, StudentId = 2},
                    new StudentCourse {CourseId = 2, StudentId = 2},
                    new StudentCourse {CourseId = 2, StudentId = 3},
                    new StudentCourse {CourseId = 3, StudentId = 3},
                    new StudentCourse {CourseId = 2, StudentId = 4},
                    new StudentCourse {CourseId = 3, StudentId = 4},
                    new StudentCourse {CourseId = 3, StudentId = 5},
                    new StudentCourse {CourseId = 4, StudentId = 5},
                    new StudentCourse {CourseId = 3, StudentId = 6},
                    new StudentCourse {CourseId = 4, StudentId = 6}
                });
        }
    }
}
