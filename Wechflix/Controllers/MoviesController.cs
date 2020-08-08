using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wechflix.Models;
using Wechflix.ViewModels;

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
			if (User.IsInRole(RoleNames.CanManageMovies)){
				return View("List");
			}
			return View("ReadOnlyList");
		}

		public ActionResult Details(int id) {
			var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

			if (movie == null) {
				return HttpNotFound();
			}

			if (User.IsInRole(RoleNames.CanManageMovies)) {
				return View(movie);
			}
			return View("DetailsNoEdit", movie);
		}

		public ActionResult RandomMovie()
		{
			int randomId = RandomMovieGenerator();

			var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == randomId);

			if (movie == null) {
				return HttpNotFound("Movie is null");
			}

			if (User.IsInRole("CanManageMovies")) {
				return View("Details", movie);
			}
			return View("DetailsNoEdit", movie);
		}

		[Authorize(Roles = RoleNames.CanManageMovies)]
		public ActionResult CreateNew()
		{
			var viewModel = new MovieFormViewModel 
			{
				Genres = _context.Genres.ToList()
			};

			return View("MovieForm", viewModel);
		}

		[Authorize(Roles = RoleNames.CanManageMovies)]
		public ActionResult Edit(int id)
		{
			var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

			if (movie == null) {
				return HttpNotFound();
			}

			var viewModel = new MovieFormViewModel(movie) {
				Genres = _context.Genres.ToList()
			};


			return View("MovieForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = RoleNames.CanManageMovies)]
		public ActionResult Save(Movie movie)
		{
			if (!ModelState.IsValid) {
				var viewModel = new MovieFormViewModel(movie) 
				{
					Genres = _context.Genres.ToList()
				};

				return View("MovieForm", viewModel);
			}

			if (movie.Id == 0) {
				movie.DateAdded = DateTime.Now;
				_context.Movies.Add(movie);
			}
			else {
				var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

				movieInDb.Name = movie.Name;
				movieInDb.ReleaseDate = movie.ReleaseDate;
				movieInDb.NumberInStock = movie.NumberInStock;
				movieInDb.GenreId = movie.GenreId;
			}
			_context.SaveChanges();

			return RedirectToAction("Index", "Movies");
		}

		private int RandomMovieGenerator()
		{
			var movieList = _context.Movies.ToList();
			List<int> idList = new List<int>();

			foreach (var m in movieList) {
				idList.Add(m.Id);
			}

			Random random = new Random();
			int randomIndex = random.Next(1, idList.Count);

			return idList[randomIndex];
		}
	}
}