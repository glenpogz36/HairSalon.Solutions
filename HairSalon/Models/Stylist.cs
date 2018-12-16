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
            cmd.CommandText = @"INSERT INTO stylists (name) VALUES (@stylist);";

            MySqlParameter stylist = new MySqlParameter();
            stylist.ParameterName = "@stylist";
            stylist.Value = this._stylist;
            cmd.Parameters.Add(stylist);
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
                string stylist = rdr.GetString(1);
                Stylist newStylist = new Stylist(stylist, id);
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
            cmd.CommandText = @"INSERT INTO employee_stylist (employee_id, stylist_id) VALUES (@employeeId, @stylistId);";

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
        public void AddCustomer(int customer)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO customers (customer_clients, stylist_id) VALUES (@copyNumber, @stylistId);";

            MySqlParameter stylist_id = new MySqlParameter();
            stylist_id.ParameterName = "@stylistId";
            stylist_id.Value = _id;
            cmd.Parameters.Add(stylist_id);

            MySqlParameter copy = new MySqlParameter();
            copy.ParameterName = "@copyNumber";
            copy.Value = customer;
            cmd.Parameters.Add(copy);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<int> GetAllAvailable()
        {
            List<int> allBookIds = new List<int> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM customers WHERE customer_clients>0;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int stylist_id = rdr.GetInt32(1);
                allBookIds.Add(stylist_id);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allBookIds;
        }
        public static List<Stylist> GetAvailableStylist()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT stylists.* FROM stylists
                JOIN customers ON (stylists.id = customers.stylist_id)
                WHERE customers.customer_clients > 0;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Stylist> availableStylist = new List<Stylist> { };
            while (rdr.Read())
            {
                int stylistId = rdr.GetInt32(0);
                string bookName = rdr.GetString(1);
                Stylist newStylist = new Stylist(bookName, stylistId);
                availableStylist.Add(newStylist);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return availableStylist;
        }
        public static void Customer(int stylistId,int customers)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE  customers SET customer_clients = @customer_clients   WHERE stylist_id = @searchId;";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = stylistId;
            cmd.Parameters.Add(searchId);
            MySqlParameter customer = new MySqlParameter();
            customer.ParameterName = "@customer_clients";
            customer.Value = customers;
            cmd.Parameters.Add(customer);
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
            string stylist = "";



            while (rdr.Read())
            {
                id = rdr.GetInt32(0);
                stylist = rdr.GetString(1);



            }
            Stylist foundStylist = new Stylist(stylist, id);

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
            cmd.CommandText = @"SELECT employee.* FROM stylists
                JOIN employee_stylist ON (stylists.id = employee_stylist.stylist_id)
                JOIN employee ON (employee_stylist.employee_id = employee.id)
                WHERE stylists.id = @stylistId;";
            MySqlParameter stylistIdParameter = new MySqlParameter();
            stylistIdParameter.ParameterName = "@stylistId";
            stylistIdParameter.Value = _id;
            cmd.Parameters.Add(stylistIdParameter);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            int employeeId = 0;
            string employeeStylist = "";
            while(rdr.Read())
            {
            employeeId = rdr.GetInt32(0);
            employeeStylist = rdr.GetString(1);

            }
            Employee newAuthor = new Employee(employeeStylist, employeeId);

            conn.Close();
            if (conn != null)
            {
            conn.Dispose();
            }
            return newAuthor;
            }
        public List<Client> GetClient()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT clients.* FROM stylists
                JOIN customer_clients ON (stylists.id = customer_clients.stylist_id)
                JOIN clients ON (customer_clients.patron_id = clients.id)
                WHERE stylists.id = @stylistId;";
            MySqlParameter stylistIdParameter = new MySqlParameter();
            stylistIdParameter.ParameterName = "@stylistId";
            stylistIdParameter.Value = _id;
            cmd.Parameters.Add(stylistIdParameter);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Client> clients = new List<Client>{};
            while(rdr.Read())
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
            cmd.CommandText = @"SELECT customer_clients.* FROM stylists
                JOIN customer_clients ON (stylists.id = customer_clients.stylist_id)
                WHERE stylists.id = @stylistId;";
            MySqlParameter stylistIdParameter = new MySqlParameter();
            stylistIdParameter.ParameterName = "@stylistId";
            stylistIdParameter.Value = _id;
            cmd.Parameters.Add(stylistIdParameter);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            DateTime dueDate = new DateTime();
        
            while(rdr.Read())
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

            public void Edit(string newStyle)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE stylists SET stylist = @newStyle WHERE id = @searchId;";
         MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = _id;
        cmd.Parameters.Add(searchId);
         MySqlParameter stylist = new MySqlParameter();
        stylist.ParameterName = "@newStyle";
        stylist.Value = newStyle;
        cmd.Parameters.Add(stylist);
         cmd.ExecuteNonQuery();
        _stylist = newStyle;
         conn.Close();
         if (conn != null)
        {
          conn.Dispose();
        }
    }
}
}