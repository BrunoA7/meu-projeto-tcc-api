using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using webAPI_ASPNET.Models;

namespace webAPI_ASPNET.Data.Map
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.USERNAME).IsRequired().HasMaxLength(30);
            builder.Property(x => x.PASSWORD).IsRequired().HasMaxLength(30);
            builder.Property(x => x.FULLNAME).IsRequired().HasMaxLength(255);
            builder.Property(x => x.EMAIL).IsRequired().HasMaxLength(150);
        }
    }
}
