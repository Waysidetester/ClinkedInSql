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
        

        public Interest AddInterest(string Name)
        {
            using (var connection = new SqlConnection(ConnectionString)) //using block is like a try catch but makes sure we always end with connection.Close
            {
                connection.Open();
                var insertInterestsCommand = connection.CreateCommand();
                insertInterestsCommand.CommandText = $@"Insert into interests (name)
                                              Output inserted. *
                                              Values(@name)";

                //using parameters here takes care of sql injection by sanitizing data
                //"username" must match the variable above, the 2nd paramter can be anything it just gets passed through
                insertInterestsCommand.Parameters.AddWithValue("name", Name);

                var reader = insertInterestsCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedPassword = reader["name"].ToString();
                    var insertedId = (int)reader["Id"];

                    var newInterest = new Interest() { Id = insertedId };

                    return newInterest;
                }
            }

            throw new Exception("No interest found");
        }
    }
}