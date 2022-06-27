using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DAL.Entities
{
    public partial class DinnerContext : IdentityDbContext<User>
    {
        public DinnerContext()
        {
        }

        public DinnerContext(DbContextOptions<DinnerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DinnerMenu> DinnerMenus { get; set; } = null!;
        public virtual DbSet<Dish> Dishes { get; set; } = null!;
        public virtual DbSet<DishMenu> DishMenus { get; set; } = null!;
        public virtual DbSet<Record> Records { get; set; } = null!;
        public virtual DbSet<RecordDish> RecordDishes { get; set; } = null!;
        public new virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=Dinner;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DinnerMenu>(entity =>
            {
                entity.ToTable("DinnerMenu");

                entity.Property(e => e.Date).HasColumnType("date");
            });

            modelBuilder.Entity<Dish>(entity =>
            {
                entity.ToTable("Dish");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<DishMenu>(entity =>
            {
                entity.ToTable("Dish_Menu");

                entity.HasOne(d => d.DishNavigation)
                    .WithMany(p => p.DishMenus)
                    .HasForeignKey(d => d.Dish)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dish_Menu_Dish");

                entity.HasOne(d => d.MenuNavigation)
                    .WithMany(p => p.DishMenus)
                    .HasForeignKey(d => d.Menu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dish_Menu_DinnerMenu");
            });

            modelBuilder.Entity<Record>(entity =>
            {
                entity.ToTable("Record");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Record_User");
            });

            modelBuilder.Entity<RecordDish>(entity =>
            {
                entity.ToTable("Record_Dish");

                entity.HasOne(d => d.DishNavigation)
                    .WithMany(p => p.RecordDishes)
                    .HasForeignKey(d => d.Dish)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Record_Dish_Dish");

                entity.HasOne(d => d.RecordNavigation)
                    .WithMany(p => p.RecordDishes)
                    .HasForeignKey(d => d.Record)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Record_Dish_Record");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Debt).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
