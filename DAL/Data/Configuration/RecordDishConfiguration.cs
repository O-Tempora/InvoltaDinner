using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configuration
{
    public class RecordDishConfiguration : IEntityTypeConfiguration<RecordDish>
    {
        public void Configure(EntityTypeBuilder<RecordDish> builder)
        {
            builder.ToTable("record_dish");

            builder.HasIndex(e => e.Dish, "dish_idx");

            builder.HasIndex(e => e.Record, "recordId_idx");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.HasOne(d => d.DishNavigation)
                .WithMany(p => p.RecordDishes)
                .HasForeignKey(d => d.Dish)
                .HasConstraintName("dishId");

            builder.HasOne(d => d.RecordNavigation)
                .WithMany(p => p.RecordDishes)
                .HasForeignKey(d => d.Record)
                .HasConstraintName("recordId");

            builder.HasData(
                new RecordDish
                {
                    Id = 1,
                    Dish = 1,
                    Record = 1
                },
                new RecordDish
                {
                    Id = 2,
                    Dish = 8,
                    Record = 1
                },
                new RecordDish
                {
                    Id = 3,
                    Dish = 9,
                    Record = 2
                }
            );
        }
    }
}