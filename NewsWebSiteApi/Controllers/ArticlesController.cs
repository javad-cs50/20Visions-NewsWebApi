using Microsoft.AspNetCore.Mvc;
using NewsWebSiteApi.Application.Interfaces.Repositories;
using NewsWebSiteApi.Application.Models.Article;
using NewsWebSiteApi.Domain.Entities.Article;
using NewsWebSiteApi.Domain.Entities.Comment;

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
        [HttpGet]
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
        [HttpPost]
        public async Task<ActionResult<bool>> CreateArticle([FromBody] CreateArticleDto req )
        {
            var article = new Article
            {
                Title = req.Title,
                Cover = req.Cover,
                Discription = req.Discription,
                CategoryId = req.CategoryId,
                AuthorId = req.AuthorId,
                CreatedDate = DateTime.Now
            };
            var result =_articleRepository.Create(article);
            if (result == null)
                return BadRequest(result);
            return Ok(result);



        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateArticle(int id, [FromBody] CreateArticleDto articleDto )
        {
            var article =await _articleRepository.GetById(id);
            if (article is not null)
                return NotFound();

            else
            {
                article.ModifiedDate = DateTime.Now;
                article.IsFeatured = articleDto.IsFeatured;
                article.Title = articleDto.Title;
                article.Discription = articleDto.Discription;
                article.Cover = articleDto.Cover;
                article.CategoryId = articleDto.CategoryId;
                article.KeyWord = articleDto.KeyWord;
                
                
            }     
            

            var result = await _articleRepository.Update(article);
            if (result == false)
                return BadRequest(result);
            return Ok(result);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteArticle(int id) 
        {
            var article =await _articleRepository.GetById(id);
            if (article == null) return NotFound();

            var result = await _articleRepository.Delete(id);

            if (result == false)
                BadRequest(result);

            return Ok(result);
        }
    }
}
