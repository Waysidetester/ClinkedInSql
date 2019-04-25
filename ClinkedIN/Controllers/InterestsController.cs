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
        

        public AddInterest AddInterest(AddInterest newInterest)
        {
            using (var connection = new SqlConnection(ConnectionString)) //using block is like a try catch but makes sure we always end with connection.Close
            {
                connection.Open();
                var insertInterestsCommand = connection.CreateCommand();
                insertInterestsCommand.CommandText = $@"Insert into interests (Name)
                                              Output inserted. *
                                              Values (@Name)";

                //using parameters here takes care of sql injection by sanitizing data
                //"username" must match the variable above, the 2nd paramter can be anything it just gets passed through
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
    }
}