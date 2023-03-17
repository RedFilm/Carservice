using Carservice.Hubs;
using Microsoft.EntityFrameworkCore;
using Carservice.Data;
using Microsoft.AspNetCore.Identity;
using Carservice.Models.Users;
using Carservice.Models.Repair;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("MSSQLServer");

// Add services to the container.
builder.Services.AddControllersWithViews(opt => opt.EnableEndpointRouting = false);
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connection));
//builder.Services.AddSignalR();
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredUniqueChars = 0;
    opt.Password.RequiredLength = 3;
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.User.AllowedUserNameCharacters = null;
    opt.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = "/Auth/Login";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseMvcWithDefaultRoute();
app.UseStaticFiles();
app.UseRouting();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Home}/{action=Index}/{id?}");

//    endpoints.MapHub<NotificationHub>("/notification");
//});

try
{
    var scope = app.Services.CreateScope();
    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();

    if (!context.RequestStatuses.Any(s => s.Name == "Waiting" || s.Name == "Active" || s.Name == "Done"))
    {
        var waiting = new RequestStatus() { Name = "Waiting" };
		var active = new RequestStatus() { Name = "Active" };
		var done = new RequestStatus() { Name = "Done" };

		context.RequestStatuses.AddAsync(waiting).GetAwaiter().GetResult();
		context.RequestStatuses.AddAsync(active).GetAwaiter().GetResult();
		context.RequestStatuses.AddAsync(done).GetAwaiter().GetResult();

        context.SaveChangesAsync().GetAwaiter().GetResult();
	}

	var adminRole = new IdentityRole("Manager");
    if (!context.Roles.Any(r => r.Name == "Manager"))
    {
        roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
    }

    if (!context.Users.Any(u => u.UserName == "Manager"))
    {
        var adminUser = new AppUser
        {
            UserName = "Manager",
            Email = "Manager@test.com"
		};
        var result = userMgr.CreateAsync(adminUser, "123").GetAwaiter().GetResult();

        userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}


app.Run();
