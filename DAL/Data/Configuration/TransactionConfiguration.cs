using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configuration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("transaction");

            builder.HasIndex(e => e.Admin, "admin_idx");

            builder.HasIndex(e => e.User, "user_idx");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Date).HasColumnType("datetime");

            builder.Property(e => e.Price).HasPrecision(5, 2);

            builder.HasOne(d => d.AdminNavigation)
                .WithMany(p => p.TransactionAdminNavigations)
                .HasForeignKey(d => d.Admin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("admin");

            builder.HasOne(d => d.UserNavigation)
                .WithMany(p => p.TransactionUserNavigations)
                .HasForeignKey(d => d.User)
                .HasConstraintName("user");
            
            builder.HasData(
                new Transaction
                {
                    Id = 1,
                    User = 3,
                    Admin = 1,
                    Price = 225,
                    Date = new DateTime(2022, 07, 12) 
                }
            );
        }
    }
}