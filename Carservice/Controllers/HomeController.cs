﻿using Carservice.Data;
using Carservice.Models.Repair;
using Carservice.Models.Users;
using Carservice.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Carservice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly UserManager<AppUser> _userManager;
		private readonly AppDbContext _ctx;

		public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, AppDbContext ctx)
        {
            _logger = logger;
			_userManager = userManager;
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> RepairRequest()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

			var vm = new RepairRequestViewModel()
            {
                UserName = user.UserName,
                Email= user.Email,
            };

            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RepairRequest(RepairRequestViewModel repairRequestVm)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest("Invalid request form");
            //}

			var user = await _userManager.GetUserAsync(HttpContext.User);
            var waitStatus = await _ctx.RequestStatuses.FirstOrDefaultAsync(s => s.Name == "Ожидает обработки");

            var servList = "";

            foreach (var service in repairRequestVm.Services)
            {
                servList += $"{service} || ";
            }

            var request = new RepairRequest()
            {
                AppUserId = user.Id,
                RequestStatus = waitStatus,
                RequestStatusId = waitStatus.Id,
                MadeYear = repairRequestVm.MadeYear,
                CarBrand = repairRequestVm.CarBrand,
                Mileage = repairRequestVm.Mileage,
                UserName = repairRequestVm.UserName,
                Email = repairRequestVm.Email,
                ContactNumber = repairRequestVm.ContactNumber,
                RequestText = repairRequestVm.RequestText,
                PreferedDay = repairRequestVm.PreferedDay,
                TimeFrame = repairRequestVm.TimeFrame,
                CarNumber = repairRequestVm.CarNumber,
                VinNumber = repairRequestVm.VinNumber,
                Date = repairRequestVm.Date,
                Services = servList,
            };
            await _ctx.RepairRequests.AddAsync(request);
            await _ctx.SaveChangesAsync();

			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
        public IActionResult Reviews() => View();


        [HttpGet]
        public IActionResult Services()
        {
            //var services = _ctx.Services.ToList();

            return View();
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}