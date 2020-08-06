﻿using AutoMapper;
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
	public class MoviesController : ApiController
	{
		ApplicationDbContext _context;

		public MoviesController()
		{
			_context = new ApplicationDbContext();
		}

		// GET /api/movies
		public IEnumerable<MovieDto> GetMovies()
		{	
			return _context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>);
		}

		// GET /api/movies/1
		public IHttpActionResult GetMovie(int id)
		{
			var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
			
			if (movie == null) {
				return NotFound();
			}
	
			return Ok(Mapper.Map<Movie, MovieDto>(movie));
		}

		// POST /api/movies
		[HttpPost]
		public IHttpActionResult CreateMovie(MovieDto movieDto)
		{
			if (!ModelState.IsValid) {
				return BadRequest();
			}

			var movie = Mapper.Map<MovieDto, Movie>(movieDto);

			movie.DateAdded = DateTime.Today;

			_context.Movies.Add(movie);
			_context.SaveChanges();

			movieDto.Id = movie.Id;
			movieDto.DateAdded = movie.DateAdded;

			return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
		}

		// PUT /api/movies/1
		[HttpPut]
		public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
		{
			if (!ModelState.IsValid) {
				return BadRequest();
			}

			var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

			if (movieInDb == null) {
				return NotFound();
			}

			Mapper.Map(movieDto, movieInDb);

			_context.SaveChanges();

			return Ok();
		}

		// DELETE /api/movies/1
		public IHttpActionResult DeleteMovie(int id)
		{
			var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

			if (movieInDb == null) {
				return NotFound();
			}

			_context.Movies.Remove(movieInDb);
			_context.SaveChanges();

			return Ok();
		}
	}
}