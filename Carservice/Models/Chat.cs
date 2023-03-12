using Carservice.Models.Users;

namespace Carservice.Models
{
	public class Chat
	{
		public int Id { get; set; }
		public int ClientId { get; set; }
		public Client? Client { get; set; }
		public int EmployeeId { get; set; }
		public Employee? Employee { get; set; }
	}
}
