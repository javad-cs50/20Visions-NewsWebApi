using Microsoft.EntityFrameworkCore;
using NewsWebSiteApi.Application.Interfaces;
using NewsWebSiteApi.Application.Interfaces.Repositories;
using NewsWebSiteApi.Infrastructure.ApplicationDb;
using NewsWebSiteApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//use for registering httpContextAccessor as singleton and then accessing controllers context outside of controller
builder.Services.AddHttpContextAccessor();


//it's register ApplicationDbContext as AddScoped and then attach the dBContext to db
builder.Services.AddDbContext<IApplicationDbContext,ApplicationDbContext>
    (option =>option.UseSqlServer(connectionString:builder.Configuration.GetConnectionString("Default")));

// add Repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICommentRepository,CommentRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();
