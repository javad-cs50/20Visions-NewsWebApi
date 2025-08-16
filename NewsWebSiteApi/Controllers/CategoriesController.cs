using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsWebSiteApi.Application.Interfaces.Repositories;
using NewsWebSiteApi.Application.Models.Category;
using NewsWebSiteApi.Domain.Entities.Category;
using NewsWebSiteApi.Domain.Enum;

namespace NewsWebSiteApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<CategoriesController> _logger;
    private readonly IConfiguration _configuration;

    public CategoriesController(ICategoryRepository categoryRepository,ILogger<CategoriesController> logger , IConfiguration configuration)
    {
        _categoryRepository = categoryRepository;
        _logger = logger;
        _configuration = configuration;
    }
    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<ShowCategoryDto>>> GetAllCategory() {
       var categories = await _categoryRepository.GetAll();
        var categoryDtos= categories.Select(c => new ShowCategoryDto
        {
            Symbol = c.Symbol,
            Title = c.Title
        });
        
        return Ok(categoryDtos);
    }
    [HttpGet("{categoryId}")]
    public async Task<ActionResult<ShowCategoryDto>> GetCategoryById(int categoryId)
    {
        var category = await _categoryRepository.GetById(categoryId);
        var categoryDto = new ShowCategoryDto
        {
            Symbol= category.Symbol ,
            Title= category.Title 
        };
        
        return Ok(categoryDto);
    }
    [HttpPost]
    public async Task<ActionResult<bool>> CreateCategory([FromBody] CreateCategoryDto req)
    {   
        var category = new Category 
        {
            Symbol=req.Symbol,
            Title= req.Title,
            AppAction = AppAction.Active,
            CreatedDate = DateTime.Now
            
        };
        var isCreated = await _categoryRepository.Create(category);
        if (isCreated == true)
            return Ok(isCreated);
        else
            return BadRequest(isCreated);
    }
    [HttpPut("{categoryId}")]
    public async Task<ActionResult<bool>> UpdateCategory(int categoryId,[FromBody]CreateCategoryDto categoryDto)
    {
        var category =await _categoryRepository.GetById(categoryId);
        
        if (category == null)
            return NotFound();
        else
        {
            category.Title = categoryDto.Title;
            category.Symbol = categoryDto.Symbol;
            category.ModifiedDate = DateTime.Now;
            
        }
        var isUpdated = await _categoryRepository.Update(category);
        if (isUpdated)
            return Ok(isUpdated);
        else
            return BadRequest(false);
    }
    [HttpDelete("{categoryId}")]
    public async Task<ActionResult<bool>> DeleteCategory(int categoryId) 
    {
        var category = await _categoryRepository.GetById(categoryId);

        if (category == null)
            return NotFound();

      
        var isDeleted = await _categoryRepository.Delete(categoryId);

        if (isDeleted == true)
            return Ok(isDeleted);
        else
            return BadRequest(isDeleted);
        
    }
}