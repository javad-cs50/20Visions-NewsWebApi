using NewsWebSiteApi.Application.Interfaces;
using NewsWebSiteApi.Application.Interfaces.Repositories;

namespace NewsWebSiteApi.Infrastructure.Repositories;

public class CommentRepository: ICommentRepository
{
    private readonly IApplicationDbContext _context;

    public CommentRepository(IApplicationDbContext context)
    {
        _context = context;
    }
}
