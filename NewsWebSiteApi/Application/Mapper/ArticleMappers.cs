using NewsWebSiteApi.Application.Models.Article;
using NewsWebSiteApi.Domain.Entities.Article;

namespace NewsWebSiteApi.Application.Mapper;

public static class ArticleMappers
{
    public static Article ToArticleEntity( this CreateArticleDto article) 
    {
        var articleEntity = new Article
        {
            Title = article.Title,
            Cover = article.Cover,
            Discription = article.Description,
            CategoryId = article.CategoryId,
            AuthorId = article.AuthorId,
            CreatedDate = DateTime.Now,
            KeyWord = article.KeyWord,
            IsFeatured =article.IsFeatured, 
            
            
        };
        return articleEntity;
    }
    public static ShowArticleDto ToCreateArticleDto(this Article article) 
    {
        var articleDto = new ShowArticleDto 
        {
            Id = article.Id,
            Title = article.Title,
            Cover = article.Cover,
            Description = article.Discription,
            AuthorId = article.AuthorId,
            CategoryId = article.CategoryId,
            CreatedDate = article.CreatedDate,
            IsFeatured = article.IsFeatured
        };
        return articleDto;
    }
    public static ShowArticleDto ToShowArticleDto(this Article article)
    {
        var articleDto = new ShowArticleDto
        {
            Id = article.Id,
            Title = article.Title,
            Cover = article.Cover,
            Description = article.Discription,
            AuthorId = article.AuthorId,
            CategoryId = article.CategoryId,
            CreatedDate = article.CreatedDate,
            IsFeatured = article.IsFeatured
        };
        return articleDto;
    }
    public static ShowArticleDto ToShowArticleSummaryDto(this Article article)
    {
        var articleSummaryDto = new ShowArticleDto
        {
            Id = article.Id,
            Title = article.Title,
            Cover = article.Cover,
            Description = article.Discription,
            AuthorId = article.AuthorId,
            CategoryId = article.CategoryId,
            CreatedDate = article.CreatedDate,
            IsFeatured = article.IsFeatured
        };
        return articleSummaryDto;
    }

}