using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configuration
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            
            builder.ToTable("menu");
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.Date).HasColumnType("datetime");

            builder.HasData(
                new Menu
                {
                    Id = 1,
                    Date = new DateTime(2022, 07, 11),
                    IsActive = 1
                },
                new Menu
                {
                    Id = 2,
                    Date = new DateTime(2022, 07, 12),
                    IsActive = 1
                },
                new Menu
                {
                    Id = 3,
                    Date = new DateTime(2022, 07, 13),
                    IsActive = 1
                },
                new Menu
                {
                    Id = 4,
                    Date = new DateTime(2022, 07, 14),
                    IsActive = 1
                },
                new Menu
                {
                    Id = 5,
                    Date = new DateTime(2022, 07, 15),
                    IsActive = 1
                },
                new Menu
                {
                    Id = 6,
                    Date = new DateTime(2022, 07, 16),
                    IsActive = 0
                },
                new Menu
                {
                    Id = 7,
                    Date = new DateTime(2022, 07, 17),
                    IsActive = 0
                }
            );
        }
    }
}