﻿using SportsEquipmentRental.Shared.Models;


namespace SportsEquipmentRental.Services
{
	public interface ICategoryService
	{
		Task<IEnumerable<Category>> GetAllAsync();
		Task<Category> GetByIdAsync(int id);
		Task AddAsync(Category category);
		Task UpdateAsync(Category category);
		Task DeleteAsync(int id);
	}
}
