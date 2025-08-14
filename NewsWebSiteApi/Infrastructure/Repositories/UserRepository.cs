using Microsoft.EntityFrameworkCore;
using NewsWebSiteApi.Application.Interfaces;
using NewsWebSiteApi.Application.Interfaces.Repositories;
using NewsWebSiteApi.Domain.Entities.User;
using NewsWebSiteApi.Domain.Enum;
using NewsWebSiteApi.Infrastructure.ApplicationDb;

namespace NewsWebSiteApi.Infrastructure.Repositories;

public class UserRepository: IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<User?> GetByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }

    public async Task<bool> AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        var changes = await _context.SaveChangesAsync();
        if(changes >0)
            return true;
        else
            return false;
    }

    public async Task<bool> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        var changes = await _context.SaveChangesAsync();
        if (changes >0)
            return true;
        else
            return false;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is not null) { 
            
            user.AppAction=AppAction.Deleted;
            var changes = await _context.SaveChangesAsync();
            
            if (changes>0)
                return true;
            else 
                return false;
        
        } 
        return false;

    }

}
