using Microsoft.EntityFrameworkCore;
using NewsWebSiteApi.Domain.Entities.Article;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsWebSiteApi.Domain.Entities.User;


namespace NewsWebSiteApi.Infrastructure.Configuration.ArticleConfiguration;


public class ArticleConfiguraion : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
               .IsRequired()
               .HasMaxLength(200).IsUnicode(true);

        builder.Property(x => x.Cover)
               .HasMaxLength(500);

        builder.Property(x => x.Discription)
               .HasMaxLength(2000).IsUnicode(true);

        builder.Property(x => x.IsFeatured)
               .HasDefaultValue(false);


        builder.HasKey(x => x.Id);
        //article and user relationship
        builder.HasOne(a => a.User)
               .WithMany(u => u.Articles)
               .HasForeignKey(a => a.AuthorId);

        //article and category relationship
        builder.HasOne(a=>a.Category)
            .WithMany(c=>c.articles)
            .HasForeignKey(a => a.CategoryId);

        //article and comment relationship
        builder.HasMany(a => a.Comments)
            .WithOne(c => c.Article)
            .HasForeignKey(a => a.ArticleId);

        builder.ToTable("Articles");

    }

}
