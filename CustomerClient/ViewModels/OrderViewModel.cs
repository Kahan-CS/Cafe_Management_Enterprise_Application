using System.ComponentModel.DataAnnotations;

namespace CustomerClient.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; } // Order identifier

        [Required]
        public string Items { get; set; } // Items can be a comma-separated list or a more structured type

        [Required]
        public decimal TotalPrice { get; set; }
    }
}
