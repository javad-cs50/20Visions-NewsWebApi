using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NewsWebSiteApi.Application.Interfaces;
using NewsWebSiteApi.Application.Interfaces.Repositories;
using NewsWebSiteApi.Domain.Entities.Comment;
using NewsWebSiteApi.Domain.Enum;
using NewsWebSiteApi.Infrastructure.ApplicationDb;
using System.Threading.Channels;

namespace NewsWebSiteApi.Infrastructure.Repositories;

public class CommentRepository: ICommentRepository
{
    private readonly ApplicationDbContext _context;

    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Comment>> GetAll(int articleId)
    {
        var comments = await _context.Comments.Where(c=>c.ArticleId==articleId && c.AppAction == AppAction.Active).ToListAsync();
        return comments;
    }
    public async Task<Comment?> GetById(int id)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c=>c.Id==id && c.AppAction == AppAction.Active);
        return comment;
    }
    public async Task<bool> Create(Comment comment)
    {
        if (comment== null)return false; 

        await _context.Comments.AddAsync(comment);
        var changes = await _context.SaveChangesAsync();

        if (changes >= 1) return true;
        else
            return false;
    }
    public async Task<bool> Update(Comment comment)
    {
        if (comment== null)
            return false;

        _context.Comments.Update(comment);
        var changes = await _context.SaveChangesAsync();
        
        if (changes >= 1)
            return true;
        else
            return false;
    }
    public async Task<bool> Delete(int id)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        if (comment == null)
            return false;

        comment.AppAction = AppAction.Deleted;
        var changes = await _context.SaveChangesAsync();

        if(changes >= 1) 
            return true;
        else 
            return false;


    }
}
