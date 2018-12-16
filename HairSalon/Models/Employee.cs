using System;
using System.Collections.Generic;
using System.Collections;
using HairSalon;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Employee
    {
        private int _id;
        private string _name;

        public Employee(string name, int id = 0)
        {
            _name = name;
            _id = id;
        }
        public string GetName()
        {
            return _name;
        }
        public int GetId()
        {
            return _id;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO employees (name) VALUES (@name);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);
            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<Employee> GetAll()
        {
            List<Employee> allEmployees = new List<Employee> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM employees;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Employee newEmployee = new Employee(name, id);
                allEmployees.Add(newEmployee);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allEmployees;
        }
        public static Employee Find(int Id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `employees` WHERE id = @employeeId;";

            MySqlParameter employeeId = new MySqlParameter();
            employeeId.ParameterName = "@employeeId";
            employeeId.Value = Id;
            cmd.Parameters.Add(employeeId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int id = 0;
            string employeeName = "";



            while (rdr.Read())
            {
                id = rdr.GetInt32(0);
                employeeName = rdr.GetString(1);


            }
            Employee foundEmployee = new Employee(employeeName, id);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundEmployee;
        }

        public List<Stylist> GetStylists()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT stylists.* FROM employees
                JOIN employees_stylists ON (employees.id = employees_stylists.employee_id)
                JOIN stylists ON (employees_stylists.stylist_id = stylists.id)
                WHERE employees.id = @employeeId;";
            MySqlParameter employeeIdParameter = new MySqlParameter();
            employeeIdParameter.ParameterName = "@employeeId";
            employeeIdParameter.Value = _id;
            cmd.Parameters.Add(employeeIdParameter);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Stylist> stylists = new List<Stylist> { };
            while (rdr.Read())
            {
                int stylistId = rdr.GetInt32(0);
                string stylistName = rdr.GetString(1);

                Stylist newStylist = new Stylist(stylistName, stylistId);
                stylists.Add(newStylist);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return stylists;
        }


        public void Edit(string newName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE employees SET name = @newName WHERE id = @searchId;";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@newName";
            name.Value = newName;
            cmd.Parameters.Add(name);
            cmd.ExecuteNonQuery();
            _name = newName;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }
    }
}