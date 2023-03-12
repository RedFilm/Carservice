using Microsoft.AspNetCore.Identity;

namespace Carservice.Models.Users
{
	public class Employee : AppUser
    {
		public override string? Id { get; set; }
		public int PassportNumber { get; set; }
		public List<Chat>? Chats { get; set; }
	}
}
