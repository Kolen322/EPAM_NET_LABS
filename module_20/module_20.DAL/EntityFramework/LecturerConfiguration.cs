using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using module_20.DAL.Entities;

namespace module_20.DAL.EntityFramework
{
    internal class LecturerConfiguration : IEntityTypeConfiguration<Lecturer>
    {
        public void Configure(EntityTypeBuilder<Lecturer> builder)
        {
            builder.HasIndex(lecturer => lecturer.Id);

            builder.HasData(
                new Lecturer[]
                {
                    new Lecturer { Id=1, Name="Ivanov Ivan Ivanovich", Email="ivanov@epam.com", Mobile="+7 (931) 945-23-45"},
                    new Lecturer { Id=2, Name="Korobov Andrew Ivanovich", Email="korobov@epam.com", Mobile="+7 (931) 955-23-45"},
                    new Lecturer { Id=3, Name="Burmistrov Oleg Aleksandrovich", Email="burmistrov@epam.com", Mobile="+7 (931) 235-23-45"},
                    new Lecturer { Id=4, Name="Slavnov Nikita Alekseevich", Email="slavnov@epam.com", Mobile="+7 (931) 967-23-45"}
                });
        }
    }
}
