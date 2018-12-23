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
      List<Client> clientList = Client.GetAll();
      return View("Index", clientList);
    }

    [HttpGet("/stylists/{stylistId}/clients/new")]
    public ActionResult CreateForm(int stylistId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      return View(stylist);
    }

    [HttpGet("/stylists/{stylistId}/clients/{clientId}")]
    public ActionResult Details(int stylistId, int clientId, int specialtyId)
    {
      Client client = Client.Find(clientId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("client", client);
      model.Add("stylist", stylist);
      return View(model);
    }
    [HttpGet("clients/delete")]
    public ActionResult DeleteAll()
    {

      Client.DeleteAll();
        return RedirectToAction("Index");
    }

    [HttpGet("/clients/{clientId}/update")]
    public ActionResult UpdateForm(int clientId)
    {
      Client thisClient = Client.Find(clientId);
      return View(thisClient);
    }

    [HttpPost("/clients/{clientId}/update")]
    public ActionResult Update(int clientId)
    {
      Client thisClient = Client.Find(clientId);
      thisClient.Edit(Request.Form["new-client-name"]);
      return RedirectToAction("Index");
    }

    [HttpGet("/clients/{clientId}/delete")]
    public ActionResult DeleteOne(int clientId)
    {
      Client thisClient = Client.Find(clientId);
      thisClient.Delete();
      return RedirectToAction("Index");
    }
 }
}