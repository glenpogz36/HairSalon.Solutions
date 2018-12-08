using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Models;


namespace HairSalon.Controllers
{
  public class EmployeeController : Controller
  {


    [HttpGet("/employee")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/employee/new")]
       public ActionResult Create(string Name)
       {
           Employee newEmployee = new Employee(Name);
           List<Employee> allemployee = Employee.GetAll();
           return View("Index", allemployee);
       }
    [HttpGet("/employee/show")]
    public ActionResult Show()
    {
      List<Employee> allemployee = Employee.GetAll();
         return View(allemployee);
    }
     // [HttpGet("/employee/{id}")]
     //      public ActionResult Show(int Id)
     //      {
     //          Dictionary<string, object> model = new Dictionary<string, object>();
     //          Employee newEmployee = Employee.Find(Id);
     //          List<Employee> allemployee = newEmployee.GetAll();
     //          model.Add("employee", newEmployee);
     //          model.Add("details", details);
     //          return View(model);
     //      }

  }
}
