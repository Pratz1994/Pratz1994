using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Web.Script.Serialization;
using Pratz;


namespace Pratz.Controllers
{
    public class CustomerController : Controller
    {
        private TestEntities db = new TestEntities();
        // GET: Customer
        public ActionResult Index()
        {
            db.Configuration.ProxyCreationEnabled = false;

            List<Customer> customers = db.Customers.ToList();
            return View(customers);
        }

        public JsonResult GetAllCustomers()
        {
            db.Configuration.ProxyCreationEnabled = false;

            List<Customer> customers = db.Customers.ToList();

            return Json(customers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustomerById(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Customer customer = db.Customers.Where(x => x.Id == Id).SingleOrDefault();
            return Json(customer, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateCustomer(Customer customer)
        {
            var result = false;
            try
            {
                Customer customers = db.Customers.Where(x => x.Id == customer.Id).SingleOrDefault();
                customers.Name = customer.Name;
                customers.Address = customer.Address;
                customers.Status = customer.Status;
                customers.Contact = customer.Contact;
                customers.Date = customer.Date;
                customers.Notes = customer.Notes;

                db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddCustomer(Customer customer)
        {
            var result = false;
            try
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int Id)
        {
            Customer customer = db.Customers.Where(x => x.Id == Id).SingleOrDefault();
            return View(customer);
        }

    }
}
