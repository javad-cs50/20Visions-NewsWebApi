using Microsoft.AspNetCore.Mvc;
using NewsWebSiteApi.Application.Interfaces.Repositories;
using NewsWebSiteApi.Application.Mapper;
using NewsWebSiteApi.Application.Models.Article;
using NewsWebSiteApi.Application.Models.Category;
using NewsWebSiteApi.Application.Models.Comment;
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
        public ArticlesController(IArticleRepository articleRepository, ILogger<ArticlesController> logger, IConfiguration configuration)
        {
            _articleRepository = articleRepository;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShowArticleDto>> GetById(int id)
        {
            var article = await _articleRepository.GetById(id);
            if (article == null) return NotFound();

            var articleDto = new ShowArticleDto
            {
                Id = article.Id,
                Title = article.Title,
                Cover = article.Cover,
                Description = article.Discription,
                AuthorId = article.AuthorId,
                CategoryId = article.CategoryId,
                CreatedDate = article.CreatedDate,
                IsFeatured = article.IsFeatured,
                CommentsDto = article.Comments
                .Select(c => new ShowCommentDto
                {
                    Id = c.Id,
                    CreatedDate = c.CreatedDate,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Message = c.Message
                }).ToList(),
                CategoryDto=new ShowCategoryDto
                { 
                    Id=article.Category.Id,
                    Symbol=article.Category.Symbol,
                    Title=article.Category.Title
                }

            };
            return Ok(articleDto);
        
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShowArticleDto>?>> GetAll()
        {
            var articles = await _articleRepository.GetAll();
            if (articles==null || !articles.Any())
                return NotFound();
            
            var articleDtos = articles.Select( a => a.ToShowArticleSummaryDto());

            return Ok(articleDtos);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ShowArticleDto>>> GetBySearch([FromQuery]string text)
        {
            var articles = await _articleRepository.GlobalSearch(text);
            if(articles==null || !articles.Any())
                return NotFound();
            else
            {
                var articleDtos = articles.Distinct().Select(a=>a.ToShowArticleSummaryDto());

                return Ok(articleDtos);
            }

        }
        [HttpGet("Featured")]
        public async Task<ActionResult<ShowArticleDto>> GetIsFeatured()
        {
            var featuredArticle = await _articleRepository.GetFeaturedArticles();
            if (featuredArticle == null || !featuredArticle.Any())
                return NotFound();
            else
            {
                var featuredArticleDto = featuredArticle.Select(a=>a.ToShowArticleSummaryDto());
                
            }
                return Ok(featuredArticle);

        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] CreateArticleDto req )
        {
            var article = req.ToArticleEntity();
            var result =await _articleRepository.Create(article);
            if (result==false)
                return BadRequest(result);
            return Ok(result);



        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] CreateArticleDto articleDto )
        {
            var article =await _articleRepository.GetById(id);
            if (article is not null)
                return NotFound();

            else
            {
                article.ModifiedDate = DateTime.Now;
                article.IsFeatured = articleDto.IsFeatured;
                article.Title = articleDto.Title;
                article.Discription = articleDto.Description;
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
        public async Task<ActionResult<bool>> Delete([FromRoute]int id) 
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
