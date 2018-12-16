using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpGet("/employee")]
        public ActionResult Index()
        {
            List<Employee> allEmployee= Employee.GetAll();
            return View(allEmployee);
        }
    }
}
        