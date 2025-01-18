using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportsEquipmentRental.Shared.Helpers.EnumHelper;

namespace SportsEquipmentRental.Shared.Models
{
	public class Rental
	{
		public int RentalId { get; set; }

		public string UserId { get; set; }
		public ApplicationUser User { get; set; }

		public DateTime RentalStart { get; set; }
		public DateTime RentalEnd { get; set; }

		public RentalStatus Status { get; set; } = RentalStatus.New;

		public ICollection<RentalItem> RentalItems { get; set; }
	}
}
