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
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpGet("/stylists/new")]
    public ActionResult CreateForm()
    {
      return View();
    }

    [HttpPost("/stylists")]
    public ActionResult Create()
    {
      Stylist newStylist = new Stylist(Request.Form["new-stylist"]);
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      return RedirectToAction("Index");
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(id);
      List<Client> stylistClients = selectedStylist.GetClients();
      model.Add("specialty", selectedStylist);
      model.Add("stylist", selectedStylist);
      model.Add("client", stylistClients);
      return View(model);
    }

    [HttpPost("/stylists/{id}")]
    public ActionResult CreateClient(string clientName, int stylistId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist foundStylist = Stylist.Find(stylistId);
      Client newClient = new Client(Request.Form["new-client"], stylistId);
      newClient.Save();
      List<Client> stylistClients = foundStylist.GetClients();
      model.Add("client", stylistClients);
      model.Add("stylist", foundStylist);
      return RedirectToAction("Details");
    }

    [HttpGet("/stylists/{id}/specialties")]
    public ActionResult Specialties(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(id);
      List<Specialty> stylistSpecialties = selectedStylist.GetSpecialties();
      List<Specialty> allSpecialties = Specialty.GetAll();
      model.Add("stylist", selectedStylist);
      model.Add("specialty", stylistSpecialties);
      model.Add("allSpecialties", allSpecialties);
      return View(model);
    }

    [HttpPost("/stylists/{stylistid}/specialties/new")]
    public ActionResult CreateSpecialty(int stylistId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      Specialty specialty = Specialty.Find(Int32.Parse(Request.Form["specialty-id"]));
      stylist.AddSpecialty(specialty);
      return RedirectToAction("Index");
    }

    [HttpPost("/stylists/delete")]
    public ActionResult DeleteAll()
    {

      Stylist.DeleteAll();
      return View();
    }

    [HttpGet("/stylists/{stylistId}/delete")]
    public ActionResult DeleteOne(int stylistId)
    {
      Stylist thisStylist = Stylist.Find(stylistId);
      thisStylist.Delete();
      return RedirectToAction("Index");
    }

    [HttpGet("/stylists/{stylistId}/update")]
    public ActionResult UpdateForm(int stylistId)
    {
      Stylist thisStylist = Stylist.Find(stylistId);
      return View(thisStylist);
    }

    [HttpPost("/stylists/{stylistId}/update")]
    public ActionResult Update(int stylistId)
    {
      Stylist thisStylist = Stylist.Find(stylistId);
      thisStylist.Edit(Request.Form["new-stylist-name"]);
      return RedirectToAction("Index");
    }

  }
}
