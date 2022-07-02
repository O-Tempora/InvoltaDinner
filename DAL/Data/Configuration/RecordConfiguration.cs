using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configuration
{
    public class RecordConfiguration : IEntityTypeConfiguration<Record>
    {
        public void Configure(EntityTypeBuilder<Record> builder)
        {
            builder.ToTable("record");

            builder.HasIndex(e => e.UserId, "user_idx");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Date).HasColumnType("datetime");

            builder.Property(e => e.Price).HasPrecision(5, 2);

            builder.HasOne(d => d.User)
                .WithMany(p => p.Records)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("userFK");
            
            builder.HasData(
                new Record
                {
                    Id = 1,
                    UserId = 3,
                    Price = 450,
                    Date = new DateTime(2022, 07, 11),
                    IsReady = 0
                },
                new Record
                {
                    Id = 2,
                    UserId = 3,
                    Price = 225,
                    Date = new DateTime(2022, 07, 12),
                    IsReady = 0
                }
            );
        }
    }
}