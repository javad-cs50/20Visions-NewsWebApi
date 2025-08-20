using Microsoft.EntityFrameworkCore;
using NewsWebSiteApi.Application.Interfaces.Repositories;
using NewsWebSiteApi.Domain.Entities.Article;
using NewsWebSiteApi.Domain.Enum;
using NewsWebSiteApi.Infrastructure.ApplicationDb;
using System.Linq;

namespace NewsWebSiteApi.Infrastructure.Repositories;

public class ArticleRepository : IArticleRepository
{
    
    private readonly ApplicationDbContext _context;

    public ArticleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Article?> GetById(int id)
    {
        return await _context.Articles.FindAsync(id);
    }
    public async Task<IEnumerable<Article?>> GetByKeyWord(string keyWords)
    {
        var keyWordList = SplitStringToList(keyWords);
        var matchArticle = await _context.Articles
            .Where(a => keyWordList
            .Any(kw => a.KeyWord
            .Contains(kw)))
            .ToListAsync();

        return matchArticle;
        
    }
    private static List<string> SplitStringToList(string text) {
        var keywordsList = text.Split(',').ToList();
        return keywordsList;
    }

    public async Task<List<Article>> GetByDiscription(string discription) {

        var articles = await _context.Articles.Where(a => a.Discription.Contains(discription)).ToListAsync();
        return articles;
    }
    public async Task<List<Article>> GetByTitle(string title)
    {
        var articles = await _context.Articles.Where(a => a.Discription.Contains(title)).ToListAsync();
        return articles;
    }
    public async Task<List<Article?>?> GlobalSearch(string text) {

        var byTitle = await GetByTitle(text);
        var byDiscription = await GetByDiscription(text);
        var byKeyword = await GetByKeyWord(text);
        var articles = byTitle.Concat(byDiscription).Concat(byKeyword).DistinctBy(a=>a.Id).ToList();
        return articles;

    }

    public async Task<IEnumerable<Article>> GetAll()
    {
        var articles = await _context.Articles.ToListAsync();
        return articles;
    }

    public async Task<IEnumerable<Article>> GetFeaturedArticles()
    {
        var articles = await _context.Articles
            .Where(a => a.IsFeatured)
            .OrderByDescending(featuredArticle => featuredArticle.CreatedDate)
            .ToListAsync();
        return articles;
    }

    

    public async Task<bool> Create(Article article)
    {
        await _context.Articles.AddAsync(article);
        //"changes" is the number of entity that change in DB
        var changes = await _context.SaveChangesAsync();
        if (changes>0) 
            return true;
        else
            return false;
    }

    public async Task<bool> Update(Article article)
    {
        _context.Articles.Update(article);
        var changes=await _context.SaveChangesAsync();
        if (changes > 0)
            return true;
        else
            return false;
    }

    public async Task<bool> Delete(int id)
    {
        var article = await _context.Articles.FindAsync(id);

        if (article != null)
        {
            article.AppAction=AppAction.Deleted;
            var changes = await _context.SaveChangesAsync();
            if (changes > 0)
                return true;
            else
                return false;
        }
        return false;
    }

}

//SELECT
//* FROM Articles
//WHERE Discription like "%testDiscription%"
