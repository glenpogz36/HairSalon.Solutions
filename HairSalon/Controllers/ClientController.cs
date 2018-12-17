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
        public ActionResult Index(string clientname)
        {
            Client client = new Client(clientname);
            client.Save();
            List<Client> allClients = Client.GetAll();
            return View(allClients);
        }
        [HttpGet("/clients/{id}")]
        public ActionResult Show(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Client client = Client.Find(id);
            List<Stylist> clientStylists = client.GetStylists();
            model.Add("client", client);
            model.Add("clientStylists", clientStylists);
            return View(model);
        }
        [HttpGet("/clients/{id}/checkout")]
        public ActionResult Checkout(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Client client = Client.Find(id);
            List<Stylist> availablestylists = Stylist.GetAvailableStylists();
            model.Add("client", client);
            model.Add("availableStylists", availablestylists);
            return View(model);
        }
        [HttpPost("/clients/{id}/checkout/show")]
        public ActionResult Show(int checkoutStylist, DateTime dueDate, int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            int customerNumber = Stylist.FindCustomer(checkoutStylist) - 1;
            Stylist.Checkout(checkoutStylist, customerNumber);
            Client client = Client.Find(id);
            client.AddCustomersClients(checkoutStylist, dueDate);
            List<Stylist> clientStylists = client.GetStylists();

            model.Add("client", client);
            model.Add("clientStylists", clientStylists);
            return View(model);
        }
        [HttpGet("/clients/{id}/edit")]
        public ActionResult Edit(int id)
        {
            Client client = Client.Find(id);
            return View(client);
        }
        [HttpPost("/clients/{id}")]
        public ActionResult Show(string newClient, int id)
        {
            Client client = Client.Find(id);
            client.Edit(newClient);
            Dictionary<string, object> model = new Dictionary<string, object>();
            List<Stylist> clientStylists = client.GetStylists();
            model.Add("client", client);
            model.Add("clientStylists", clientStylists);
            return View(model);
        }

        [HttpGet("/clients/{clientId}/delete")]
        public ActionResult Delete(int clientId)
        {
            Client thisClient = Client.Find(clientId);
            thisClient.Delete();
            return RedirectToAction("Index");
        }
    }
}
