using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class WebAPIDbContext : IdentityDbContext<ApplicationUser>
    {
        public WebAPIDbContext(DbContextOptions<WebAPIDbContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Invitation> Invitations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure the relationship between Booking and ApplicationUser.
            builder.Entity<Booking>()
                .HasOne(b => b.CreatedByUser)
                .WithMany() // Optionally, you can add a navigation collection in ApplicationUser if desired.
                .HasForeignKey(b => b.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship between Invitation and Booking.
            builder.Entity<Invitation>()
                .HasOne(i => i.Booking)
                .WithMany(b => b.Invitations)
                .HasForeignKey(i => i.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
