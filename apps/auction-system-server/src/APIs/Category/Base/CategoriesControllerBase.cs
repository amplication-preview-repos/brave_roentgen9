using AuctionSystem.APIs;
using AuctionSystem.APIs.Common;
using AuctionSystem.APIs.Dtos;
using AuctionSystem.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSystem.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CategoriesControllerBase : ControllerBase
{
    protected readonly ICategoriesService _service;

    public CategoriesControllerBase(ICategoriesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Category
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Category>> CreateCategory(CategoryCreateInput input)
    {
        var category = await _service.CreateCategory(input);

        return CreatedAtAction(nameof(Category), new { id = category.Id }, category);
    }

    /// <summary>
    /// Delete one Category
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteCategory([FromRoute()] CategoryWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteCategory(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Categories
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Category>>> Categories(
        [FromQuery()] CategoryFindManyArgs filter
    )
    {
        return Ok(await _service.Categories(filter));
    }

    /// <summary>
    /// Meta data about Category records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CategoriesMeta(
        [FromQuery()] CategoryFindManyArgs filter
    )
    {
        return Ok(await _service.CategoriesMeta(filter));
    }

    /// <summary>
    /// Get one Category
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Category>> Category(
        [FromRoute()] CategoryWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Category(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Category
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateCategory(
        [FromRoute()] CategoryWhereUniqueInput uniqueId,
        [FromQuery()] CategoryUpdateInput categoryUpdateDto
    )
    {
        try
        {
            await _service.UpdateCategory(uniqueId, categoryUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
