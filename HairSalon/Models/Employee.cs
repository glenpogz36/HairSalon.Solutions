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
         cmd.CommandText = @"SELECT * from hairsalon;";
         MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
         while (rdr.Read())
         {
             int Id = rdr.GetInt32(0);
             string Name = rdr.GetString(1);
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
      }
    }
