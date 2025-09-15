using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NewsWebSiteApi.Application.Interfaces;
using NewsWebSiteApi.Application.Interfaces.Repositories;
using NewsWebSiteApi.Infrastructure.ApplicationDb;
using NewsWebSiteApi.Infrastructure.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
opt.SwaggerDoc("v1", new OpenApiInfo
{
    Version = "v1",
    Title = "News",
    Description = "News Web Site Api"

});
opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Please Enter token :"
});
opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }

                },
             new string[] {}
        }
    });

//use for registering httpContextAccessor as singleton and then accessing controllers context outside of controller
builder.Services.AddHttpContextAccessor();


//it's register ApplicationDbContext as AddScoped and then attach the dBContext to DB
builder.Services.AddDbContext<IApplicationDbContext,ApplicationDbContext>
    (option =>option.UseSqlServer(connectionString:builder.Configuration.GetConnectionString("Default")));

// add Repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICommentRepository,CommentRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();

//add Jwt Bearer Authentication
builder.Services.AddAuthentication("Bearer").AddJwtBearer(option =>
option.TokenValidationParameters =
    new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                builder.Configuration["Jwt:SecretKey"]))
    });
builder.Services.AddAuthorization();

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
