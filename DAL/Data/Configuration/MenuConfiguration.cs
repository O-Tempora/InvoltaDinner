using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configuration
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            
            builder.ToTable("menu");
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Date).HasColumnType("datetime");

            builder.HasData(
                new Menu { Id = 1, Date = new DateTime(2022, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 2, Date = new DateTime(2022, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 3, Date = new DateTime(2022, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 4, Date = new DateTime(2022, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 5, Date = new DateTime(2022, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 6, Date = new DateTime(2022, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)0 },
                new Menu { Id = 7, Date = new DateTime(2022, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)0 },
                new Menu { Id = 8, Date = new DateTime(2022, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 9, Date = new DateTime(2022, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 10, Date = new DateTime(2022, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 11, Date = new DateTime(2022, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 12, Date = new DateTime(2022, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 13, Date = new DateTime(2022, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)0 },
                new Menu { Id = 14, Date = new DateTime(2022, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)0 },
                new Menu { Id = 15, Date = new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 16, Date = new DateTime(2022, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 17, Date = new DateTime(2022, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 18, Date = new DateTime(2022, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 19, Date = new DateTime(2022, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 20, Date = new DateTime(2022, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)0 },
                new Menu { Id = 21, Date = new DateTime(2022, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)0 },
                new Menu { Id = 22, Date = new DateTime(2022, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 23, Date = new DateTime(2022, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 24, Date = new DateTime(2022, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 25, Date = new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 26, Date = new DateTime(2022, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 27, Date = new DateTime(2022, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)0 },
                new Menu { Id = 28, Date = new DateTime(2022, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)0 },
                new Menu { Id = 29, Date = new DateTime(2022, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 30, Date = new DateTime(2022, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 31, Date = new DateTime(2022, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 32, Date = new DateTime(2022, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 33, Date = new DateTime(2022, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 34, Date = new DateTime(2022, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)0 },
                new Menu { Id = 35, Date = new DateTime(2022, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)0 },
                new Menu { Id = 36, Date = new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 37, Date = new DateTime(2022, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 38, Date = new DateTime(2022, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 39, Date = new DateTime(2022, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 40, Date = new DateTime(2022, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 41, Date = new DateTime(2022, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)0 },
                new Menu { Id = 42, Date = new DateTime(2022, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)0 },
                new Menu { Id = 43, Date = new DateTime(2022, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 44, Date = new DateTime(2022, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 45, Date = new DateTime(2022, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 46, Date = new DateTime(2022, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 47, Date = new DateTime(2022, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 48, Date = new DateTime(2022, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)0 },
                new Menu { Id = 49, Date = new DateTime(2022, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)0 },
                new Menu { Id = 50, Date = new DateTime(2022, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 51, Date = new DateTime(2022, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 52, Date = new DateTime(2022, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 53, Date = new DateTime(2022, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 54, Date = new DateTime(2022, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)0 },
                new Menu { Id = 55, Date = new DateTime(2022, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)0 },
                new Menu { Id = 56, Date = new DateTime(2022, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 57, Date = new DateTime(2022, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 58, Date = new DateTime(2022, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 59, Date = new DateTime(2022, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 60, Date = new DateTime(2022, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)1 },
                new Menu { Id = 61, Date = new DateTime(2022, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)0 },
                new Menu { Id = 62, Date = new DateTime(2022, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = (sbyte)0 }
            );
        }
    }
}