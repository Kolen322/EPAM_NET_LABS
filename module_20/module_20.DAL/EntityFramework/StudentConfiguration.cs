using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using module_20.DAL.Entities;

namespace module_20.DAL.EntityFramework
{
    internal class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasIndex(student => student.Id);

            _ = builder.HasData(
               new Student[]
               {
                    new Student { Id = 1, Name = "Vasyukhin Nikita", Email="kolen322@yandex.ru", Mobile="+7 (931) 945-23-45"},
                    new Student { Id = 2, Name = "Khebnev Pavel", Email="khebnev@mail.ru", Mobile="+7 (931) 965-23-45"},
                    new Student { Id = 3, Name = "Medvedev Ivan", Email="medvedevI@mail.ru", Mobile="+7 (931) 949-23-45"},
                    new Student { Id = 4, Name = "Ivanutin Artem", Email="ivanutinA@mail.ru", Mobile="+7 (931) 915-23-45"},
                    new Student { Id = 5, Name = "Malinin Vadim", Email="malininV@mail.ru", Mobile="+7 (931) 945-63-45"},
                    new Student { Id = 6, Name = "Suetova Elizavetha", Email="suetovaE@mail.ru", Mobile="+7 (931) 948-23-45"}
               });
        }
    }
}
