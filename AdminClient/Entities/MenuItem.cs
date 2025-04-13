namespace AdminClient.Entities
{
	public class MenuItem
	{
		// PK
		public int MenuItemId { get; set; }

		public string Description { get; set; } = string.Empty;
		
		public decimal Price { get; set; }
	}
}
