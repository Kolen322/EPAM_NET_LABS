using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using module_20.DAL.Entities;

namespace module_20.DAL.EntityFramework
{
    internal class StudentLectureConfiguration : IEntityTypeConfiguration<StudentLecture>
    {
        public void Configure(EntityTypeBuilder<StudentLecture> builder)
        {
            builder
                .HasKey(t => new { t.StudentId, t.LectureId });

            builder
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentLectures)
                .HasForeignKey(sc => sc.StudentId);

            builder
                .HasOne(sc => sc.Lecture)
                .WithMany(l => l.StudentLectures)
                .HasForeignKey(sc => sc.LectureId);
            builder.HasData(
                new StudentLecture[]
                {
                    new StudentLecture { LectureId = 1, StudentId = 1, Attendance = true},
                    new StudentLecture { LectureId = 2, StudentId = 1, Attendance = true},
                    new StudentLecture { LectureId = 3, StudentId = 1, Attendance = true},
                    new StudentLecture { LectureId = 4, StudentId = 1, Attendance = true},
                    new StudentLecture { LectureId = 1, StudentId = 2, Attendance = false},
                    new StudentLecture { LectureId = 2, StudentId = 2, Attendance = false},
                    new StudentLecture { LectureId = 3, StudentId = 2, Attendance = false},
                    new StudentLecture { LectureId = 4, StudentId = 2, Attendance = false},
                    new StudentLecture { LectureId = 3, StudentId = 3, Attendance = true},
                    new StudentLecture { LectureId = 4, StudentId = 3, Attendance = true},
                    new StudentLecture { LectureId = 5, StudentId = 3, Attendance = true},
                    new StudentLecture { LectureId = 6, StudentId = 3, Attendance = true},
                    new StudentLecture { LectureId = 3, StudentId = 4, Attendance = true},
                    new StudentLecture { LectureId = 4, StudentId = 4, Attendance = true},
                    new StudentLecture { LectureId = 5, StudentId = 4, Attendance = true},
                    new StudentLecture { LectureId = 6, StudentId = 4, Attendance = true},
                    new StudentLecture { LectureId = 5, StudentId = 5, Attendance = true},
                    new StudentLecture { LectureId = 6, StudentId = 5, Attendance = true},
                    new StudentLecture { LectureId = 7, StudentId = 5, Attendance = true},
                    new StudentLecture { LectureId = 5, StudentId = 6, Attendance = true},
                    new StudentLecture { LectureId = 6, StudentId = 6, Attendance = true},
                    new StudentLecture { LectureId = 7, StudentId = 6, Attendance = false},
                });
        }
    }
}
