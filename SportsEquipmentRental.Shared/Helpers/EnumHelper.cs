using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsEquipmentRental.Shared.Helpers
{
	public class EnumHelper
	{
		public enum EquipmentItemStatus
		{
			Available,
			Rented,
			Maintenance
		}
		public enum EquipmentCondition
		{
			New, Used, Damaged
		}

		public enum RentalStatus
		{
			New,
			Active,
			Completed,
			Cancelled
		}
	}
}
