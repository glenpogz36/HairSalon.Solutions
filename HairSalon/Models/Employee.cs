using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Collections;
using HairSalon;

namespace HairSalon.Models
{
  public class Employee
  {
    private string _name;
    private int _id;

    public Employee (string Name, int Id = 0)
    {
      _name = Name;
      _id = Id;
    }
    public string GetName()
    {
      return _name;
    }
    public int GetId()
    {
      return _id;
    }
    public static List<Employee> GetAll()
    {
      List<Employee> allemployee = new List<Employee> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * from employee;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int Id = rdr.GetInt32(1);
        string Name = rdr.GetString(0);
        Employee newEmployee = new Employee(Name, Id);
        allemployee.Add(newEmployee);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allemployee;
    }
    public static Employee Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM employee WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int Id = 1;
      string Name = "";
      while (rdr.Read())
      {
        Id = rdr.GetInt32(1);
        Name = rdr.GetString(0);
      }
      Employee newEmployee = new Employee(Name, Id);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newEmployee;
    }

    public override bool Equals(System.Object newEmployee)
  {
    if (!(newEmployee is Employee))
    {
      return false;
    }
    else
    {
      Employee newEmployee = (Employee) newEmployee;
      bool idEquality = (this.GetId() == newEmployee.GetId());
      bool nameEquality = (this.GetName() == newEmployee.GetName());
      return (idEquality && nameEquality);
    }
  }

    public void Save()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO employees (name) VALUES (@EmployeeName);";

        MySqlParameter name = new MySqlParameter();
        name.ParameterName = "@EmployeeName";
        name.Value = this._name;
        cmd.Parameters.Add(name);

        cmd.ExecuteNonQuery();
        _id = (int) cmd.LastInsertedId;
        //
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }


    }
  }
