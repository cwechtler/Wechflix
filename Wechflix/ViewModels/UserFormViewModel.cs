using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wechflix.Models;

namespace Wechflix.ViewModels
{
	public class UserFormViewModel
	{
		public IList<ApplicationUser> Users { get; set; }
		public IList<string> UserRoles { get; set; }
		public IEnumerable<string> ListOfRoles { get; set; }
		public IList<string> AvailableRoleNames { get; set; }

		public string Id { get; set; }
		public string UserName { get; set; }	
		public string Email { get; set; }
		public string UserphoneNumber { get; set; }
		public string DriversLicenseNumber { get; set; }
		public string UserRole { get; set; }

		public UserFormViewModel()
		{
			var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
			var roleManager = new RoleManager<IdentityRole>(roleStore);
			ListOfRoles = roleManager.Roles.Select(x => x.Name).ToList();

			AvailableRoleNames = RoleNames.AllRoles;
		}

		public string GetUserRole(string id) {

			UserManager<ApplicationUser> _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
			ApplicationDbContext _context = new ApplicationDbContext();

			var users = _context.Users.ToList();
			var roles = new List<string>();
			foreach (var user in users) {
				string str = "";
				foreach (var role in _userManager.GetRoles(user.Id)) {
					str = (str == "") ? role.ToString() : str + " - " + role.ToString();
				}
				roles.Add(str);
			}

			Users = users.ToList();
			UserRoles = roles.ToList();

			string userRole = null;
			int i = 0;
			foreach (var user in Users) {
				if (user.Id == id) {
					userRole = UserRoles[i].ToString();
				}
				i++;
			}

			return userRole;
		}
	}
}