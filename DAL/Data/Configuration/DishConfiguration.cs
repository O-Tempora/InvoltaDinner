using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configuration
{
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.ToTable("dish");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).HasMaxLength(50);

            builder.Property(e => e.Price).HasPrecision(5, 2);

            builder.HasData(
                new Dish
                {
                    Id = 1,
                    Name = "Борщ",
                    Price = 200,
                    Position = 1
                },
                new Dish
                {
                    Id = 2,
                    Name = "Окрошка",
                    Price = 200,
                    Position = 1
                },
                new Dish
                {
                    Id = 3,
                    Name = "Солянка",
                    Price = 220,
                    Position = 1
                },
                new Dish
                {
                    Id = 4,
                    Name = "Щи из свежей капустой",
                    Price = 200,
                    Position = 1
                },
                new Dish
                {
                    Id = 5,
                    Name = "Рассольник",
                    Price = 200,
                    Position = 1
                },
                new Dish
                {
                    Id = 6,
                    Name = "Уха",
                    Price = 220,
                    Position = 1
                },
                new Dish
                {
                    Id = 7,
                    Name = "Лапша с курицей",
                    Price = 180,
                    Position = 1
                },
                new Dish
                {
                    Id = 8,
                    Name = "Паста Карбонара",
                    Price = 250,
                    Position = 2
                },
                new Dish
                {
                    Id = 9,
                    Name = "Пюре с котлетой",
                    Price = 225,
                    Position = 2
                },
                new Dish
                {
                    Id = 10,
                    Name = "Хинкали",
                    Price = 230,
                    Position = 2
                },
                new Dish
                {
                    Id = 11,
                    Name = "Макароны с курицей",
                    Price = 180,
                    Position = 2
                }               
            );
        }
    }
}