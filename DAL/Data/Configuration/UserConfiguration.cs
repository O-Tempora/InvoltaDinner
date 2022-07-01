using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Debt).HasPrecision(6, 2);

            builder.Property(e => e.Email).HasMaxLength(50);

            builder.Property(e => e.Name).HasMaxLength(45);

            builder.Property(e => e.Password).HasMaxLength(45);

            builder.Property(e => e.Role).HasMaxLength(45);
        }
    }
}