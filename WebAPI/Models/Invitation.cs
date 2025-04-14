using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public enum InvitationStatus
    {
        InviteNotSent,
        InviteSent,
        RespondedYes,
        RespondedNo
    }

    public class Invitation
    {
        [Key]
        public int InvitationId { get; set; }

        [Required(ErrorMessage = "Please enter guest's name.")]
        public string GuestName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter guest's email.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$", ErrorMessage = "Please enter a valid email with a proper domain")]
        public string GuestEmail { get; set; } = string.Empty;

        [Required]
        public InvitationStatus Status { get; set; } = InvitationStatus.InviteNotSent;

        // Foreign key to the associated booking.
        [Required]
        public int BookingId { get; set; }
        public virtual Booking? Booking { get; set; }

        // Read-only property to display status in a human-friendly format.
        [NotMapped]
        public string HumanReadableStatus
        {
            get
            {
                return Status switch
                {
                    InvitationStatus.InviteNotSent => "Invite Not Sent",
                    InvitationStatus.InviteSent => "Invitation Sent",
                    InvitationStatus.RespondedYes => "Responded Yes",
                    InvitationStatus.RespondedNo => "Responded No",
                    _ => Status.ToString()
                };
            }
        }
    }
}
