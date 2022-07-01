using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.Data
{
    public partial class dinnerContext : DbContext
    {
        public dinnerContext()
        {
        }

        public dinnerContext(DbContextOptions<dinnerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dish> Dishes { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<MenuDish> MenuDishes { get; set; } = null!;
        public virtual DbSet<Record> Records { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;user=root;password=1944Rommel1944;database=dinner", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Dish>(entity =>
            {
                entity.ToTable("dish");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Price).HasPrecision(5, 2);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("menu");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<MenuDish>(entity =>
            {
                entity.ToTable("menu_dish");

                entity.HasIndex(e => e.Dish, "dish_idx");

                entity.HasIndex(e => e.Menu, "menu_idx");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.DishNavigation)
                    .WithMany(p => p.MenuDishes)
                    .HasForeignKey(d => d.Dish)
                    .HasConstraintName("dish");

                entity.HasOne(d => d.MenuNavigation)
                    .WithMany(p => p.MenuDishes)
                    .HasForeignKey(d => d.Menu)
                    .HasConstraintName("menu");
            });

            modelBuilder.Entity<Record>(entity =>
            {
                entity.ToTable("record");

                entity.HasIndex(e => e.MenuId, "menu_idx");

                entity.HasIndex(e => e.UserId, "user_idx");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Price).HasPrecision(4, 2);

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("menuFK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("userFK");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transaction");

                entity.HasIndex(e => e.Admin, "admin_idx");

                entity.HasIndex(e => e.User, "user_idx");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Price).HasPrecision(5, 2);

                entity.HasOne(d => d.AdminNavigation)
                    .WithMany(p => p.TransactionAdminNavigations)
                    .HasForeignKey(d => d.Admin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("admin");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.TransactionUserNavigations)
                    .HasForeignKey(d => d.User)
                    .HasConstraintName("user");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Debt).HasPrecision(6, 2);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(45);

                entity.Property(e => e.Password).HasMaxLength(45);

                entity.Property(e => e.Role).HasMaxLength(45);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
