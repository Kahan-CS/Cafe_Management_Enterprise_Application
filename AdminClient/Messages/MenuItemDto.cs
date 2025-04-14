namespace AdminClient.Messages
{
	public class MenuItemDto
	{
		public int MenuItemId { get; set; }
		public string Description { get; set; } = string.Empty;
		public decimal Price { get; set; }
	}
}
