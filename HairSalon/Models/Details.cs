using System;
using System.Collections.Generic;
using HairSalon;
using MySql.Data.MySqlClient;
using System.Collections;

namespace HairSalon.Models
{
  public class Details
  {
    private int _id;
    private string _name;
    private string _detail;

    public Details(string name, int Id = 0)
    {
      _id = Id;
      _name = name;

    }
    public string GetDetailsName()
    {
      return _name;
    }

    public string GetDetail()
    {
      return _detail;
    }
    public int GetDetailsId()
    {
      return _id;
    }

    public override bool Equals(System.Object otherDetails)
    {
      if (!(otherDetails is Details))
      {
        return false;
      }
      else
      {
        Details newDetails = (Details) otherDetails;
        bool idEquality = (this.GetDetailsId() == newDetails.GetDetailsId());
        bool nameEquality = (this.GetDetailsName() == newDetails.GetDetailsName());

        return (idEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetDetailsName().GetHashCode();
    }

    public static List<Details> GetAll()
    {
      List<Details> allSpecialties = new List<Details> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM details;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int Id = rdr.GetInt32(0);
        string Name = rdr.GetString(1);


        Details newDetails = new Details(Name, Id);
        allSpecialties.Add(newDetails);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allSpecialties;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO details (specialty_name) VALUES (@DetailsName);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@DetailsName";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public static Details Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `details` WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int specialtyId = 0;
      string specialtyName = "";


      while (rdr.Read())
      {
        specialtyId = rdr.GetInt32(0);
        specialtyName = rdr.GetString(1);


      }
      Details foundDetails = new Details(specialtyName, specialtyId);

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return foundDetails;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"TRUNCATE TABLE details";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void AddEmployee(Employee newEmployee)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO employee_details (employee_id, specialty_id) VALUES (@EmployeeId, @DetailsId);";

      MySqlParameter employee_id = new MySqlParameter();
      employee_id.ParameterName = "@EmployeeId";
      employee_id.Value = newEmployee.GetId();
      cmd.Parameters.Add(employee_id);

      MySqlParameter specialty_id = new MySqlParameter();
      specialty_id.ParameterName = "@DetailsId";
      specialty_id.Value = _id;
      cmd.Parameters.Add(specialty_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Employee> GetEmployees()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT employee.* FROM details
        JOIN employee_details ON (details.id = employee_details.specialty_id)
        JOIN employees ON (employee_details.id = employee.id)
        WHERE details.id = @DetailsId;";

        MySqlParameter specialtyIdParameter = new MySqlParameter();
        specialtyIdParameter.ParameterName = "@DetailsId";
        specialtyIdParameter.Value = _id;
        cmd.Parameters.Add(specialtyIdParameter);

        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        List<Employee> employees = new List<Employee>{};

        while(rdr.Read())
        {
          int employeeId = rdr.GetInt32(0);
          string employeeName = rdr.GetString(1);

          Employee newEmployee = new Employee(employeeName, employeeId);
          employees.Add(newEmployee);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return employees;
      }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = new MySqlCommand("DELETE FROM details WHERE id = @DetailsId; DELETE FROM employee_details WHERE specialty_id = @DetailsId;", conn);
      MySqlParameter specialtyIdParameter = new MySqlParameter();
      specialtyIdParameter.ParameterName = "@DetailsId";
      specialtyIdParameter.Value = this.GetDetailsId();

      cmd.Parameters.Add(specialtyIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Edit(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE details SET name = @newName WHERE id = @searchId;";

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
