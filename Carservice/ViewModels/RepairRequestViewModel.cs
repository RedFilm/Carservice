using System.ComponentModel.DataAnnotations;

namespace Carservice.ViewModels
{
	public class RepairRequestViewModel
	{
		public string? RequestStatus { get; set; } = "";
		public int MadeYear { get; set; }
		public string? CarBrand { get; set; } = "";
		public string? Mileage { get; set; } = "";
		public string Servises { get; set; } = "";
		[Required]
		public string UserName { get; set; } = "";
		[Required]
		public string Email { get; set; } = "";
		public string? ContactNumber { get; set; } = "";
		public string? RequestText { get; set; } = "";
	}
}
