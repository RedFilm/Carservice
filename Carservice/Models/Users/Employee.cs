using Microsoft.AspNetCore.Identity;

namespace Carservice.Models.Users
{
	public class Employee : AppUser
    {
		public int PassportNumber { get; set; }
		public string Position { get; set; } = "";
		public int Salary { get; set; }
	}
}
