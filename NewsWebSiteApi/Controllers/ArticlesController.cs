using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        public async Task<ActionResult<IEnumerable<ShowArticleDto>>> GetAllArticles()
        {
            var articles = await _articleRepository.GetAll();
            if (articles==null || !articles.Any())
                return NotFound();
            var categoryDtos= articles.Select(a => new ShowArticleDto
            {
                Id = a.Id,
                Cover=a.Cover, 
                Discription=a.Discription,
                Title=a.Title,
                AuthorId=a.AuthorId,
                CategoryId=a.CategoryId,
                CreatedDate=a.CreatedDate,
                IsFeatured =a.IsFeatured


            });
            return Ok(categoryDtos);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ShowArticleDto>>> GetBySearch([FromQuery] string keyWord, [FromQuery] string title, [FromQuery]string description ) {
            
            var articles=new List<Article>();
            if(keyWord is not null)
            {
                articles.AddRange(await _articleRepository.GetByKeyWord(keyWord));

            }
            if (title is not null)
            {
                articles.AddRange(await _articleRepository.GetByTitle(title));

            }
            if (description is not null)
            {
                articles.AddRange(await _articleRepository.GetByDescription(description));

            }

            if (articles == null || !articles.Any())
                return NotFound();
            else
            {
                var articleDtos = articles.Distinct().Select(a => new ShowArticleDto
                {
                    Id = a.Id,
                    Cover = a.Cover,
                    Discription = a.Discription,
                    Title = a.Title,
                    AuthorId = a.AuthorId,
                    CategoryId = a.CategoryId,
                    CreatedDate = a.CreatedDate,
                    IsFeatured = a.IsFeatured
                });
                

                return Ok(articleDtos);
            }
        }

        [HttpGet("globalSearch")]
        public async Task<ActionResult<IEnumerable<ShowArticleDto>>> GetByGlobalSearch(string globalSearch)
        {
            var articles = await _articleRepository.GlobalSearch(globalSearch);
            if(articles==null || !articles.Any())
                return NotFound();
            else
            {
                var articleDtos = articles.Distinct().Select(a => new ShowArticleDto
                {
                    Id = a.Id,
                    Cover = a.Cover,
                    Discription = a.Discription,
                    Title = a.Title,
                    AuthorId = a.AuthorId,
                    CategoryId = a.CategoryId,
                    CreatedDate = a.CreatedDate,
                    IsFeatured = a.IsFeatured
                });

                return Ok(articles);
            }

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
                CreatedDate = DateTime.Now,
                KeyWord=req.KeyWord
            };
            var result =await _articleRepository.Create(article);
            if (result==false)
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
