using Microsoft.EntityFrameworkCore;
using NewsWebSiteApi.Domain.Entities.Category;
using NewsWebSiteApi.Domain.Entities.Comment;
using NewsWebSiteApi.Domain.Entities.Common;
using NewsWebSiteApi.Domain.Entities.News;
using NewsWebSiteApi.Domain.Entities.User;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace NewsWebSiteApi.Application.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; }
    public DbSet<Article> Articles { get; }
    public DbSet<Comment> Comments { get; }
    public DbSet<Category> Categories { get; }

    public DbSet<TEntity> SetDbSet<TEntity>() where TEntity : BaseEntity;
}
   
