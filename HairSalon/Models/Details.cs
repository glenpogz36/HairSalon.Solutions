// public static List<Detail> GetDetails(int Id)
// {
//   List<Detail> allDetail = new List<Detail> { };
//   MySqlConnection conn = DB.Connection();
//   conn.Open();
//   var cmd = conn.CreateCommand() as MySqlCommand;
//   cmd.CommandText = @"SELECT * FROM details WHERE _id = @thisId;";
//   MySqlParameter thisId = new MySqlParameter();
//   thisId.ParameterName = "@thisId";
//   thisId.Value = Id;
//   cmd.Parameters.Add(thisId);
//   var rdr = cmd.ExecuteReader() as MySqlDataReader;
//   while (rdr.Read())
//   {
//     int Id = rdr.GetInt32(0);
//     string Age = rdr.GetString(1);
//       string Details = rdr.GetString(2);
//         string DOB = rdr.GetString(3);
//         string Contact = rdr.GetInt32(4)
//     Details newDetails = new Details(Id, Age, Details, DOB, Contact);
//     allDetail.Add(newDetails);
//   }
//   conn.Close();
//   if (conn != null)
//   {
//     conn.Dispose();
//   }
//   return allDetail;
//
// }
