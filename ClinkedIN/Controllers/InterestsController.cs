using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ClinkedIN.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinkedIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        const string ConnectionString = "Server = localhost; Database = ClinkedIn; Trusted_Connection = True;";
        
        [HttpPost]
        public AddInterest AddInterest(AddInterest newInterest)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var insertInterestsCommand = connection.CreateCommand();
                insertInterestsCommand.CommandText = $@"Insert into interests (Name)
                                              Output inserted. *
                                              Values (@Name)";

                
                insertInterestsCommand.Parameters.AddWithValue("Name", newInterest.Name);

                var reader = insertInterestsCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedName = reader["Name"].ToString();
                    var insertedId = (int)reader["Id"];

                    var createdInterest = new CreateInterest() { Id = insertedId, Name = insertedName };

                    connection.Close();
                    return newInterest;
                }
            }

            throw new Exception("No interest found");
        }

        [HttpGet]
        public List<Interests> GetAll()
        {
            var users = new List<Interests>();
            //connection string
            var connection = new SqlConnection("Server = localhost; Database = SwordAndFather; Trusted_Connection = True;");
            connection.Open();

            //descriptive command for what it should be executing against the server in SQL
            var getAllUsersCommand = connection.CreateCommand();
            getAllUsersCommand.CommandText = "SELECT username, password, id FROM users";

            //execute reader if i want to know results
            //execute nonQuery if I don't care about seeing results(only rows affected)
            //execute scalar returns top left most column and row
            var reader = getAllUsersCommand.ExecuteReader();

            //asks for more data, returns true or false for if there is more data
            //initial reader returns no data, we must use READ method 
            //can use a while loop to get all info out of reader
            while (reader.Read())
            {
                var id = (int)reader["id"]; //use direct casting to int
                var username = reader["username"].ToString(); //cast to appropriate type
                var password = reader["password"].ToString();
                var user = new User(username, password) { Id = id };

                users.Add(user); //loop continues to build list until it runs out of data
            }

            connection.Close();

            return users;

        }
    }
}