using Microsoft.EntityFrameworkCore;
using NewsWebSiteApi.Application.Interfaces.Repositories;
using NewsWebSiteApi.Domain.Entities.Category;
using NewsWebSiteApi.Domain.Enum;
using NewsWebSiteApi.Infrastructure.ApplicationDb;

namespace NewsWebSiteApi.Infrastructure.Repositories;

public class CategoryRepository:ICategoryRepository
{
    private readonly ApplicationDbContext _context; 

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Category?> GetById(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        return category;
    }
    public async Task<IEnumerable<Category>> GetAll()
    {
        var categories = await _context.Categories.ToListAsync();
        return categories;
    }
    public async Task<bool> Create(Category category)
    {
        if (category == null) return false; 

        await _context.Categories.AddAsync(category);
        var changes = await _context.SaveChangesAsync();

        if (changes>=1)
            return true;
        else
            return false;
    }
    public async Task<bool> Update(Category category)
    {
        if (category == null) return false;
        
         _context.Categories.Update(category);
        var changes = _context.SaveChanges();
        
        if (changes>=1) 
            return true;
        else
            return false;
    }
    public async Task<bool> Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if(category==null)return false;

        category.AppAction=AppAction.Deleted;
        var changes=await _context.SaveChangesAsync();

        if (changes>=1) 
            return true;
        else
            return false;
    }
}
