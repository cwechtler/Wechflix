﻿using System;
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

		public ActionResult RandomMovie()
		{
			Random random = new Random();
			int randomId = random.Next(1, _context.Movies.Count());

			var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == randomId);
			return View("Details", movie);
		}

		public ActionResult CreateNew()
		{
			var movie = new Movie();
			var viewModel = new MovieFormViewModel {
				movie = movie,
				Genres = _context.Genres.ToList()
			};
			return View("MovieForm", viewModel);
		}

		public ActionResult Edit(int id)
		{
			var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

			if (movie == null) {
				return HttpNotFound();
			}

			var viewModel = new MovieFormViewModel {
				movie = movie,
				Genres = _context.Genres.ToList()
			};


			return View("MovieForm", viewModel);
		}

		[HttpPost]
		public ActionResult Save(Movie movie)
		{
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
	}
}