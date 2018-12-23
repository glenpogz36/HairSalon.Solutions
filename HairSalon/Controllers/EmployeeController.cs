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
            List<Employee> allEmployee = Employee.GetAllEmployee();
            return View(allEmployee);
        }
        [HttpGet("/employee/new")]
        public ActionResult CreateForm()
        {
            return View();
        }
        [HttpGet("/employee/{id}")]
        public ActionResult Details(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Employee selectedEmployee = Employee.Find(id);
            List<Client> employeeClient = selectedEmployee.GetClient();
            List<Specialty> employeeSpecialty = selectedEmployee.GetSpecialty();
            List<Client> allClients = Client.GetAllClient();
            List<Specialty> allSpecialties = Specialty.GetAll();
            model.Add("employee", selectedEmployee);
            model.Add("employeeSpecialties", employeeSpecialty);
            model.Add("client", employeeClient);
            model.Add("allClient", allClients);
            model.Add("allSpecialties", allSpecialties);
            return View(model);
        }
        [HttpGet("/employee/{employeeId}/update")]
        public ActionResult UpdateForm (int employeeId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Employee thisEmployee = Employee.Find(employeeId);
            model.Add("employee", thisEmployee);
            return View("UpdateForm", model);
        }
        [HttpGet("/employee/delete")]
        public ActionResult DeleteAll()
        {
            Employee.DeleteAll();
            return RedirectToAction("Index");
        }
        [HttpGet("/employee/{employeeId}/delete")]
        public ActionResult DeleteOne(int employeeId)
        {
            Employee thisEmployee = Employee.Find(employeeId);
            thisEmployee.Delete();
            return RedirectToAction("Index");
        }
        [HttpPost("/employee/{employeeId}/update")]
        public ActionResult UpdateEmployee (int employeeId)
        {
            Employee thisEmployee = Employee.Find(employeeId);
            thisEmployee.Edit(Request.Form["new-employee-name"]);
            return RedirectToAction("Index");
        }
        [HttpPost("/employee/{employeeId}/specialty/new")]
        public ActionResult AddSpecialty (int employeeId)
        {
            Employee employee = Employee.Find(employeeId);
            Specialty specialty = Specialty.Find(int.Parse(Request.Form["specialty-id"]));
            employee.AddSpecialty(specialty);
            return RedirectToAction("Details", new {id = employeeId});
        }
        [HttpPost("/employee/{employeeId}/client/new")]
        public ActionResult AddEmployee (int employeeId)
        {
            Employee employee = Employee.Find(employeeId);
            Client client = Client.Find(int.Parse(Request.Form["client-id"]));
            employee.AddClient(client);
            return RedirectToAction("Details", new {id = employeeId});
        }
        [HttpPost("/employee")]
        public ActionResult CreateEmployee()
        {
            Employee NewEmployee = new Employee(Request.Form["new-employee"]);
            NewEmployee.Save();
            List<Employee> allEmployee = Employee.GetAllEmployee();
            return RedirectToAction("Index");
        }
        [HttpPost("/client")]
        public ActionResult CreateClient(int employeeId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Employee foundEmployee = Employee.Find(employeeId);
            Client newClient = new Client(Request.Form["new-client"]);
            newClient.Save();
            foundEmployee.AddClient(newClient);
            List<Client> employeeClient = foundEmployee.GetClient();
            model.Add("client", employeeClient);
            model.Add("employee", foundEmployee);
            return RedirectToAction("Index", new{id = employeeId});
        }
        [HttpPost("/specialty")]
        public ActionResult CreateSpecialty(int employeeId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Employee foundEmployee = Employee.Find(employeeId);
            Specialty newSpecialty = new Specialty(Request.Form["new-specialties"]);
            newSpecialty.Save();
            foundEmployee.AddSpecialty(newSpecialty);
            List<Specialty> employeeSpecialty = foundEmployee.GetSpecialty();
            model.Add("specialty", employeeSpecialty);
            model.Add("employee", foundEmployee);
            return RedirectToAction("Index", new{id = employeeId});
        }
    }
}
