using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wechflix.Models
{
	public class Movie
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter Movie's Name")]
		[StringLength(255)]
		[CheckDuplicateMovieName]
		public string Name { get; set; }

		public Genre Genre { get; set; }

		[Display(Name = "Genre")]
		[Required]
		public int GenreId { get; set; }


		[Display(Name = "Release Date")]
		public DateTime ReleaseDate { get; set; }

		public DateTime DateAdded { get; set; }

		[Display(Name = "Number In Stock")]
		[Range(1,50, ErrorMessage = "Value Must be between 1 and 50")]
		public byte NumberInStock { get; set; }


		public byte NumberAvailable { get; set; }
	}
}