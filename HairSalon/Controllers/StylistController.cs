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
            List<Stylist> allStylists= Stylist.GetAll();
            return View(allStylists);
        }
        [HttpGet("/stylists/new")]
        public ActionResult New()
        {
             List<Employee> allEmployees= Employee.GetAll();
            return View(allEmployees);
        }
        [HttpPost("/stylists")]
        public ActionResult Index(string stylistname, int style, int customer)
        {
            Stylist stylist = new Stylist(stylistname);
            stylist.Save();
            stylist.AddEmployee(style);
            stylist.AddCustomer(customer);
            List<Stylist> allStylists= Stylist.GetAll();

            return View(allStylists);
        }
        [HttpGet("/stylists/{id}")]
        public ActionResult Show(int id)
        {
            Dictionary<string,object> model = new Dictionary<string,object>();
            Stylist stylists= Stylist.Find(id);
            Employee employees= stylists.GetEmployee();
            List<Client> clients = stylists.GetClient();
            model.Add("stylists",stylists);
            model.Add("employees",employees);
            model.Add("clients",clients);
            return View(model);
        }
        [HttpGet("/stylists/{id}/edit")]
        public ActionResult Edit(int id)
        {
            Stylist stylists = Stylist.Find(id);
            return View(stylists);
        }
        [HttpPost("/stylists/{id}")]
        public ActionResult Show(string newTitle, int id)
        {
            Stylist stylists = Stylist.Find(id);
            stylists.Edit(newTitle);
            Dictionary<string,object> model = new Dictionary<string,object>();
            Employee employees= stylists.GetEmployee();
            List<Client> clients = stylists.GetClient();
            model.Add("stylists",stylists);
            model.Add("employees",employees);
            model.Add("clients",clients);
            return View(model);
        }
    }
}
