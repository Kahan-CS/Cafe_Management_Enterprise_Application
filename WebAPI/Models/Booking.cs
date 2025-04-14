using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter the event date.")]
        [DataType(DataType.DateTime)]
        [CustomValidation(typeof(Booking), nameof(ValidateFutureDate))]
        public DateTime EventDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Please enter a location.")]
        public string Location { get; set; } = string.Empty;

        // Foreign key linking to the user who created the booking.
        [Required(ErrorMessage = "Booking must have an associated user.")]
        public string CreatedByUserId { get; set; } = string.Empty;

        public virtual ApplicationUser? CreatedByUser { get; set; }

        // List of invitations associated with this booking.
        public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

        // Unmapped property for convenience.
        [NotMapped]
        public int NumberOfInvitations => Invitations?.Count ?? 0;

        // Custom validator to ensure the event date is today or in the future.
        public static ValidationResult? ValidateFutureDate(DateTime eventDate, ValidationContext context)
        {
            return eventDate >= DateTime.Today
                ? ValidationResult.Success
                : new ValidationResult("Event date must be today or in the future.");
        }
    }
}
