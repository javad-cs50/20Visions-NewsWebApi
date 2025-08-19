using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsWebSiteApi.Domain.Entities.Comment;

namespace NewsWebSiteApi.Infrastructure.Configuration.CommentConfiguration;

public class CommentConfiguraion : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
               .HasMaxLength(50).IsUnicode(true);

        builder.Property(x => x.LastName)
               .HasMaxLength(50).IsUnicode(true);

        builder.Property(x => x.PhoneNumber)
               .HasMaxLength(15);

        builder.Property(x => x.Message)
               .IsRequired()
               .HasMaxLength(1000).IsUnicode(true);


        builder.HasOne(c=>c.Article)
            .WithMany(a=>a.Comments)
            .HasForeignKey(c=>c.ArticleId);

        builder.ToTable("Comments");


    }
}
