using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wechflix.Dtos;
using Wechflix.Models;

namespace Wechflix.Controllers.Api
{
	public class NewRentalsController : ApiController
	{
		ApplicationDbContext _context;

		public NewRentalsController()
		{
			_context = new ApplicationDbContext();
		}

		[HttpPost]
		public IHttpActionResult CreateNewRental(NewRentalDto newRental)
		{
			if (newRental.MovieIds.Count == 0) {
				return BadRequest("No Movie Ids have been given.");
			}

			var customer = _context.Customers.Single(c => c.Id == newRental.CustomerId);

			var movies = _context.Movies.Where(m => newRental.MovieIds.Contains(m.Id)).ToList();

			foreach (var movie in movies) {
				if (movie.NumberAvailable == 0) {
					return BadRequest("Movie is not available.");
				}

				movie.NumberAvailable--;

				var rental = new Rental {
					Customer = customer,
					Movie = movie,
					DateRented = DateTime.Now
				};

				_context.Rentals.Add(rental);
			}

			_context.SaveChanges();

			return Ok();
		}
	}
}
