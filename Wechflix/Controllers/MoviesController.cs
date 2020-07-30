using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wechflix.Models;

namespace Wechflix.Controllers
{
	public class MoviesController : Controller
	{
		public ActionResult Index()
		{
			var movies = GetMovies();
			return View(movies);
		}

		// GET: Movies/Random
		public ActionResult Random()
		{
			var movie = new Movie() { Name = "Shrek!" };
			return View(movie);
		}

		private IEnumerable<Movie> GetMovies()
		{
			return new List<Movie>
			{
				new Movie {Id = 1, Name = "Shrek!" },
				new Movie {Id = 2, Name = "Iron Man" }
			};

		}
	}
}