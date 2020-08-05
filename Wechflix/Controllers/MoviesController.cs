using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wechflix.Models;

namespace Wechflix.Controllers
{
	public class MoviesController : Controller
	{
		ApplicationDbContext _context;

		public MoviesController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		public ActionResult Index()
		{
			var movies =  _context.Movies.Include(m => m.Genre).ToList();
			return View(movies);
		}

		public ActionResult Details(int id) {
			var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

			if (movie ==null) {
				return HttpNotFound();
			}

			return View(movie);
		}

		public ActionResult Random()
		{
			var movie = new Movie() { Name = "Shrek!" };
			return View(movie);
		}
	}
}