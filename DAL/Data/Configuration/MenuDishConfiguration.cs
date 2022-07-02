using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configuration
{
    public class MenuDishConfiguration : IEntityTypeConfiguration<MenuDish>
    {
        public void Configure(EntityTypeBuilder<MenuDish> builder)
        {
            builder.ToTable("menu_dish");

            builder.HasIndex(e => e.Dish, "dish_idx");

            builder.HasIndex(e => e.Menu, "menu_idx");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.HasOne(d => d.DishNavigation)
                .WithMany(p => p.MenuDishes)
                .HasForeignKey(d => d.Dish)
                .HasConstraintName("dish");

            builder.HasOne(d => d.MenuNavigation)
                .WithMany(p => p.MenuDishes)
                .HasForeignKey(d => d.Menu)
                .HasConstraintName("menu");

            builder.HasData(
                new MenuDish
                {
                    Id = 1,
                    Dish = 1,
                    Menu = 1
                },
                new MenuDish
                {
                    Id = 2,
                    Dish = 8,
                    Menu = 1
                },
                new MenuDish
                {
                    Id = 3,
                    Dish = 2,
                    Menu = 2
                },
                new MenuDish
                {
                    Id = 4,
                    Dish = 9,
                    Menu = 2
                },
                new MenuDish
                {
                    Id = 5,
                    Dish = 3,
                    Menu = 3
                },
                new MenuDish
                {
                    Id = 6,
                    Dish = 10,
                    Menu = 3
                },
                new MenuDish
                {
                    Id = 7,
                    Dish = 4,
                    Menu = 4
                },
                new MenuDish
                {
                    Id = 8,
                    Dish = 11,
                    Menu = 4
                },
                new MenuDish
                {
                    Id = 9,
                    Dish = 5,
                    Menu = 5
                },
                new MenuDish
                {
                    Id = 10,
                    Dish = 8,
                    Menu = 5
                }
            );
        }
    }
}