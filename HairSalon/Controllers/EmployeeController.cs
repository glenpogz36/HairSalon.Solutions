using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class EmployeeController : Controller
    {
         [HttpGet("/employees")]
        public ActionResult Index()
        {
            List<Employee> allEmployee= Employee.GetAll();
            return View(allEmployee);
        }
        [HttpGet("/employees/new")]
        public ActionResult New()
        {
            return View();
        }
        [HttpPost("/employees")]
        public ActionResult Index(string employeeName)
        {
            Employee employee = new Employee(employeeName);
            employee.Save();
            List<Employee> allEmployee= Employee.GetAll();
            return View(allEmployee);
        }
        [HttpGet("/employees/{id}")]
        public ActionResult Show(int id)
        {
            Dictionary<string,object> model= new Dictionary<string,object>();
            Employee employee= Employee.Find(id);
            List<Stylist> employeeStylists= employee.GetStylist();
            model.Add("employee",employee);
            model.Add("employeeStylists",employeeStylists);
            return View(model);
        }
        [HttpGet("/employees/{id}/edit")]
        public ActionResult Edit(int id)
        {
            Employee employee = Employee.Find(id);
            return View(employee);
        }
        [HttpPost("/employees/{id}")]
        public ActionResult Show(string newAuthor, int id)
        {
            Employee employee = Employee.Find(id);
            employee.Edit(newAuthor);
            Dictionary<string,object> model= new Dictionary<string,object>();
            List<Stylist> employeeStylists= employee.GetStylist();
            model.Add("employee",employee);
            model.Add("employeeStylists",employeeStylists);
            return View(model);
        }

    }
}
