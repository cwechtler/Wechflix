﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wechflix.Controllers
{
	[AllowAnonymous]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "About Wech-Flix";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Wech-Flix";

			return View();
		}
	}
}