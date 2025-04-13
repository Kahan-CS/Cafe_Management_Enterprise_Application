using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace CafeCustomerClient.ViewModels
{
    public class BookingViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan BookingTime { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Please enter a valid number of guests.")]
        public int GuestCount { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Description is too long.")]
        public string BookingDescription { get; set; }

        // Comma-separated emails or a list of guest identifiers.
        public List<string> InvitedGuests { get; set; } = new List<string>();
    }
}
