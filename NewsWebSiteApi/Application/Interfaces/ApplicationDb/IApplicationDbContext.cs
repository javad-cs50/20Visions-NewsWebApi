using NewsWebSiteApi.Domain.Entities.Category;
using NewsWebSiteApi.Domain.Entities.Comment;
using NewsWebSiteApi.Domain.Entities.Common;
using NewsWebSiteApi.Domain.Entities.News;
using NewsWebSiteApi.Domain.Entities.User;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace NewsWebSiteApi.Application.Interfaces;

public class IApplicationDbContext
{
    public Dbset<User> Users => Set<User>();
    public Dbset<Article> Articles => Set<Article>();
    public Dbset<Comment> Comments => Set<Comment>();
    public Dbset<Category> Categories => Set<Category>();

    public DbSet<TEntity> SetDbSet<TEntity>() where TEntity : BaseEntity => Set<TEntity>();
}
   
