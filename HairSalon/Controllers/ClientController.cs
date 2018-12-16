using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class ClientController : Controller
    {
        [HttpGet("/clients")]
        public ActionResult Index()
        {
            List<Client> allClients = Client.GetAll();
            return View(allClients);
        }
        [HttpGet("/clients/new")]
        public ActionResult New()
        {
            return View();
        }
        [HttpPost("/clients")]
        public ActionResult Index(string clientName)
        {
            Client Client = new Client(clientName);
            Client.Save();
            List<Client> allClients = Client.GetAll();
            return View(allClients);
        }
        [HttpGet("/clients/{id}")]
        public ActionResult Show(int id)
        {
            Dictionary<string,object> model= new Dictionary<string,object>();
            Client Client = Client.Find(id);
            List<Stylist> clientStylist= Client.GetStylist();
            model.Add("Client",Client);
            model.Add("clientStylist",clientStylist);
            return View(model);
        }
        [HttpGet("/clients/{id}/customer")]
        public ActionResult Customer(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Client Client = Client.Find(id);
            List<Stylist> availablebooks = Stylist.GetAvailableStylist();
            model.Add("Client", Client);
            model.Add("availableBooks", availablebooks);
            return View(model);
        }
        [HttpPost("/clients/{id}/customer/show")]
        public ActionResult Show(int customer, DateTime dueDate, int id)
        {
            Dictionary<string,object> model= new Dictionary<string,object>();
            int copyNumber = Stylist.FindCustomer(customer)-1;
            Stylist.Customer(customer,copyNumber);
            Client Client = Client.Find(id);
            Client.AddClients(customer, dueDate);
            List<Stylist> clientStylist= Client.GetStylist();
            
            model.Add("Client",Client);
            model.Add("clientStylist",clientStylist);
            return View(model);
        }
        [HttpGet("/clients/{id}/edit")]
        public ActionResult Edit(int id)
        {
            Client Client = Client.Find(id);
            return View(Client);
        }
        [HttpPost("/clients/{id}")]
        public ActionResult Show(string newPatron, int id)
        {
            Client Client = Client.Find(id);
            Client.Edit(newPatron);
            Dictionary<string,object> model= new Dictionary<string,object>();
            List<Stylist> clientStylist= Client.GetStylist();
            model.Add("Client",Client);
            model.Add("clientStylist",clientStylist);
            return View(model);
        }
    }
}
