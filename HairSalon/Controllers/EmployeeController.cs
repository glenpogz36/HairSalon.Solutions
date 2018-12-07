using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Models;


namespace HairSalon.Controllers
{
  public class EmployeeController : Controller
  {

    [HttpGet("/employee")]
    public ActionResult Index()
    {
      return View();
    }

  }
}
