using Microsoft.EntityFrameworkCore;
using NewsWebSiteApi.Application.Interfaces;
using NewsWebSiteApi.Domain.Entities.Category;
using NewsWebSiteApi.Domain.Entities.Comment;
using NewsWebSiteApi.Domain.Entities.Common;
using NewsWebSiteApi.Domain.Entities.News;
using NewsWebSiteApi.Domain.Entities.User;
using System.Reflection;

namespace NewsWebSiteApi.Infrastructure.ApplicationDb;

public class ApplicationDbContext:DbContext,IApplicationDbContext
{
    private readonly IHttpContextAccessor _contextAccessor;
public  ApplicationDbContext(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
    public DbSet<User> Users => Set<User>();
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<TEntity> SetDbSet<TEntity>() where TEntity : BaseEntity=>Set<TEntity>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder) {
        base.OnConfiguring(optionBuilder);
    }

}
