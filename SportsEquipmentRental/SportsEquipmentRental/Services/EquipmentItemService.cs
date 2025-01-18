using SportsEquipmentRental.Data;
using SportsEquipmentRental.Shared.Models;
using SportsEquipmentRental.Services;
using Microsoft.EntityFrameworkCore;

namespace SportsEquipmentRental.Services
{
	public class EquipmentItemService : IEquipmentItemService
	{
		private readonly ApplicationDbContext _context;

		public EquipmentItemService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<EquipmentItem>> GetAllAsync()
		{
			// Pobierz wszystkie egzemplarze sprzętu razem z powiązanym sprzętem
			return await _context.EquipmentItems
				.AsNoTracking()
				.Include(ei => ei.Equipment) // Uwzględnij sprzęt (Equipment)
				.ToListAsync();
		}

		public async Task<EquipmentItem> GetByIdAsync(int id)
		{
			// Pobierz pojedynczy egzemplarz sprzętu po ID
			return await _context.EquipmentItems
				.AsNoTracking()
				.Include(ei => ei.Equipment) // Uwzględnij sprzęt (Equipment)
				.FirstOrDefaultAsync(ei => ei.EquipmentItemId == id);
		}

		public async Task AddAsync(EquipmentItem equipmentItem)
		{
			// Dodaj nowy egzemplarz sprzętu
			_context.EquipmentItems.Add(equipmentItem);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(EquipmentItem equipmentItem)
		{
			// Pobierz istniejący element ze śledzenia lub z bazy danych
			var existingItem = await _context.EquipmentItems.AsNoTracking()
				.FirstOrDefaultAsync(ei => ei.EquipmentItemId == equipmentItem.EquipmentItemId);

			if (existingItem == null)
			{
				throw new KeyNotFoundException($"EquipmentItem with ID {equipmentItem.EquipmentItemId} not found.");
			}

			// Odłącz istniejącą encję ze śledzenia
			_context.Entry(existingItem).State = EntityState.Detached;

			// Aktualizuj właściwości encji
			_context.Entry(equipmentItem).State = EntityState.Modified;

			// Zapisz zmiany
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			// Znajdź egzemplarz sprzętu po ID
			var equipmentItem = await _context.EquipmentItems.FindAsync(id);

			if (equipmentItem != null)
			{
				// Usuń egzemplarz sprzętu
				_context.EquipmentItems.Remove(equipmentItem);
				await _context.SaveChangesAsync();
			}
		}
	}
}
