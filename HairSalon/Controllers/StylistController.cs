using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{

    public class StylistController : Controller
    {
        [HttpGet("/stylists")]
        public ActionResult Index()
        {
            List<Stylist> allStylits = Stylist.GetAll();
            return View(allStylits);
        }
        [HttpGet("/stylists/new")]
        public ActionResult New()
        {
            List<Employee> allEmployees = Employee.GetAll();
            return View(allEmployees);
        }
        [HttpPost("/stylists")]
        public ActionResult Index(string stylistname, int stylistemployee, int customers)
        {
            Stylist stylists = new Stylist(stylistname);
            stylists.Save();
            stylists.AddEmployee(stylistemployee);
            stylists.AddCustomers(customers);
            List<Stylist> allStylists = Stylist.GetAll();

            return View(allStylists);
        }
        [HttpGet("/stylists/{id}")]
        public ActionResult Show(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Stylist stylist = Stylist.Find(id);
            Employee employee = stylist.GetEmployee();
            List<Client> clients = stylist.GetClients();
            model.Add("stylist", stylist);
            model.Add("employee", employee);
            model.Add("clients", clients);
            return View(model);
        }
        [HttpGet("/stylists/{id}/edit")]
        public ActionResult Edit(int id)
        {
            Stylist stylist = Stylist.Find(id);
            return View(stylist);
        }
        [HttpPost("/stylists/{id}")]
        public ActionResult Show(string newStylist, int id)
        {
            Stylist stylist = Stylist.Find(id);
            stylist.Edit(newStylist);
            Dictionary<string, object> model = new Dictionary<string, object>();
            Employee employee = stylist.GetEmployee();
            List<Client> clients = stylist.GetClients();
            model.Add("stylist", stylist);
            model.Add("employee", employee);
            model.Add("clients", clients);
            return View(model);
        }
        [HttpGet("/stylists/{stylistId}/delete")]
        public ActionResult Delete(int stylistId)
        {
            Stylist thisStylist = Stylist.Find(stylistId);
            thisStylist.Delete();
            return RedirectToAction("Index");
        }
    }
}
