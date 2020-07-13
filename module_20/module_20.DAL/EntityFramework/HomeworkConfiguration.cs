using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using module_20.DAL.Entities;

namespace module_20.DAL.EntityFramework
{
    internal class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder.HasIndex(homework => homework.Id);

            builder.HasData(
                new Homework[]
                {
                    new Homework {Id = 1, Task = "L01", LectureId = 1, Mark = 5, StudentId = 1},
                    new Homework {Id = 2, Task = "L02", LectureId = 2, Mark = 5, StudentId = 1},
                    new Homework {Id = 3, Task = "L03", LectureId = 3, Mark = 4, StudentId = 1},
                    new Homework {Id = 4, Task = "L04", LectureId = 4, Mark = 4, StudentId = 1},
                    new Homework {Id = 5, Task = "L01", LectureId = 1, Mark = 0, StudentId = 2},
                    new Homework {Id = 6, Task = "L02", LectureId = 2, Mark = 0, StudentId = 2},
                    new Homework {Id = 7, Task = "L03", LectureId = 3, Mark = 0, StudentId = 2},
                    new Homework {Id = 8, Task = "L04", LectureId = 4, Mark = 0, StudentId = 2},
                    new Homework {Id = 9, Task = "L03", LectureId = 3, Mark = 5, StudentId = 3},
                    new Homework {Id = 10, Task = "L04", LectureId = 4, Mark = 5, StudentId = 3},
                    new Homework {Id = 11, Task = "L05", LectureId = 5, Mark = 5, StudentId = 3},
                    new Homework {Id = 12, Task = "L06", LectureId = 6, Mark = 5, StudentId = 3},
                    new Homework {Id = 13, Task = "L03", LectureId = 3, Mark = 0, StudentId = 4},
                    new Homework {Id = 14, Task = "L04", LectureId = 4, Mark = 0, StudentId = 4},
                    new Homework {Id = 15, Task = "L05", LectureId = 5, Mark = 0, StudentId = 4},
                    new Homework {Id = 16, Task = "L06", LectureId = 6, Mark = 0, StudentId = 4},
                    new Homework {Id = 17, Task = "L05", LectureId = 5, Mark = 5, StudentId = 5},
                    new Homework {Id = 18, Task = "L06", LectureId = 6, Mark = 5, StudentId = 5},
                    new Homework {Id = 19, Task = "L07", LectureId = 7, Mark = 5, StudentId = 5},
                    new Homework {Id = 20, Task = "L05", LectureId = 5, Mark = 5, StudentId = 6},
                    new Homework {Id = 21, Task = "L06", LectureId = 6, Mark = 5, StudentId = 6},
                    new Homework {Id = 22, Task = "L07", LectureId = 7, Mark = 5, StudentId = 6},
                });
        }
    }
}
