namespace Carservice.Models
{
	public class Provider
	{
		public int Id { get; set; }
		public string? Name { get; set; } = "";
		public string? ContactNumber { get; set; } = "";
		public string? Address { get; set; } = ""; 
		public string? City { get; set; } = "";

		public List<Order>? Orders { get; set; }
		
	}
}