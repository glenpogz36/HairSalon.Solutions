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

    [HttpPost("/showemployee")]
        public ActionResult Create(string Name)
        {
            Employee newEmployee = new Employee(Name);
            List<Employee> allemployee = Employee.GetAll();
            return View("Index", allemployee);

        }

  }
}
