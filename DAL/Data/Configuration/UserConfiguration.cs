using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Email).HasMaxLength(50);

            builder.Property(e => e.Name).HasMaxLength(45);

            builder.Property(e => e.Password).HasMaxLength(200);

            builder.Property(e => e.Role).HasMaxLength(45);

            builder.HasData(
                new
                { Id = 8, Email = "reksmbd@gmail.com", IsApproved = (sbyte)1, Name = "MishaBausov", Password = "1U+u9QwJ8SdXuiRip3b83S7jiu06Z0PxlaPHFOJZJ+Q=:tiUz98Ow0IbpP7gWSLBCcA==", Role = "user" },
                new
                { Id = 9, Email = "admin@gmail.com", IsApproved = (sbyte)1, Name = "Admin1", Password = "8eqn6A6N11WY0k4j8PLlVfcmDvnUQZJOvTtxdBYtINA=:5tZTJitFXi/473n+fWFzog==", Role = "admin" },
                new
                { Id = 10, Email = "cook@gmail.com", IsApproved = (sbyte)1, Name = "Cook1", Password = "ucPtmgnShnsbFBQVZg7kNukEDDluMTr2/fYAq3odDF8=:amw/M3NvUh1kzCQkIJnVIg==", Role = "cook" }
            );
    }
    }
}