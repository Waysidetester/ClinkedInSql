﻿using ClinkedIN.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIN.Data
{
    public class ServiceRepository
    {
        const string ConnectionString = "Server=localhost;Database=ClinkedIn;Trusted_Connection=True;";

        public SuccessfulAddedService AddService(string name, string description, decimal price)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var insertService = connection.CreateCommand();
                insertService.CommandText = @"INSERT into Services (name, description, price)
                                              OUTPUT Inserted.*
                                              VALUES (@name, @description, @price)";

                insertService.Parameters.AddWithValue("name", name);
                insertService.Parameters.AddWithValue("description", description);
                insertService.Parameters.AddWithValue("price", price);

                var reader = insertService.ExecuteReader();

                if (reader.Read())
                {
                    var newService = new SuccessfulAddedService()
                    {
                        Id = (int)reader["Id"],
                        Name = reader["name"].ToString(),
                        Description = reader["description"].ToString(),
                        Price = (decimal)reader["price"]
                    };

                    connection.Close();

                    return newService;
                }
            }

            throw new Exception("Service not inserted");
        }

    }
}
