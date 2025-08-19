using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsWebSiteApi.Domain.Entities.User;
namespace NewsWebSiteApi.Infrastructure.Configuration.UserConfiguration;

public class UserConfiguraion :IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
               .IsRequired()
               .HasMaxLength(50).IsUnicode(true);

        builder.Property(x => x.LastName)
               .IsRequired()
               .HasMaxLength(50).IsUnicode(true);

        builder.Property(x => x.PhoneNumber)
               .IsRequired()
               .HasMaxLength(15);


        builder.HasMany(u => u.Articles)
            .WithOne(a => a.User)
            .HasForeignKey(a=>a.AuthorId);
        
        
        builder.ToTable("Users");
    }
}
