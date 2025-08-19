using Microsoft.AspNetCore.Mvc;
using NewsWebSiteApi.Application.Interfaces.Repositories;
using NewsWebSiteApi.Application.Models.Article;
using NewsWebSiteApi.Domain.Entities.Article;

namespace NewsWebSiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ILogger<ArticlesController> _logger;
        private readonly IConfiguration _configuration;
        public ArticlesController(IArticleRepository articleRepository,ILogger<ArticlesController> logger ,IConfiguration configuration)
        {
            _articleRepository = articleRepository; 
            _logger = logger;
            _configuration = configuration;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Article>>> GetAllArticles()
        {
            var articles = await _articleRepository.GetAll();
            if (articles == null)
                return NotFound();
            var categoryDtos= articles.Select(a => new ShowArticleDto
            {
                Cover=a.Cover, 
                Discription=a.Discription,
                Title=a.Title,AuthorId=a.AuthorId,CreatedDate=a.CreatedDate

            });
            return Ok(categoryDtos);
        }
    }
}
