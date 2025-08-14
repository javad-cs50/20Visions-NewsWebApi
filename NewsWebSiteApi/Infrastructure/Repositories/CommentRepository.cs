using NewsWebSiteApi.Application.Interfaces;

namespace NewsWebSiteApi.Infrastructure.Repositories;

public class CommentRepository
{
    private readonly IApplicationDbContext _context;

    public CommentRepository(IApplicationDbContext context)
    {
        _context = context;
    }
}
