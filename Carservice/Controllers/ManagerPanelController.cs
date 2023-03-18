using Carservice.Data;
using Carservice.Models.Repair;
using Carservice.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Carservice.Controllers
{
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
        public IActionResult Index()
        {
            var waitingRequests = _ctx.RepairRequests.Where(r => r.RequestStatusId == 1).ToList();

            return View(waitingRequests);
        }

        [HttpGet]
        public IActionResult RepairRequest(int? id)
        {
            if (id == null)
            {
                return View(new RepairRequest());
            }
            var repairRequest = _ctx.RepairRequests.First(r => r.Id == id);

            return View(repairRequest);
        }

		[HttpPost]
		public async Task<IActionResult> ChangeStatus(RepairRequest vm)
        {
            if (vm == null)
                return BadRequest("RepairRequestId is null");

			var request = _ctx.RepairRequests.First(r => r.Id == vm.Id);

			var status = _ctx.RequestStatuses.First(s => s.Id == vm.RequestStatusId);

            request.RequestStatusId = status.Id;
			_ctx.Entry(request).Reference(r => r.RequestStatus).Load();

            await _ctx.SaveChangesAsync();

			return RedirectToAction("Index");
		}
	}
}
