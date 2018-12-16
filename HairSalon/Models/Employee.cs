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

        public Employee (string name, int id = 0)
        {
        _name = name;
         _id = id;
        }
        public string GetName()
        {
            return _name;
        }
        public  int GetId()
        {
            return _id;
        }

               public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO employee (name) VALUES (@name);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);
            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
          public static List<Employee> GetAll()
        {
            List<Employee> allEmployees = new List<Employee> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM employee;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
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
        cmd.CommandText = @"SELECT * FROM `employee` WHERE id = @employeeId;";

        MySqlParameter employeeId = new MySqlParameter();
        employeeId.ParameterName = "@employeeId";
        employeeId.Value = Id;
        cmd.Parameters.Add(employeeId);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int id = 0;
        string emplyee = "";
        


        while (rdr.Read())
        {
            id = rdr.GetInt32(0);
            emplyee = rdr.GetString(1);


        }
        Employee foundEmployee = new Employee(emplyee, id);

        conn.Close();
        if(conn != null)
            {
            conn.Dispose();
            }
            return foundEmployee;
        }

        public List<Stylist> GetStylist()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT stylist.* FROM employee
                JOIN employee_stylist ON (employee.id = employee_stylist.employee_id)
                JOIN stylist ON (employee_stylist.stylist_id = stylist.id)
                WHERE employee.id = @employeeId;";
            MySqlParameter employeeIdParameter = new MySqlParameter();
            employeeIdParameter.ParameterName = "@employeeId";
            employeeIdParameter.Value = _id;
            cmd.Parameters.Add(employeeIdParameter);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Stylist> stylist = new List<Stylist>{};
            while(rdr.Read())
            {
            int stylistId = rdr.GetInt32(0);
            string stylistName = rdr.GetString(1);

            Stylist newstylist = new Stylist(stylistName, stylistId);
            stylist.Add(newstylist);
            }
            conn.Close();
            if (conn != null)
            {
            conn.Dispose();
            }
            return stylist;
            }


        public void Edit(string newName)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE employee SET name = @newName WHERE id = @searchId;";
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
