﻿using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;
using System.Runtime.Caching;
using System.Collections.Generic;

namespace Vidly.Controllers
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
            base.Dispose(disposing);    
        }

        public ViewResult Index()
        {
            if (MemoryCache.Default["Genres"] == null)
            {
                MemoryCache.Default["Genres"] = _context.Genres.ToList();
            }

            var genres = MemoryCache.Default["Genres"] as IEnumerable<Genre>;

            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(x => x.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {

                return HttpNotFound();
            }

            return View(customer);
        }


        public ActionResult New() 
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes= membershipTypes
            };
            return View("CustomerForm", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel 
                { 
                    Customer = customer, 
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
            {

                _context.Customers.Add(customer);
            }

            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }
                

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

    }
}