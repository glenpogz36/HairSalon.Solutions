using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class ClientController : Controller
    {
        [HttpGet("/client")]
        public ActionResult Index()
        {
            List<Client> allClients = Client.GetAllClient();
            return View(allClients);
        }
        [HttpGet("/client/new")]
        public ActionResult CreateForm()
        {
            return View();
        }
        [HttpGet("/client/{clientId}")]
        public ActionResult Details(int clientId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Client client = Client.Find(clientId);
            List<Employee> allEmployees = Employee.GetAllEmployee();
            List<Employee> employee = client.GetEmployee();
            model.Add("client", client);
            model.Add("clientEmployee", employee);
            model.Add("allEmployees", allEmployees);
            return View(model);
        }
        [HttpGet("client/{clientId}/update")]
        public ActionResult UpdateForm (int clientId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Client thisClient = Client.Find(clientId);
            model.Add("client", thisClient);
            return View(model);
        }
        [HttpGet("client/delete")]
        public ActionResult DeleteAll()
        {
            Client.DeleteAll();
            return RedirectToAction("Index");
        }
        [HttpGet("client/{clientId}/delete")]
        public ActionResult DeleteOne(int employeeId, int clientId)
        {
            Client thisClient = Client.Find(clientId);
            thisClient.Delete();
            return RedirectToAction("Index");
        }
        [HttpPost("/client/{clientId}/update")]
        public ActionResult UpdateClient(int employeeId, int clientId)
        {
            Client thisClient = Client.Find(clientId);
            thisClient.Edit(Request.Form["new-client-name"]);
            return RedirectToAction("Index", thisClient);
        }
        [HttpPost("/client/{clientId}")]
        public ActionResult AddEmployeeToClient(int clientId)
        {
            Client client = Client.Find(clientId);
            Employee employee = Employee.Find(int.Parse(Request.Form["employee-id"]));
            client.AddEmployee(employee);
            return RedirectToAction("Details", new { id = clientId});
        }
    
    }
}
