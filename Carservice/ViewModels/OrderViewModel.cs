using Carservice.Models;

namespace Carservice.ViewModels
{
	public class OrderViewModel
	{
		public int Id { get; set; }
		public string? Manager { get; set; } = "";
		public string? UserId { get; set; } = "";

		public string? Product { get; set; } = "";
		public int? Amount { get; set; }
		public string? Date { get; set; } = "";
		public int? ProviderId { get; set; }
		public string? ProviderName { get; set; } = "";
		public List<Provider>? Providers { get; set; }
	}
}
