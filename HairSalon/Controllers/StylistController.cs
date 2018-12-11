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
}
}