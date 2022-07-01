using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configuration
{
    public class RecordConfiguration : IEntityTypeConfiguration<Record>
    {
        public void Configure(EntityTypeBuilder<Record> builder)
        {
        builder.ToTable("record");

        builder.HasIndex(e => e.MenuId, "menu_idx");

        builder.HasIndex(e => e.UserId, "user_idx");

        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.Price).HasPrecision(4, 2);

        builder.HasOne(d => d.Menu)
            .WithMany(p => p.Records)
            .HasForeignKey(d => d.MenuId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("menuFK");

        builder.HasOne(d => d.User)
            .WithMany(p => p.Records)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("userFK");
        }
    }
}