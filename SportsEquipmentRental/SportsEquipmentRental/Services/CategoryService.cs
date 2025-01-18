using SportsEquipmentRental.Data;
using SportsEquipmentRental.Shared.Models;

using Microsoft.EntityFrameworkCore;

namespace SportsEquipmentRental.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly ApplicationDbContext _context;

		public CategoryService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Category>> GetAllAsync()
		{
			return await _context.Categories.AsNoTracking().ToListAsync();
		}

		public async Task<Category> GetByIdAsync(int id)
		{
			return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == id);
		}

		public async Task AddAsync(Category category)
		{
			_context.Categories.Add(category);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Category category)
		{
			var existingCategory = await _context.Categories.FindAsync(category.CategoryId);

			if (existingCategory == null)
			{
				throw new KeyNotFoundException($"Category with ID {category.CategoryId} not found.");
			}

			existingCategory.Name = category.Name;

			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var category = await _context.Categories.FindAsync(id);

			if (category == null)
			{
				throw new KeyNotFoundException($"Category with ID {id} not found.");
			}

			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();
		}
	}
}
