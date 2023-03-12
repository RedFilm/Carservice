using Carservice.Models;
using Carservice.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Carservice.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
		public DbSet<Employee> Employees { get; set; }
        public DbSet<Chat> Chats { get; set; }  
	}
}
