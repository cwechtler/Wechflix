using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wechflix.Models;
using Wechflix.ViewModels;

namespace Wechflix.Controllers
{
	[Authorize(Roles = RoleNames.CanManageUsers)]
	public class UsersController : Controller
	{
		ApplicationDbContext _context;
		UserManager<ApplicationUser> _userManager;

		public UsersController()
		{
			_context = new ApplicationDbContext();
			_userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
		}

		// GET: Users
		public ActionResult Index()
		{
			var users = _userManager.Users.ToList();
			List<string> roles = RolesByNameToList(users);

			var viewModel = new UserFormViewModel() {
				Users = users.ToList(),
				UserRoles = roles.ToList()
			};

			return View(viewModel);
		}

		public ActionResult Edit(string id)
		{
			var user = _userManager.Users.SingleOrDefault(c => c.Id == id);
			var users = _userManager.Users.ToList();
			List<string> roles = RolesByNameToList(users);

			if (user == null) {
				return HttpNotFound();
			}

			var viewModel = new UserFormViewModel() {
				Id = user.Id,
				Email = user.Email,
				UserphoneNumber = user.UserPhoneNumber,
				DriversLicenseNumber = user.DriversLicenceNumber,
				Users = users.ToList(),
				UserRoles = roles.ToList()
			};
			//viewModel.UserRole = viewModel.GetUserRole(id);

			return View("UserForm", viewModel);
		}

		[HttpPost]
		public ActionResult Save(UserFormViewModel viewModel)
		{
			if (!ModelState.IsValid) {
				return HttpNotFound("ViewModel State Invalid");
			}

			string id = viewModel.Id;
			var userInDb = _context.Users.SingleOrDefault(c => c.Id == id);

			//userInDb.UserName = viewModel.UserName;
			userInDb.Email = viewModel.Email;
			userInDb.UserPhoneNumber = viewModel.UserphoneNumber;
			userInDb.DriversLicenceNumber = viewModel.DriversLicenseNumber;

			_context.SaveChanges();

			return RedirectToAction("Index", "Users");
		}

		public ActionResult AddRoleToUser(UserFormViewModel viewModel) {
			if (!ModelState.IsValid) {
				return HttpNotFound("ViewModel State Invalid");
			}

			if (!User.IsInRole(viewModel.UserRole)) {
				_userManager.AddToRole(viewModel.Id, viewModel.UserRole);
				_context.SaveChanges();
			}

			return RedirectToAction("Index", "Users");
		}

		public ActionResult RemoveRoleFromUser(UserFormViewModel viewModel) {

			if (!ModelState.IsValid) {
				return HttpNotFound("ViewModel State Invalid");
			}

			if (User.IsInRole(viewModel.UserRole)) {
				_userManager.RemoveFromRole(viewModel.Id, viewModel.UserRole);
				_context.SaveChanges();
			}
			return RedirectToAction("Index", "Users");
		}

		public ActionResult AddRole(UserFormViewModel viewModel)
		{
			var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
			var roleManager = new RoleManager<IdentityRole>(roleStore);
			roleManager.Create(new IdentityRole(viewModel.UserRole));
			//await roleManager.CreateAsync(new IdentityRole("CanManageMovies"));

			return RedirectToAction("Index", "Users");
		}

		private List<string> RolesByNameToList(List<ApplicationUser> users)
		{
			var roles = new List<string>();
			foreach (var user in users) {
				string str = "";
				foreach (var role in _userManager.GetRoles(user.Id)) {
					str = (str == "") ? role.ToString() : str + " - " + role.ToString();
				}
				roles.Add(str);
			}

			return roles;
		}
	}
}