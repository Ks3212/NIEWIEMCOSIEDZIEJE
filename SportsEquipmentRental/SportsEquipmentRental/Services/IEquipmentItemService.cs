using SportsEquipmentRental.Shared.Models;


namespace SportsEquipmentRental.Services
{
	public interface IEquipmentItemService
	{
		Task<IEnumerable<EquipmentItem>> GetAllAsync();
		Task<EquipmentItem> GetByIdAsync(int id);
		Task AddAsync(EquipmentItem equipmentItem);
		Task UpdateAsync(EquipmentItem equipmentItem);
		Task DeleteAsync(int id);
	}
}
