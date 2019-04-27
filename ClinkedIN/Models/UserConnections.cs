using ClinkedIN.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Models
{
    public class UserConnections
    {
        const string ConnectionString = "Server = localhost; Database = ClinkedIn; Trusted_Connection = True;";
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Age { get; set; }
        public bool IsPrisoner { get; set; }

        public User AddNewUser()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
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
                    var addedIsPrisoner = (bool)reader["IsPrisoner"];

                    var user = new User(addedName, addedReleaseDate, addedAge, addedIsPrisoner);
                    user.Id = (Int32)reader["id"];
                    return user;
                }
                throw new Exception("Ya blew it!");
            }
        }

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
        public User DeleteUser(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var deleteUserCmd = connection.CreateCommand();
                deleteUserCmd.CommandText = "Delete From Users Output Deleted.* Where Id = @Id";
                deleteUserCmd.Parameters.AddWithValue("Id", id);
                var reader = deleteUserCmd.ExecuteReader();
                if (reader.Read())
                {
                    var name = reader["Name"].ToString();
                    var releaseDate = (DateTime)reader["ReleaseDate"];
                    var age = (int)reader["Age"];
                    var isPrisoner = (bool)reader["IsPrisoner"];
                    var user = new User(name, releaseDate, age, isPrisoner);
                    return user;
                }
                throw new Exception("Ya blew it!");
            }
        }

        public User GetUserWithDetails(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var getUserCmd = connection.CreateCommand();
                getUserCmd.CommandText = @"Select Users.*, Services.Name as ServiceName, Services.Description, Services.Price, ServiceId,
                                           Interests.Name as InterestName
                                           From Users
                                           Join UserServices on UserServices.UserId = Users.Id
                                           Join Services on Services.Id = UserServices.ServiceId
                                           Join UserInterests on UserInterests.UserId = Users.Id
                                           Join Interests on Interests.Id = UserInterests.InterestId
                                           Where Users.Id = @Id";
                getUserCmd.Parameters.AddWithValue("Id", id);
                var reader = getUserCmd.ExecuteReader();
                var user = new User() { Services = new List<DbService>(), Interests = new List<string>()};
                while (reader.Read())
                {
                    user.Name = reader["Name"].ToString();
                    user.ReleaseDate = (DateTime)reader["ReleaseDate"];
                    user.Age = (int)reader["Age"];
                    user.IsPrisoner = (bool)reader["IsPrisoner"];

                    var serviceName = reader["ServiceName"].ToString();
                    var description = reader["Description"].ToString();
                    var price = (decimal)reader["Price"];
                    var serviceId = (int)reader["ServiceId"];
                    var service = new DbService() { Name = serviceName, Description = description, Price = price, Id = serviceId};
                    user.Services.Add(service);                
                }
                return user;
            }
            throw new Exception("Ya blew it!");
        }
    }
}
