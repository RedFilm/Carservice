using System.ComponentModel.DataAnnotations;

namespace Carservice.ViewModels
{
	public class RepairRequestViewModel
	{
		public int Id { get; set; }
		public string? AppUserId { get; set; } = "";
		public string? RequestStatus { get; set; } = "";
		[Required]
		public int MadeYear { get; set; }
		[Required]
		public string CarBrand { get; set; } = "";
		[Required]
		public string Mileage { get; set; } = "";
		[Required]
		public string UserName { get; set; } = "";
		[Required]
		public string Email { get; set; } = ""; 
		[Required]
		public string ContactNumber { get; set; } = "";
		public string? RequestText { get; set; } = "";
		public string? PreferedDay { get; set; } = "";
		public string? TimeFrame { get; set; } = "";
		public string VinNumber { get; set; } = "";
		public string CarNumber { get; set; } = "";
		public string? Date { get; set; } = "";
		public List<string>? Services { get; set; }
		public string? StrServices { get; set; } = "";
	}
}
