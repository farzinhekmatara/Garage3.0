﻿using Garage3._0.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Garage3._0.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult MemberShipRegister()
		{
			return View();
		}

		public IActionResult RegisterVehicle()
        {
			return View();
        }

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}