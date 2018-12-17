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
            List<Employee> allEmployees = Employee.GetAll();
            return View(allEmployees);
        }
        [HttpGet("/employees/new")]
        public ActionResult New()
        {
            return View();
        }
        [HttpPost("/employees")]
        public ActionResult Index(string employeename)
        {
            Employee employee = new Employee(employeename);
            employee.Save();
            List<Employee> allEmployees = Employee.GetAll();
            return View(allEmployees);
        }
        [HttpGet("/employees/{id}")]
        public ActionResult Show(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Employee employee = Employee.Find(id);
            List<Stylist> employeeStylists = employee.GetStylists();
            model.Add("employee", employee);
            model.Add("employeeStylists", employeeStylists);
            return View(model);
        }
        [HttpGet("/employees/{id}/edit")]
        public ActionResult Edit(int id)
        {
            Employee employee = Employee.Find(id);
            return View(employee);
        }
        [HttpPost("/employees/{id}")]
        public ActionResult Show(string newEmployee, int id)
        {
            Employee employee = Employee.Find(id);
            employee.Edit(newEmployee);
            Dictionary<string, object> model = new Dictionary<string, object>();
            List<Stylist> employeeStylists = employee.GetStylists();
            model.Add("employee", employee);
            model.Add("employeeStylists", employeeStylists);
            return View(model);
        }
        [HttpGet("/employees/{employeeId}/delete")]
        public ActionResult Delete(int employeeId)
        {
            Employee thisEmployee = Employee.Find(employeeId);
            thisEmployee.Delete();
            return RedirectToAction("Index");
        }
    }
}
