using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class Specialty
    {
        private int _id;
        private string _specialty;

        public Specialty(string newSpecialty, int newId = 0)
        {
            _id = newId;
            _specialty = newSpecialty;
        }
        public string GetSpecialty()
        {
            return _specialty;
        }
        public int GetId()
        {
            return _id;
        }
        public override bool Equals(System.Object otherSpecialty)
        {
            if (!(otherSpecialty is Specialty))
            {
                return false;
            }
            else
            {
                Specialty newSpecialty = (Specialty)otherSpecialty;
                bool idEquality = this.GetId() == newSpecialty.GetId();
                bool nameEquality = this.GetSpecialty() == newSpecialty.GetSpecialty();
                return (idEquality && nameEquality);
            }
        }
        public override int GetHashCode()
        {
            string allHash = this.GetSpecialty();
            return allHash.GetHashCode();
        }
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO specialties (name) VALUES (@newSpecialty);";

            MySqlParameter newSpecialty = new MySqlParameter();
            newSpecialty.ParameterName = "@newSpecialty";
            newSpecialty.Value = this._specialty;
            cmd.Parameters.Add(newSpecialty);

            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<Specialty> GetAll()
        {
            List<Specialty> allSpecialty = new List<Specialty>() { };
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties;";

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int specialtyId = rdr.GetInt32(0);
                string specialtyName = rdr.GetString(1);
                Specialty newSpecialty = new Specialty(specialtyName, specialtyId);
                allSpecialty.Add(newSpecialty);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allSpecialty;
        }
        public static Specialty Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties WHERE id =(@searchId);";

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
            Specialty newSpecialty = new Specialty(specialtyName, specialtyId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newSpecialty;
        }
        public void AddEmployee(Employee newEmployee)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO employees_specialties (specialty_id, employee_id) VALUES (@SpecialtyId, @EmployeeId);";

            MySqlParameter specialties_id = new MySqlParameter();
            specialties_id.ParameterName = "@SpecialtyId";
            specialties_id.Value = _id;
            cmd.Parameters.Add(specialties_id);

            MySqlParameter Employees_id = new MySqlParameter();
            Employees_id.ParameterName = "@EmployeeId";
            Employees_id.Value = newEmployee.GetEmployeeId();
            cmd.Parameters.Add(Employees_id);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }
        public List<Employee> GetEmployee()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT employees.* FROM specialties 
            JOIN employees_specialties ON (specialties.id = employees_specialties.specialty_id)
            JOIN employees ON (employees_specialties.employee_id = employees.id)
            WHERE specialties.id=@specialtyIdParameter;";

            MySqlParameter specialtyIdParameter = new MySqlParameter();
            specialtyIdParameter.ParameterName = "@specialtyIdParameter";
            specialtyIdParameter.Value = this._id;
            cmd.Parameters.Add(specialtyIdParameter);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Employee> Employees = new List<Employee> {};

            while (rdr.Read())
            {
                int EmployeeId = rdr.GetInt32(0);
                string EmployeeName = rdr.GetString(1);
                Employee newEmployee = new Employee(EmployeeName, EmployeeId);
                Employees.Add(newEmployee);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return Employees;
        }
        public void Delete()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM specialties WHERE id = @specialtyId; DELETE FROM employees_specialties WHERE specialty_id= @specialtyId;";

            MySqlParameter specialtyId = new MySqlParameter();
            specialtyId.ParameterName = "@specialtyId";
            specialtyId.Value = _id;
            cmd.Parameters.Add(specialtyId);

            cmd.ExecuteNonQuery();
            if (conn != null)
            {
                conn.Close();
            }
        }
        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM specialties;";

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}