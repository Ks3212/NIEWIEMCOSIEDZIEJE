using SportsEquipmentRental.Data;
using SportsEquipmentRental.Shared.Models;
using Microsoft.EntityFrameworkCore;
using static SportsEquipmentRental.Shared.Helpers.EnumHelper;

namespace SportsEquipmentRental.Services
{
	public class EquipmentService : IEquipmentService
	{
		private readonly ApplicationDbContext _context;

		public EquipmentService(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<Equipment>> GetbyCategoryIdAsync(int categoryId)
		{
			return await _context.Equipment
				.AsNoTracking()
				.Include(e => e.Items)
				.Where(e => e.CategoryId == categoryId)
				.ToListAsync();
		}
		public async Task<IEnumerable<Equipment>> GetAllAsync()
		{
			return await _context.Equipment
				.AsNoTracking()
				.Include(e => e.Items)
				.ToListAsync();
		}

		public async Task<Equipment> GetByIdAsync(int id)
		{
			return await _context.Equipment
				.AsNoTracking()
				.Include(e => e.Items)
				.FirstOrDefaultAsync(e => e.EquipmentId == id);
		}

		public async Task AddAsync(Equipment equipment)
		{
			_context.Equipment.Add(equipment);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var equipment = await _context.Equipment.FindAsync(id);
			if (equipment != null)
			{
				_context.Equipment.Remove(equipment);
				await _context.SaveChangesAsync();
			}
		}
		public async Task UpdateAsync(Equipment equipment)
		{
			// Sprawdzenie i inicjalizacja przekazanego sprzętu
			if (equipment.Items == null)
			{
				equipment.Items = new List<EquipmentItem>();
			}

			// Pobranie istniejącego sprzętu z bazy danych
			var existingEquipment = await _context.Equipment
				.Include(e => e.Items)
				.FirstOrDefaultAsync(e => e.EquipmentId == equipment.EquipmentId);

			// Sprawdzenie, czy sprzęt istnieje
			if (existingEquipment == null)
			{
				throw new KeyNotFoundException($"Equipment with ID {equipment.EquipmentId} not found.");
			}

			// Inicjalizacja elementów, jeśli są null
			if (existingEquipment.Items == null)
			{
				existingEquipment.Items = new List<EquipmentItem>();
			}

			// Aktualizacja właściwości sprzętu
			existingEquipment.Name = equipment.Name;
			existingEquipment.Brand = equipment.Brand;
			existingEquipment.PricePerDay = equipment.PricePerDay;
			existingEquipment.CategoryId = equipment.CategoryId;

			// Aktualizacja obrazu, jeśli nowy został przesłany
			if (equipment.Image != null && equipment.Image.Length > 0)
			{
				existingEquipment.Image = equipment.Image;
			}

			// Synchronizacja elementów
			foreach (var item in equipment.Items)
			{
				var existingItem = existingEquipment.Items.FirstOrDefault(i => i.EquipmentItemId == item.EquipmentItemId);

				if (existingItem != null)
				{
					// Aktualizacja istniejącego elementu
					existingItem.SerialNumber = item.SerialNumber;
					existingItem.Condition = item.Condition;
				}
				else
				{
					// Dodanie nowego elementu
					existingEquipment.Items.Add(item);
				}
			}

			// Zapisanie zmian w bazie danych
			await _context.SaveChangesAsync();
		}
		public async Task<int> GetAvailableItemsCountAsync(int equipmentId)
		{
			return await _context.EquipmentItems
				.Where(ei => ei.EquipmentId == equipmentId && ei.Status == EquipmentItemStatus.Available)
				.CountAsync();
		}
	}
}
