using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class Employee
    {
        private string _employeeName;
        private int _employeeId;
        public Employee(string EmployeeName, int EmployeeId = 0)
        {
            _employeeName = EmployeeName;
            _employeeId = EmployeeId;
        }
        public string GetEmployee()
        {
            return _employeeName;
        }
        public int GetEmployeeId()
        {
            return _employeeId;
        }
        public override bool Equals(System.Object otherEmployee)
        {
            if (!(otherEmployee is Employee))
            {
                return false;
            }
            else
            {
                Employee newEmployee = (Employee) otherEmployee;
                bool idEquality = this.GetEmployeeId() == newEmployee.GetEmployeeId();
                bool nameEquality = this.GetEmployee() == newEmployee.GetEmployee();
                return (idEquality && nameEquality);
            }
        }
        public override int GetHashCode()
        {
            string allHash = this.GetEmployee();
            return allHash.GetHashCode();
        }
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO employees (name) VALUES (@name);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this.GetEmployee();
            cmd.Parameters.Add(name);

            cmd.ExecuteNonQuery();
            _employeeId = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<Employee> GetAllEmployee()
        {
            List<Employee> allEmployee = new List<Employee> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM employees;";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int EmployeeId = rdr.GetInt32(0);
                string EmployeeName = rdr.GetString(1);
                Employee newEmployee = new Employee(EmployeeName, EmployeeId);
                allEmployee.Add(newEmployee);
            }

            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            return allEmployee;
        }
        public static Employee Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM employees where id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int EmployeeId = 0;
            string EmployeeName = "";
            while(rdr.Read())
            {
                EmployeeId = rdr.GetInt32(0);
                EmployeeName = rdr.GetString(1);
            }
            Employee newEmployee = new Employee (EmployeeName, EmployeeId);
            
            conn.Close();
            if (conn!=null)
            {
                conn.Dispose();
            }
            return newEmployee;
        }
        public List<Client> GetClient()
        {
            List<Client> EmployeeClient = new List<Client> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT clients.* FROM employees
            JOIN employees_clients ON (employees.id = employees_clients.employee_id)
            JOIN clients ON (employees_clients.client_id = clients.id)
            WHERE employees.id = @employeeId;";

            MySqlParameter employeeIdParameter = new MySqlParameter();
            employeeIdParameter.ParameterName = "@employeeId";
            employeeIdParameter.Value = _employeeId;
            cmd.Parameters.Add(employeeIdParameter);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int clientId = 0;
            string clientName = "";

            while(rdr.Read())
            {
                clientId = rdr.GetInt32(0);
                clientName = rdr.GetString(1);
                Client newClient = new Client(clientName, clientId);
                EmployeeClient.Add(newClient);
            }
          conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return EmployeeClient;
        }
   
        public void AddClient(Client newClient)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO employees_clients(employee_id, client_id) VALUES (@employeeId, @clientId);";

            MySqlParameter employeeIdParameter = new MySqlParameter();
            employeeIdParameter.ParameterName = "@employeeId";
            employeeIdParameter.Value = _employeeId;
            cmd.Parameters.Add(employeeIdParameter);

            MySqlParameter clientIdParameter = new MySqlParameter();
            clientIdParameter.ParameterName = "clientId";
            clientIdParameter.Value = newClient.GetClientId();
            cmd.Parameters.Add(clientIdParameter);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public List<Specialty> GetSpecialty()
        {
            List<Specialty> specialties = new List<Specialty> {};

            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT specialties.* FROM employees
            JOIN employees_specialties ON (employees.id = employees_specialties.employee_id)
            JOIN specialties ON (employees_specialties.specialty_id = specialties.id)
            WHERE employees.id = @employeesIdParameter;";

            MySqlParameter employeesIdParameter = new MySqlParameter();
            employeesIdParameter.ParameterName = "@employeesIdParameter";
            employeesIdParameter.Value = this._employeeId;
            cmd.Parameters.Add(employeesIdParameter);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int specialtiesId = rdr.GetInt32(0);
                string specialtiesName = rdr.GetString(1);
                Specialty newSpecialty = new Specialty(specialtiesName, specialtiesId);
                specialties.Add(newSpecialty);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            
            return specialties;
        }
        public void AddSpecialty (Specialty newSpecialty)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO employees_specialties (specialty_id, employee_id) VALUES (@SpecialtyId, @EmployeeId);";

            MySqlParameter specialties_id = new MySqlParameter();
            specialties_id.ParameterName = "@SpecialtyId";
            specialties_id.Value = newSpecialty.GetId();
            cmd.Parameters.Add(specialties_id);

            MySqlParameter Employees_id = new MySqlParameter();
            Employees_id.ParameterName = "@EmployeeId";
            Employees_id.Value = _employeeId;
            cmd.Parameters.Add(Employees_id);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }
        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM employees;";

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM employees WHERE id = @employeeId; DELETE FROM employees_specialties WHERE employee_id= @employeeId; DELETE FROM employees_clients WHERE employee_id = @employeeId;";

            MySqlParameter employeeId = new MySqlParameter();
            employeeId.ParameterName = "@employeeId";
            employeeId.Value = _employeeId;
            cmd.Parameters.Add(employeeId);

            cmd.ExecuteNonQuery();
            if(conn != null)
            {
                conn.Close();
            }
        }
        public void Edit(string newName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"UPDATE employees SET name = @newName WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _employeeId;
            cmd.Parameters.Add(searchId);

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@newName";
            name.Value = newName;
            cmd.Parameters.Add(name);

            cmd.ExecuteNonQuery();
            _employeeName = newName;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
