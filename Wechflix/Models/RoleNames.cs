using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wechflix.Models
{
	public static class RoleNames
	{
		public const string CanManageMovies = "CanManageMovies";
		public const string CanManageCustomers = "CanManageCustomers";
		public const string CanManageUsers = "CanManageUsers";
		public const string FullAdmin = "FullAdmin";

		public static List<string> AllRoles = new List<string> { CanManageMovies, CanManageCustomers, CanManageUsers, FullAdmin };
	}
}