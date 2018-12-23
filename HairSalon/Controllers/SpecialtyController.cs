using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class SpecialtyController : Controller
  {

    [HttpGet("/specialties")]
    public ActionResult Index()
    {
      List<Specialty> allSpecialties = Specialty.GetAll();
      return View(allSpecialties);
    }

    [HttpGet("/specialties/new")]
    public ActionResult CreateForm()
    {
      return View();
    }

    [HttpPost("/specialties")]
    public ActionResult Create()
    {
      Specialty newSpecialty = new Specialty(Request.Form["new-specialty"]);
      newSpecialty.Save();
      List<Specialty> allSpecialties = Specialty.GetAll();
      return RedirectToAction("Index");
    }

    [HttpGet("/stylists/{stylistId}/specialties/{specialtyId}")]
    public ActionResult Details(int stylistId, int specialtyId)
    {
      Specialty specialty = Specialty.Find(specialtyId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("specialty", specialty);
      model.Add("stylist", stylist);
      return View(specialty);
    }

    [HttpGet("/specialties/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Specialty selectedSpecialty = Specialty.Find(id);
      List<Stylist> specialtyStylist = selectedSpecialty.GetStylists();
      List<Stylist> allStylist = Stylist.GetAll();
      model.Add("selectedSpecialty", selectedSpecialty);
      model.Add("specialtyStylist", specialtyStylist);
      model.Add("allStylist", allStylist);
      return View(model);
    }

    [HttpPost("/specialties/{specialtyId}/stylists/new")]
    public ActionResult AddStylist(int specialtyId)
    {
      Specialty specialty = Specialty.Find(specialtyId);
      Stylist stylist = Stylist.Find(Int32.Parse(Request.Form["stylist-id"]));
      specialty.AddStylist(stylist);
      return RedirectToAction("Index");
    }

    [HttpGet("/specialties/{specialtyId}/delete")]
    public ActionResult DeleteOne(int specialtyId)
    {
      Specialty thisSpecialty = Specialty.Find(specialtyId);
      thisSpecialty.Delete();
      return RedirectToAction("Index");
    }

    [HttpGet("/specialties/{speicaltyId}/update")]
    public ActionResult UpdateForm(int speicaltyId)
    {
      Specialty thisSpecialty = Specialty.Find(speicaltyId);
      return View(thisSpecialty);
    }

    [HttpPost("/specialties/{speicaltyId}/update")]
    public ActionResult Update(int speicaltyId)
    {
      Specialty thisSpecialty = Specialty.Find(speicaltyId);
      thisSpecialty.Edit(Request.Form["new-specialty-name"]);
      return RedirectToAction("Index");
    }

    [HttpPost("/specialties/delete")]
    public ActionResult DeleteAll()
    {

      Specialty.DeleteAll();
      return View();
    }
  }
}