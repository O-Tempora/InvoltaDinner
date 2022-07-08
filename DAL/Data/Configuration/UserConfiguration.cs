using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Email).HasMaxLength(50);

            builder.Property(e => e.Name).HasMaxLength(45);

            builder.Property(e => e.Password).HasMaxLength(200);

            builder.Property(e => e.Role).HasMaxLength(45);

            builder.HasData(
                new User
                {
                    Id = 1,
                    Email = "dinneradmin@gmail.com",
                    Name = "Виталий Волков",
                    Password = "qwerty_Admin",
                    Role = "admin"
                },
                new User
                {
                    Id = 2,
                    Email = "dinnercook@gmail.com",
                    Name = "Вова Вист",
                    Password = "asdf_Cook",
                    Role = "cook"
                },
                new User
                {
                    Id = 3,
                    Email = "casualuser@gmail.com",
                    Name = "Алекс Дарксталкер98",
                    Password = "devkabezruki",
                    Role = "user"
                }
            );
    }
    }
}