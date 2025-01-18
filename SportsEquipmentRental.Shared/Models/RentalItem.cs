using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsEquipmentRental.Shared.Models
{
	public class RentalItem
	{
		public int RentalId { get; set; }
		public Rental Rental { get; set; }

		public int EquipmentItemId { get; set; }
		public EquipmentItem EquipmentItem { get; set; }

	}
}
