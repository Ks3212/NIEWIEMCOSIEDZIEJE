using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportsEquipmentRental.Shared.Models;

namespace SportsEquipmentRental.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
		public DbSet<Category> Categories { get; set; }
		public DbSet<Equipment> Equipment { get; set; }
		public DbSet<EquipmentItem> EquipmentItems { get; set; }
		public DbSet<Rental> Rentals { get; set; }
		public DbSet<RentalItem> RentalItems { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// Category Configuration
			builder.Entity<Category>()
				.HasKey(c => c.CategoryId);

			builder.Entity<Category>()
				.HasMany(c => c.Equipment)
				.WithOne(e => e.Category)
				.HasForeignKey(e => e.CategoryId);

			// Equipment Configuration
			builder.Entity<Equipment>()
				.HasKey(e => e.EquipmentId);

			builder.Entity<Equipment>()
				.HasMany(e => e.Items)
				.WithOne(i => i.Equipment)
				.HasForeignKey(i => i.EquipmentId);

			// EquipmentItem Configuration
			builder.Entity<EquipmentItem>()
				.HasKey(ei => ei.EquipmentItemId);

			builder.Entity<EquipmentItem>()
				.HasOne(ei => ei.Equipment)
				.WithMany(e => e.Items)
				.HasForeignKey(ei => ei.EquipmentId)
				.OnDelete(DeleteBehavior.Cascade);

			// Rental Configuration
			builder.Entity<Rental>()
				.HasKey(r => r.RentalId);

			builder.Entity<Rental>()
				.HasOne(r => r.User)
				.WithMany(u => u.Rentals)
				.HasForeignKey(r => r.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			// RentalItem Configuration
			builder.Entity<RentalItem>()
				.HasKey(ri => new { ri.RentalId, ri.EquipmentItemId });

			builder.Entity<RentalItem>()
				.HasOne(ri => ri.Rental)
				.WithMany(r => r.RentalItems)
				.HasForeignKey(ri => ri.RentalId);

			builder.Entity<RentalItem>()
				.HasOne(ri => ri.EquipmentItem)
				.WithMany(ei => ei.RentalItems)
				.HasForeignKey(ri => ri.EquipmentItemId);
		}
	}
}
