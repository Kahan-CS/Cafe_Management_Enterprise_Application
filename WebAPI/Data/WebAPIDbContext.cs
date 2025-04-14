using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
                .WithMany()
                .HasForeignKey(b => b.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship between Invitation and Booking.
            builder.Entity<Invitation>()
                .HasOne(i => i.Booking)
                .WithMany(b => b.Invitations)
                .HasForeignKey(i => i.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            // ----------------------------------------------
            // SEED DATA
            // ----------------------------------------------

            // Define constants for seeding the admin user and role.
            string adminUserId = "1";
            string adminRoleId = "1";

            // Create a password hasher instance to hash the admin password.
            var hasher = new PasswordHasher<ApplicationUser>();

            // Seed the admin user.
            var adminUser = new ApplicationUser
            {
                Id = adminUserId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Sesame123#"),
                SecurityStamp = Guid.NewGuid().ToString("D"),
                Name = "Administrator"
            };
            builder.Entity<ApplicationUser>().HasData(adminUser);

            // Seed the Admin role.
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            );

            // Associate the admin user with the Admin role.
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId
                }
            );

            // Seed a Booking record associated with the admin user.
            builder.Entity<Booking>().HasData(
                new Booking
                {
                    BookingId = 1,
                    Description = "Seeded Booking",
                    EventDate = DateTime.Today.AddDays(7),
                    Location = "Main Hall",
                    CreatedByUserId = adminUserId
                }
            );

            // Seed an Invitation record for the seeded booking.
            builder.Entity<Invitation>().HasData(
                new Invitation
                {
                    InvitationId = 1,
                    GuestName = "John Doe",
                    GuestEmail = "johndoe@example.com",
                    Status = InvitationStatus.InviteNotSent,
                    BookingId = 1
                }
            );
        }

        // Static method that can be invoked at startup to ensure an admin user exists.
        // This is useful if you prefer to create the admin user using the Identity APIs rather than relying solely on EF Core migrations.
        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            // Create a scope to retrieve scoped services.
            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                string username = "admin";
                string password = "Sesame123#";
                string roleName = "Admin";

                // Ensure the admin role exists.
                if (await roleManager.FindByNameAsync(roleName) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }

                // Ensure the admin user exists.
                if (await userManager.FindByNameAsync(username) == null)
                {
                    var adminUser = new ApplicationUser
                    {
                        UserName = username,
                        Email = "admin@example.com",
                        Name = "Administrator",
                        EmailConfirmed = true,
                    };

                    var result = await userManager.CreateAsync(adminUser, password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, roleName);
                    }
                }
            }

        }
    }
}
