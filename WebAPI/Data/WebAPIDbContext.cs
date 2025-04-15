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
        public DbSet<OrderRequest> OrderRequests { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

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
                PasswordHash = "AQHAHAHAAEAACcQAAAAEGslp7npl23e2342126",
                // static stamp, because we don't want to change it every time we seed the database
                SecurityStamp = "00000000-0000-0000-0000-000000000001",
                Name = "Administrator"
            };

            // Seed three additional (non-admin) users.
            var user2 = new ApplicationUser
            {
                Id = "2",
                UserName = "jane.doe",
                NormalizedUserName = "JANE.DOE",
                Email = "jane.doe@example.com",
                NormalizedEmail = "JANE.DOE@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEGslp7npl23e2342126",
                // static stamp, because we don't want to change it every time we seed the database
                SecurityStamp = "00000000-0000-0000-0000-000000000002",
                Name = "Jane Doe"
            };

            var user3 = new ApplicationUser
            {
                Id = "3",
                UserName = "bob.smith",
                NormalizedUserName = "BOB.SMITH",
                Email = "bob.smith@example.com",
                NormalizedEmail = "BOB.SMITH@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEGslp7npl23e2342123",
                // static stamp, because we don't want to change it every time we seed the database
                SecurityStamp = "00000000-0000-0000-0000-000000000003",
                Name = "Bob Smith"
            };

            var user4 = new ApplicationUser
            {
                Id = "4",
                UserName = "alice.johnson",
                NormalizedUserName = "ALICE.JOHNSON",
                Email = "alice.johnson@example.com",
                NormalizedEmail = "ALICE.JOHNSON@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEGslp7npl23e2342124",
                // static stamp, because we don't want to change it every time we seed the database
                SecurityStamp = "00000000-0000-0000-0000-000000000004",
                Name = "Alice Johnson"
            };

            // Combine the seed data for all users.
            builder.Entity<ApplicationUser>().HasData(adminUser, user2, user3, user4);

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
            var booking1 = new Booking
            {
                BookingId = 1,
                Description = "Seeded Booking by Admin",
                EventDate = new DateTime(2025, 10, 20),
                Location = "Main Hall",
                CreatedByUserId = adminUserId
            };

            // Seed an additional Booking record for Jane Doe.
            var booking2 = new Booking
            {
                BookingId = 2,
                Description = "Seeded Booking by Jane",
                EventDate = new DateTime(2025, 10, 20),
                Location = "Conference Room A",
                CreatedByUserId = "2"
            };

            builder.Entity<Booking>().HasData(booking1, booking2);

            // Seed an Invitation record for the admin's booking.
            var invitation1 = new Invitation
            {
                InvitationId = 1,
                GuestName = "John Doe",
                GuestEmail = "johndoe@example.com",
                Status = InvitationStatus.InviteNotSent,
                BookingId = 1
            };

            // Seed an additional Invitation record for Jane's booking.
            var invitation2 = new Invitation
            {
                InvitationId = 2,
                GuestName = "Mark Spencer",
                GuestEmail = "mark.spencer@example.com",
                Status = InvitationStatus.InviteNotSent,
                BookingId = 2
            };

            builder.Entity<Invitation>().HasData(invitation1, invitation2);
        }

        // Static method to ensure an admin user exists at runtime.
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
