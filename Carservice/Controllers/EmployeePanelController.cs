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
	public class EmployeePanelController : Controller
	{
		private AppDbContext _ctx;
		private UserManager<AppUser> _userMnr;

		public EmployeePanelController(AppDbContext ctx, UserManager<AppUser> userMnr)
		{
			_ctx = ctx;
			_userMnr = userMnr;
		}
		public async Task<IActionResult> Requests()
		{
			var requests = _ctx.RepairRequests.Where(r => r.RequestStatus.Name == "Обработана");

			List<RepairRequestViewModel> vm = new List<RepairRequestViewModel>();

			foreach (var request in requests)
			{
				vm.Add(new RepairRequestViewModel()
				{
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
