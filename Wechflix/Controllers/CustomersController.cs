﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wechflix.Models;
using Wechflix.ViewModels;

namespace Wechflix.Controllers
{
	public class CustomersController : Controller
	{
		private ApplicationDbContext _context;

		public CustomersController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Details(int id)
		{
			var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

			if (customer == null) {
				return HttpNotFound();
			}

			return View(customer);
		}

		[Authorize(Roles = (RoleNames.CanManageCustomers))]
		public ActionResult CreateNew()
		{
			var viewModel = new CustomerFormViewModel {
				Customer = new Customer(),
				MembershipTypes = _context.MembershipTypes.ToList()
			};

			return View("CustomerForm", viewModel);
		}

		[Authorize(Roles = (RoleNames.CanManageCustomers))]
		public ActionResult Edit(int id)
		{
			var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

			if (customer == null) {
				return HttpNotFound();
			}

			var viewModel = new CustomerFormViewModel {
				Customer = customer,
				MembershipTypes = _context.MembershipTypes.ToList()
			};
			return View("CustomerForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = (RoleNames.CanManageCustomers))]
		public ActionResult Save(Customer customer)
		{
			if (!ModelState.IsValid) {
				var viewModel = new CustomerFormViewModel {
					Customer = customer,
					MembershipTypes = _context.MembershipTypes.ToList()
				};

				return View("CustomerForm", viewModel);
			}

			if (customer.Id == 0) {
				_context.Customers.Add(customer);
			}
			else {
				var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

				customerInDb.Name = customer.Name;
				customerInDb.Birthdate = customer.Birthdate;
				customerInDb.MembershipTypeId = customer.MembershipTypeId;
				customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
			}

			_context.SaveChanges();

			return RedirectToAction("Index", "Customers");
		}
	}
}