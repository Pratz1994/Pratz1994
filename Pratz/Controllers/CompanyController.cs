using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace Pratz.Controllers
{
    public class CompanyController : Controller
    {
        private TestEntities db = new TestEntities();


        // GET: Company
        public ActionResult Index()
        {
            var company = db.CompanyTables.Where(x => x.CustomerId == x.Customer.Id);
            return View(company.ToList());
        }

        public JsonResult GetCustomerById(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            //var company = db.CompanyTables.Where(x => x.Id == Id ).FirstOrDefault();
            var customer = db.Customers.Where(p => p.Id == Id).FirstOrDefault();



            return Json(customer, JsonRequestBehavior.AllowGet);

        }


        // GET: Company/Edit/5
        public ActionResult Edit(Customer customer)
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

      
    }
}
