using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wechflix.Models;

namespace Wechflix.ViewModels
{
	public class MovieFormViewModel
	{
		public IEnumerable<Genre> Genres { get; set; }
		public Movie movie { get; set; }
	}
}