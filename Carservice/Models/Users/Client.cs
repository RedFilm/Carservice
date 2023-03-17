using Microsoft.AspNetCore.Identity;

namespace Carservice.Models.Users
{
    public class Client : AppUser
    {
        public int PurchasedServicesCount { get; set; }
    }
}
