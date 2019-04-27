using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Models
{
    public class GetUsers
    {
        const string ConnectionString = "Server = localhost; Database = ClinkedIn; Trusted_Connection = True;";
        
        public List<User> GetAllUsers()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var getUsersCmd = connection.CreateCommand();
                getUsersCmd.CommandText = "Select * From Users";
                var reader = getUsersCmd.ExecuteReader();
                var users = new List<User>();
                while (reader.Read())
                {
                    var name = reader["Name"].ToString();
                    var releaseDate = (DateTime)reader["ReleaseDate"];
                    var age = (int)reader["Age"];
                    var isPrisoner = (bool)reader["IsPrisoner"];
                    var user = new User(name, releaseDate, age, isPrisoner);
                    user.Id = (int)reader["Id"];
                    users.Add(user);
                }
                return users;
                throw new Exception("Ya blew it!");
            }
        }

        public User GetSpecificUser(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var getUsersCmd = connection.CreateCommand();
                getUsersCmd.CommandText = "Select * From Users Where Id = @Id";
                getUsersCmd.Parameters.AddWithValue("Id", id);
                var reader = getUsersCmd.ExecuteReader();
                if (reader.Read())
                {
                    var name = reader["Name"].ToString();
                    var releaseDate = (DateTime)reader["ReleaseDate"];
                    var age = (int)reader["Age"];
                    var isPrisoner = (bool)reader["IsPrisoner"];
                    var user = new User(name, releaseDate, age, isPrisoner);
                    user.Id = (int)reader["Id"];
                    return user;
                }
                throw new Exception("Ya blew it!");
            }
        }

        public List<User> GetUsersAsWarden(bool isAPrisoner)
        {
            if (!isAPrisoner)
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var getUsersCmd = connection.CreateCommand();
                    getUsersCmd.CommandText = "Select * From Users";
                    var reader = getUsersCmd.ExecuteReader();
                    var users = new List<User>();
                    while (reader.Read())
                    {
                        var name = reader["Name"].ToString();
                        var releaseDate = (DateTime)reader["ReleaseDate"];
                        var age = (int)reader["Age"];
                        var isPrisoner = (bool)reader["IsPrisoner"];
                        var user = new User(name, releaseDate, age, isPrisoner);
                        user.Id = (int)reader["Id"];
                        users.Add(user);
                    }
                    return users;
                    throw new Exception("Ya blew it!");
                }
            }
            else
            {
                throw new Exception("You ain't the warden.");
            }
        }
    }
}
