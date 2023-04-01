using Carservice.Models.Users;

namespace Carservice.Models
{
	public class Order
	{
		public int Id { get; set; }
		public string? UserName { get; set; } = "";
		public string? UserId { get; set; } = "";
		public AppUser? User { get; set; }

		public string? Product { get; set; } = "";
		public int? Amount { get; set; }
		public string? Date { get; set; } = "";
		public string? ProviderName { get; set; } = "";
		public int? ProviderId { get; set; }
		public Provider? Provider { get; set; }
	}
}
