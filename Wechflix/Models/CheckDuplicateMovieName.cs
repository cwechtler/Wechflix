using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Wechflix.Models
{
	public class CheckDuplicateMovieName: ValidationAttribute
	{
		ApplicationDbContext _context = new ApplicationDbContext();

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var movie = (Movie)validationContext.ObjectInstance;
			if (!CheckDuplicateName(movie)) {
				return ValidationResult.Success;
			}

			return new ValidationResult("Movie Already Exists");
		}

		private bool CheckDuplicateName(Movie movie)
		{
			foreach (var movieInCollection in _context.Movies) {
				if (movie.Name == movieInCollection.Name &&
					movie.Id == 0) {
					return true;
				}
			}
			return false;
		}
	}
}