using NewsWebSiteApi.Infrastructure.ApplicationDb;

namespace NewsWebSiteApi.Infrastructure.Repositories;

public class ArticleRepository
{
    private readonly IApplicationDbContext _context;

    public ArticleRepository(IApplicationDbContext context)
    {
        _context = context;
    }

}
