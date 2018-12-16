using System;
using System.Collections.Generic;
using System.Collections;
using HairSalon;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Client
    {
        private int _id;
        private string _name;

        public Client(string name, int id = 0)
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

        public override bool Equals(System.Object otherClients)
        {
            if (!(otherClients is Client))
            {
                return false;
            }
            else
            {
                Client newClient = (Client)otherClients;
                bool idEquality = (this.GetId() == newClient.GetId());
                bool nameEquality = (this.GetName() == newClient.GetName());
                return (idEquality && nameEquality);
            }
        }
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO clients (name) VALUES (@name);";

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
        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Client newClient = new Client(name, id);
                allClients.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClients;
        }
        public static Client Find(int Id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE id = @clientId;";

            MySqlParameter clientId = new MySqlParameter();
            clientId.ParameterName = "@clientId";
            clientId.Value = Id;
            cmd.Parameters.Add(clientId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int id = 0;
            string clientName = "";
            while (rdr.Read())
            {
                id = rdr.GetInt32(0);
                clientName = rdr.GetString(1);
            }
            Client foundClient = new Client(clientName, id);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundClient;
        }
        public void AddClients(int stylistId, DateTime dueDate)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO customer_clients (stylist_id,client_id,due) VALUES (@stylistId,@clientId,@dueDate);";

            MySqlParameter stylist_id = new MySqlParameter();
            stylist_id.ParameterName = "@stylistId";
            stylist_id.Value = stylistId;
            cmd.Parameters.Add(stylist_id);

            MySqlParameter client_id = new MySqlParameter();
            client_id.ParameterName = "@clientId";
            client_id.Value = _id;
            cmd.Parameters.Add(client_id);

            MySqlParameter due_date = new MySqlParameter();
            due_date.ParameterName = "@dueDate";
            due_date.Value = dueDate;
            cmd.Parameters.Add(due_date);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public List<Stylist> GetStylist()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT stylists.* FROM clients
                JOIN customer_clients ON (clients.id = customer_clients.client_id)
                JOIN stylists ON (customer_clients.book_id = stylists.id)
                WHERE clients.id = @clientId;";
            MySqlParameter clientIdParameter = new MySqlParameter();
            clientIdParameter.ParameterName = "@clientId";
            clientIdParameter.Value = _id;
            cmd.Parameters.Add(clientIdParameter);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Stylist> stylists = new List<Stylist>{};
            while(rdr.Read())
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
        public void Edit(string newClient)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE clients SET name = @newClient WHERE id = @searchId;";
         MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = _id;
        cmd.Parameters.Add(searchId);
         MySqlParameter name = new MySqlParameter();
        name.ParameterName = "@newClient";
        name.Value = newClient;
        cmd.Parameters.Add(name);
         cmd.ExecuteNonQuery();
        _name = newClient;
         conn.Close();
         if (conn != null)
        {
          conn.Dispose();
        }
    }
  }
}