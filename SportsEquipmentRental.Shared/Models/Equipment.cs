using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsEquipmentRental.Shared.Models
{
	public class Equipment
	{
		public int EquipmentId { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; }

		public string Name { get; set; }
		public string Brand { get; set; }
		public decimal PricePerDay { get; set; }
		public byte[] Image { get; set; }


		public ICollection<EquipmentItem> Items { get; set; }
	}
}
