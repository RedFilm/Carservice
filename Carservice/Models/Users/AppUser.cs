using Carservice.Models.Repair;
using Microsoft.AspNetCore.Identity;

namespace Carservice.Models.Users
{
    public class AppUser : IdentityUser
    {
		public string FirstName { get; set; } = "";
		public string LastName { get; set; } = "";
		public string SurName { get; set; } = "";

		public List<RepairRequest>? RepairRequests { get; set; }
	}
}
