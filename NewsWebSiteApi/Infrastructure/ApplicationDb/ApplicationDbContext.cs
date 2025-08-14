using NewsWebSiteApi.Application.Interfaces;
using NewsWebSiteApi.Domain.Entities.Category;
using NewsWebSiteApi.Domain.Entities.Comment;
using NewsWebSiteApi.Domain.Entities.Common;
using NewsWebSiteApi.Domain.Entities.News;
using NewsWebSiteApi.Domain.Entities.User;

namespace NewsWebSiteApi.Infrastructure.ApplicationDb;

public class ApplicationDbContext:DbContext,IApplicationDbContext
{
    private readonly IHttpContextAccessor _contextAccessor;
public  ApplicationDbContext(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
    public Dbset<User> Users => Set<User>();
    public Dbset<Article> Articles => Set<Article>();
    public Dbset<Comment> Comments => Set<Comment>();
    public Dbset<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<TEntity> SetDbSet<TEntity>() where TEntity : BaseEntity=>Set<TEntity>();
    protected override void OnConfiguration(DbContextOptionBuilder optionBuilder) {
        base.OnConfiguring(optionBuilder);
    }

}
