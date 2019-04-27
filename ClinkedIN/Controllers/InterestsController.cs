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
        public Interest AddInterest(Interest newInterest)
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
        public List<Interest> GetAll()
        {
            var interestsList = new List<Interest>();
            var connection = new SqlConnection("Server = localhost; Database = ClinkedIn; Trusted_Connection = True;");
            connection.Open();

            var getAllInterestsCommand = connection.CreateCommand();
            getAllInterestsCommand.CommandText = "SELECT * FROM interests";

            var reader = getAllInterestsCommand.ExecuteReader();

            //asks for more data, returns true or false for if there is more data
            //initial reader returns no data, we must use READ method 
            //can use a while loop to get all info out of reader
            while (reader.Read())
            {
                var name = reader["name"].ToString();
                var interest = new Interest(name) { Name = name };

                interestsList.Add(interest); //loop continues to build list until it runs out of data
            }

            connection.Close();

            return interestsList;

        }
    }
}