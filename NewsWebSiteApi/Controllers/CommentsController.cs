using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsWebSiteApi.Application.Interfaces.Repositories;
using NewsWebSiteApi.Application.Models.Comment;
using NewsWebSiteApi.Domain.Entities.Comment;
using NewsWebSiteApi.Domain.Enum;

namespace NewsWebSiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger<CommentsController> _logger ;
        private readonly IConfiguration _configuration;

        public CommentsController(ICommentRepository commentRepository, ILogger<CommentsController> logger, IConfiguration configuration)
        {
            _commentRepository = commentRepository;
            _logger = logger;
            _configuration = configuration;
        }

        
        [HttpGet("{articleId}")]
        public async Task<ActionResult<IEnumerable<ShowCommentDto>>> GetAllCommentsByArticleId(int articleId)
        {
            var comments = await _commentRepository.GetAll(articleId);
            if (comments == null) return NotFound();
            var commentDtos = comments.Select(c => new ShowCommentDto
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                Message = c.Message,
                CreatedDate = c.CreatedDate
            });

            return Ok(commentDtos);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<ShowCommentDto>> GetCommentById(int id)
        {
            var comment = await _commentRepository.GetById(id);
            if (comment == null) return NotFound();

            var commentDto = new ShowCommentDto
            {
                FirstName = comment.FirstName,
                LastName = comment.LastName,
                Message = comment.Message,
                CreatedDate = comment.CreatedDate
            };

            return Ok(commentDto);
        }

        
        [HttpPost]
        public async Task<ActionResult<bool>> CreateComment([FromBody] CreateCommentDto req)
        {
            var comment = new Comment
            {
                ArticleId = req.NewsId,
                FirstName = req.FirstName,
                LastName = req.LastName,
                PhoneNumber = req.PhoneNumber,
                Message = req.Message,
                AppAction = AppAction.Active,
                CreatedDate = DateTime.Now
            };

            var isSaved = await _commentRepository.Create(comment);
            if (isSaved)
                return Ok(true);
            else
                return BadRequest(false);
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateCategory(int id, [FromBody] CreateCommentDto req)
        {
            var comment = await _commentRepository.GetById(id);
            if (comment == null)
                return NotFound();

            comment.FirstName = req.FirstName;
            comment.LastName = req.LastName;
            comment.PhoneNumber = req.PhoneNumber;
            comment.Message = req.Message;
            comment.ModifiedDate = DateTime.Now;

            var isUpdated = await _commentRepository.Update(comment);
            if (isUpdated)
                return Ok(true);
            else
                return BadRequest(false);
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteComment(int id)
        {
            var comment = await _commentRepository.GetById(id);
            if (comment == null)
                return NotFound();

            var isDeleted = await _commentRepository.Delete(id);
            if (isDeleted)
                return Ok(true);
            else
                return NotFound();
        }
    }
}

