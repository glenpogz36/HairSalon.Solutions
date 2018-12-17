using System;
using System.Collections.Generic;
using System.Collections;
using HairSalon;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Stylist
    {
        private int _id;
        private string _stylist;

        public Stylist(string stylist, int id = 0)
        {
            _stylist = stylist;
            _id = id;
        }
        public string GetStylist()
        {
            return _stylist;
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
            cmd.CommandText = @"INSERT INTO stylists (name) VALUES (@name);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._stylist;
            cmd.Parameters.Add(name);
            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<Stylist> GetAll()
        {
            List<Stylist> allStylist = new List<Stylist> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Stylist newStylist = new Stylist(name, id);
                allStylist.Add(newStylist);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allStylist;
        }
        public void AddEmployee(int employeeId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO employees_stylits (employee_id, stylist_id) VALUES (@employeeId, @stylistId);";

            MySqlParameter stylist_id = new MySqlParameter();
            stylist_id.ParameterName = "@stylistId";
            stylist_id.Value = _id;
            cmd.Parameters.Add(stylist_id);

            MySqlParameter employee_id = new MySqlParameter();
            employee_id.ParameterName = "@employeeId";
            employee_id.Value = employeeId;
            cmd.Parameters.Add(employee_id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public void AddCustomers(int customerNumbers)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO customers (customers_number, stylist_id) VALUES (@customerNumber, @stylistId);";

            MySqlParameter stylist_id = new MySqlParameter();
            stylist_id.ParameterName = "@stylistId";
            stylist_id.Value = _id;
            cmd.Parameters.Add(stylist_id);

            MySqlParameter customer = new MySqlParameter();
            customer.ParameterName = "@customerNumber";
            customer.Value = customerNumbers;
            cmd.Parameters.Add(customer);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<int> GetAllAvailable()
        {
            List<int> allStylistId = new List<int> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM customers WHERE customers_number>0;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int stylist_id = rdr.GetInt32(1);
                allStylistId.Add(stylist_id);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allStylistId;
        }
        public static List<Stylist> GetAvailableStylists()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT stylists.* FROM stylists
                JOIN customers ON (stylists.id = customers.stylist_id)
                WHERE customers.customers_number > 0;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Stylist> availableStylists = new List<Stylist> { };
            while (rdr.Read())
            {
                int stylistId = rdr.GetInt32(0);
                string stylistName = rdr.GetString(1);
                Stylist newStylist = new Stylist(stylistName, stylistId);
                availableStylists.Add(newStylist);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return availableStylists;
        }
        public static void Checkout(int stylistId, int customers)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE  customers SET customers_number = @customers_number   WHERE stylist_id = @searchId;";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = stylistId;
            cmd.Parameters.Add(searchId);
            MySqlParameter customersNumber = new MySqlParameter();
            customersNumber.ParameterName = "@customers_number";
            customersNumber.Value = customers;
            cmd.Parameters.Add(customersNumber);
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static Stylist Find(int Id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists WHERE id = @stylistId;";

            MySqlParameter stylistId = new MySqlParameter();
            stylistId.ParameterName = "@stylistId";
            stylistId.Value = Id;
            cmd.Parameters.Add(stylistId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int id = 0;
            string name = "";



            while (rdr.Read())
            {
                id = rdr.GetInt32(0);
                name = rdr.GetString(1);



            }
            Stylist foundStylist = new Stylist(name, id);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundStylist;
        }
        public static int FindCustomer(int Id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM customers WHERE stylist_id = @stylistId;";

            MySqlParameter stylistId = new MySqlParameter();
            stylistId.ParameterName = "@stylistId";
            stylistId.Value = Id;
            cmd.Parameters.Add(stylistId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int customers = 0;



            while (rdr.Read())
            {
                customers = rdr.GetInt32(2);

            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return customers;
        }

        public Employee GetEmployee()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT employees.* FROM stylists
                JOIN employees_stylists ON (stylists.id = employees_stylists.stylist_id)
                JOIN employees ON (employees_stylists.employee_id = employees.id)
                WHERE stylists.id = @stylistId;";
            MySqlParameter stylistIdParameter = new MySqlParameter();
            stylistIdParameter.ParameterName = "@stylistId";
            stylistIdParameter.Value = _id;
            cmd.Parameters.Add(stylistIdParameter);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            int employeeId = 0;
            string employeeName = "";
            while (rdr.Read())
            {
                employeeId = rdr.GetInt32(0);
                employeeName = rdr.GetString(1);

            }
            Employee newEmployee = new Employee(employeeName, employeeId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newEmployee;
        }
        public List<Client> GetClients()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT clients.* FROM stylists
                JOIN customers_clients ON (stylists.id = customers_clients.stylist_id)
                JOIN clients ON (customers_clients.client_id = clients.id)
                WHERE stylists.id = @stylistId;";
            MySqlParameter stylistIdParameter = new MySqlParameter();
            stylistIdParameter.ParameterName = "@stylistId";
            stylistIdParameter.Value = _id;
            cmd.Parameters.Add(stylistIdParameter);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Client> clients = new List<Client> { };
            while (rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                string clientName = rdr.GetString(1);

                Client newClient = new Client(clientName, clientId);
                clients.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return clients;
        }
        public DateTime GetStylistDueDate()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT customers_clients.* FROM stylists
                JOIN customers_clients ON (stylists.id = customers_clients.stylist_id)
                WHERE stylists.id = @stylistId;";
            MySqlParameter stylistIdParameter = new MySqlParameter();
            stylistIdParameter.ParameterName = "@stylistId";
            stylistIdParameter.Value = _id;
            cmd.Parameters.Add(stylistIdParameter);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            DateTime dueDate = new DateTime();

            while (rdr.Read())
            {
                dueDate = rdr.GetDateTime(3);
            }


            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return dueDate;
        }

        public void Edit(string newStylist)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE stylists SET name = @newStylist WHERE id = @searchId;";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@newStylist";
            name.Value = newStylist;
            cmd.Parameters.Add(name);
            cmd.ExecuteNonQuery();
            _stylist = newStylist;
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

            MySqlCommand cmd = new MySqlCommand("DELETE FROM stylists WHERE id = @stylistId;", conn);
            MySqlParameter stylistIdParameter = new MySqlParameter();
            stylistIdParameter.ParameterName = "@stylistId";
            stylistIdParameter.Value = this.GetId();

            cmd.Parameters.Add(stylistIdParameter);
            cmd.ExecuteNonQuery();

            if (conn != null)
            {
                conn.Close();
        
            }    
        }
        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}