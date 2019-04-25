using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Models
{
    public class AddUser
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Age { get; set; }
        public int IsPrisoner { get; set; }

        const string ConnectionString = "Server = localhost; Database = ClinkedIn; Trusted_Connection = True;";

        public User AddNewUser()
        {
            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();
                var addUserCmd = connection.CreateCommand();
                addUserCmd.CommandText = @"Insert into Users(Name, ReleaseDate, Age, IsPrisoner)
                                           Output inserted.* 
                                           Values(@Name, @ReleaseDate, @Age, @IsPrisoner)";
                addUserCmd.Parameters.AddWithValue("Name", Name);
                addUserCmd.Parameters.AddWithValue("ReleaseDate", ReleaseDate);
                addUserCmd.Parameters.AddWithValue("Age", Age);
                addUserCmd.Parameters.AddWithValue("IsPrisoner", IsPrisoner);
                var reader = addUserCmd.ExecuteReader();
                if (reader.Read())
                {
                    var addedName = reader["Name"].ToString();
                    var addedReleaseDate = (DateTime)reader["ReleaseDate"];
                    var addedAge = (Int32)reader["Age"];
                    var addedIsPrisoner = (bool)reader["IsPrisoner"] ? 1 : 0;

                    var user = new User(addedName, addedReleaseDate, addedAge, addedIsPrisoner);
                    user.Id = (Int32)reader["id"];
                    return user;
                }
                throw new Exception("Ya blew it!");
            }
        }
    }
}
