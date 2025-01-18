using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportsEquipmentRental.Shared.Helpers.EnumHelper;

namespace SportsEquipmentRental.Shared.Models
{
	public class EquipmentItem
	{
		public int EquipmentItemId { get; set; }
		public int EquipmentId { get; set; }
		public Equipment Equipment { get; set; }
		public string SerialNumber { get; set; }

		public EquipmentCondition Condition { get; set; } = EquipmentCondition.New;
		public EquipmentItemStatus Status { get; set; } = EquipmentItemStatus.Available;

		public ICollection<RentalItem> RentalItems { get; set; }
	}
}
