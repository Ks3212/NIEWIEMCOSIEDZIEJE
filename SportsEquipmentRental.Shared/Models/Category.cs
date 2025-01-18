using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsEquipmentRental.Shared.Models
{
	public class Category
	{
		public int CategoryId { get; set; }

		[Required(ErrorMessage = "Category name is required.")]
		public string Name { get; set; }

		public ICollection<Equipment> Equipment { get; set; }
	}
}
