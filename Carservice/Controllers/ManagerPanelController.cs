using Carservice.Data;
using Carservice.Models;
using Carservice.Models.Repair;
using Carservice.Models.Users;
using Carservice.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Carservice.Controllers
{
    [Authorize]
    public class ManagerPanelController : Controller
    {
        private SignInManager<AppUser> _signInMnr;
        private UserManager<AppUser> _userMnr;
        private RoleManager<IdentityRole> _roleMnr;
		private AppDbContext _ctx;

		public ManagerPanelController(SignInManager<AppUser> signInMnr,
                                      UserManager<AppUser> userMnr,
                                      RoleManager<IdentityRole> roleMnr,
                                      AppDbContext ctx)
        {
            _signInMnr = signInMnr;
            _userMnr = userMnr;
            _roleMnr = roleMnr;
            _ctx = ctx;
        }

		[HttpGet]
		public IActionResult Index() => View();

        [HttpGet]
        public IActionResult RepairRequests()
        {
			var requestsVm = new List<RepairRequestViewModel>();

			var requests = _ctx.RepairRequests.Where(r => r.RequestStatusId == 1 || r.RequestStatusId == 2).ToList();

			foreach (var request in requests)
			{
				requestsVm.Add(new RepairRequestViewModel()
				{
					Id= request.Id,
					AppUserId = request.AppUserId,
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

            return View(requestsVm);
        }

		[HttpGet]
		public async Task<IActionResult> AddOrder()
		{
			var user = await _userMnr.GetUserAsync(HttpContext.User);

			var orderVm = new OrderViewModel();

			var providersList = _ctx.Providers.ToList();

			orderVm.Providers = providersList;
			orderVm.UserId = user.Id;
			orderVm.Manager = user.UserName;

			return View(orderVm);
		}

		[HttpPost]
		public async Task<IActionResult> AddOrder(OrderViewModel vm)
		{
			var order = new Order()
			{
				UserId = vm.UserId,
				UserName = vm.Manager,
				Product = vm.Product,
				Amount = vm.Amount,
				Date = vm.Date,
				ProviderId = vm.ProviderId,
			};

			await _ctx.Orders.AddAsync(order);
			await _ctx.SaveChangesAsync();

			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> RemoveRequest(RepairRequestViewModel vm)
		{
			var request = await _ctx.RepairRequests.FirstAsync(r => r.Id == vm.Id);

			_ctx.RepairRequests.Remove(request);
			await _ctx.SaveChangesAsync();

			return RedirectToAction("RepairRequests");
		}

		[HttpGet]
        public IActionResult RepairRequest(int? id)
        {
            if (id == null)
            {
                return View(new RepairRequest());
            }
            var requestModel = _ctx.RepairRequests.First(r => r.Id == id);

			var vm = new RepairRequestViewModel()
			{
				Id = requestModel.Id,
				AppUserId = requestModel.AppUserId,
				RequestStatus = _ctx.RequestStatuses.First(s => s.Id == requestModel.RequestStatusId).Name,
				MadeYear = requestModel.MadeYear,
				CarBrand = requestModel.CarBrand,
				Mileage = requestModel.Mileage,
				UserName = requestModel.UserName,
				Email = requestModel.Email,
				ContactNumber = requestModel.ContactNumber,
				RequestText = requestModel.RequestText,
				PreferedDay = requestModel.PreferedDay,
				TimeFrame = requestModel.TimeFrame,
				CarNumber = requestModel.CarNumber,
				VinNumber = requestModel.VinNumber,
				Date = requestModel.Date,
				StrServices = requestModel.Services,
			};

			return View(vm);
        }

		//[HttpPost]
		//public async Task<IActionResult> ChangeStatus(RepairRequest vm)
  //      {
  //          if (vm == null)
  //              return BadRequest("RepairRequestId is null");

		//	var request = _ctx.RepairRequests.First(r => r.Id == vm.Id);

		//	var status = _ctx.RequestStatuses.First(s => s.Id == vm.RequestStatusId);

  //          request.RequestStatusId = status.Id;
		//	_ctx.Entry(request).Reference(r => r.RequestStatus).Load();

  //          await _ctx.SaveChangesAsync();

		//	return RedirectToAction("Index");
		//}

		[HttpGet]
		public IActionResult Edit(RepairRequestViewModel vm) => View(vm);


		[HttpPost]
		public async Task<IActionResult> EditR(RepairRequestViewModel vm)
		{
			var status = await _ctx.RequestStatuses.FirstAsync(s => s.Name == vm.RequestStatus);

			var repairRequest = await _ctx.RepairRequests.FirstAsync(r => r.Id == vm.Id);

			var servList = "";

			foreach (var service in vm.Services)
			{
				servList += $"{service}";
			}

			repairRequest.AppUserId = vm.AppUserId;
			repairRequest.RequestStatus = status;
			repairRequest.RequestStatusId = status.Id;
			repairRequest.MadeYear = vm.MadeYear;
			repairRequest.CarBrand = vm.CarBrand;
			repairRequest.Mileage = vm.Mileage;
			repairRequest.UserName = vm.UserName;
			repairRequest.Email = vm.Email;
			repairRequest.ContactNumber = vm.ContactNumber;
			repairRequest.RequestText = vm.RequestText;
			repairRequest.PreferedDay = vm.PreferedDay;
			repairRequest.TimeFrame = vm.TimeFrame;
			repairRequest.CarNumber = vm.CarNumber;
			repairRequest.VinNumber = vm.VinNumber;
			repairRequest.Date = vm.Date;
			repairRequest.Services = servList;

			_ctx.Entry(repairRequest).Reference(r => r.RequestStatus).Load();
			_ctx.RepairRequests.Update(repairRequest);

			await _ctx.SaveChangesAsync();

			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public IActionResult DatabaseView()
		{
			var vm = new DbViewModel()
			{
				Context = _ctx,
			};

			return View(vm);
		}

	}
}
