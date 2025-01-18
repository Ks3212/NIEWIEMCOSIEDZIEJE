using SportsEquipmentRental.Shared.Models;
namespace SportsEquipmentRental.Services
{
	public interface IEquipmentService
	{
		Task<IEnumerable<Equipment>> GetAllAsync();
		Task<Equipment> GetByIdAsync(int id);
		Task AddAsync(Equipment equipment);
		Task UpdateAsync(Equipment equipment);
		Task DeleteAsync(int id);
		Task<IEnumerable<Equipment>> GetbyCategoryIdAsync(int categoryId);
		Task<int> GetAvailableItemsCountAsync(int equipmentId);
	}
}
