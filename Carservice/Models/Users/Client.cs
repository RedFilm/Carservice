using Microsoft.AspNetCore.Identity;

namespace Carservice.Models.Users
{
    public class Client : AppUser
    {
        public override string? Id { get; set; }
        public int PurchasedServices { get; set; }
        public List<Chat>? Chats { get; set; }
    }
}
