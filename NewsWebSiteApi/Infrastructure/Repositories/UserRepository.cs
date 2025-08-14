using NewsWebSiteApi.Application.Interfaces;

namespace NewsWebSiteApi.Infrastructure.Repositories;

public class UserRepository
{
    private readonly IApplicationDbContext _context;

    public UserRepository(IApplicationDbContext context)
    {
        _context = context;
    }
}
