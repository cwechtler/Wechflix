using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Wechflix.Models;

namespace Wechflix.ViewModels
{
	public class MovieFormViewModel
	{
		public IEnumerable<Genre> Genres { get; set; }
		//public Movie movie { get; set; }

		public int? Id { get; set; }

		[Required(ErrorMessage = "Please enter Movie's Name")]
		[StringLength(255)]
		public string Name { get; set; }

		[Display(Name = "Genre")]
		[Required]
		public int? GenreId { get; set; }

		[Display(Name = "Release Date")]
		[Required]
		public DateTime? ReleaseDate { get; set; }

		[Display(Name = "Number In Stock")]
		[Range(1, 50, ErrorMessage = "Value Must be between 1 and 50")]
		[Required]
		public byte? NumberInStock { get; set; }

		public string Title
		{
			get 
			{
				return Id != 0 ? "Edit Movie" : "New Movie";
			}
		}

		public MovieFormViewModel()
		{
			Id = 0;
		}

		public MovieFormViewModel(Movie movie)
		{
			Id = movie.Id;
			Name = movie.Name;
			GenreId = movie.GenreId;
			ReleaseDate = movie.ReleaseDate;
			NumberInStock = movie.NumberInStock;
		}
	}
}