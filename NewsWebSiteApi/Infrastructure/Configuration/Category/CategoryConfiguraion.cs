using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsWebSiteApi.Domain.Entities.Category;

namespace NewsWebSiteApi.Infrastructure.Configuration.CategoryConfiguration;

public class CategoryConfiguraion : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
               .IsRequired()
               .HasMaxLength(100).IsUnicode(true);

        builder.Property(x => x.Symbol)
               .HasMaxLength(10).IsUnicode(true);


        builder.HasMany(c => c.articles)
            .WithOne(a => a.Category)
            .HasForeignKey(a=>a.CategoryId);

        builder.ToTable("Categories");

    }
}
