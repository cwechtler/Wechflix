using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Wechflix.Models;

namespace Wechflix.Dtos
{
	public class MovieDto
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter Movie's Name")]
		[StringLength(255)]
		public string Name { get; set; }

	
		[Required]
		public int GenreId { get; set; }
		public GenreDto Genre { get; set; }

		public DateTime ReleaseDate { get; set; }

		public DateTime DateAdded { get; set; }

		[Range(1, 50, ErrorMessage = "Value Must be between 1 and 50")]
		public byte NumberInStock { get; set; }
	}
}