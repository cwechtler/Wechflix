using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wechflix.Models
{
	public static class RoleNames
	{
		public const string CanManageMovies = "FullAdmin,CanManageMovies";
		public const string CanManageCustomers = "FullAdmin,CanManageCustomers";
		public const string CanManageUsers = "FullAdmin,CanManageUsers";
		public const string FullAdmin = "FullAdmin";

		public static List<string> AllRoles = new List<string> { CanManageMovies, CanManageCustomers, CanManageUsers, FullAdmin };
	}
}