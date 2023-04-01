using Carservice.Data;
using Carservice.Models.Repair;
using Carservice.Models.Users;
using Carservice.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Carservice.Controllers
{
	[Authorize]
	public class UserPanelController : Controller
	{
		private AppDbContext _ctx;
		private UserManager<AppUser> _userMnr;

		public UserPanelController(AppDbContext ctx, UserManager<AppUser> userMnr)
		{
			_ctx = ctx;
			_userMnr = userMnr;
		}
		public async Task<IActionResult> Requests()
		{
			var user = await _userMnr.GetUserAsync(HttpContext.User);
			var requests = _ctx.RepairRequests.Where(r => r.AppUserId == user.Id);

			List<RepairRequestViewModel> vm = new List<RepairRequestViewModel>();
			foreach (var request in requests)
			{
				vm.Add(new RepairRequestViewModel()
				{
					AppUserId = user.Id,
					RequestStatus = _ctx.RequestStatuses.First(s => s.Id == request.RequestStatusId).Name,
					MadeYear = request.MadeYear,
					CarBrand = request.CarBrand,
					Mileage = request.Mileage,
					UserName = request.UserName,
					Email = request.Email,
					ContactNumber = request.ContactNumber,
					RequestText = request.RequestText,
					PreferedDay = request.PreferedDay,
					TimeFrame = request.TimeFrame,
					CarNumber = request.CarNumber,
					VinNumber = request.VinNumber,
					Date = request.Date,
					StrServices = request.Services,
				});
			}

			return View(vm);
		}
	}
}
