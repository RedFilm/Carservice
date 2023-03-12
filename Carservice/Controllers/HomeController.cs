﻿using Carservice.Data;
using Carservice.Models.Users;
using Carservice.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public IActionResult Reviews() => View();


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}