using Microsoft.AspNetCore.Mvc;
using SportsEquipmentRental.Shared.Models;
using SportsEquipmentRental.Services;

namespace SportsEquipmentRental.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		// GET: api/category
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var categories = await _categoryService.GetAllAsync();
			return Ok(categories);
		}

		// GET: api/category/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var category = await _categoryService.GetByIdAsync(id);

			if (category == null)
			{
				return NotFound();
			}

			return Ok(category);
		}

		// POST: api/category
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Category category)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			await _categoryService.AddAsync(category);
			return CreatedAtAction(nameof(GetById), new { id = category.CategoryId }, category);
		}

		// PUT: api/category/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] Category category)
		{
			if (id != category.CategoryId)
			{
				return BadRequest("ID w URL i modelu nie są zgodne.");
			}

			try
			{
				await _categoryService.UpdateAsync(category);
			}
			catch (KeyNotFoundException)
			{
				return NotFound();
			}

			return NoContent();
		}

		// DELETE: api/category/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await _categoryService.DeleteAsync(id);
			}
			catch (KeyNotFoundException)
			{
				return NotFound();
			}

			return NoContent();
		}
	}
}
