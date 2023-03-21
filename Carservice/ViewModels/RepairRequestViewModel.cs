using System.ComponentModel.DataAnnotations;

namespace Carservice.ViewModels
{
	public class RepairRequestViewModel
	{
		public string? RequestStatus { get; set; } = "";
		[Required]
		public int MadeYear { get; set; }
		[Required]
		public string CarBrand { get; set; } = "";
		[Required]
		public string Mileage { get; set; } = "";
		public string? Servises { get; set; } = "";
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
	}
}
