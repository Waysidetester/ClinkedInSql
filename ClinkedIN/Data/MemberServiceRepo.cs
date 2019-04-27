using ClinkedIN.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIN.Data
{
    public class MemberServiceRepo
    {
        const string ConnectionString = "Server=localhost;Database=ClinkedIn;Trusted_Connection=True;";

        public MemberService AddMemberServices(int membId, int serviceId)
        {

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var insertMembService = connection.CreateCommand();

                insertMembService.CommandText = @"INSERT Into UserServices (ServiceId, UserId)
                                                      OUTPUT Inserted.*
                                                      VALUES (@servId, @userId)";
                insertMembService.Parameters.AddWithValue("servId", serviceId);
                insertMembService.Parameters.AddWithValue("userId", membId);

                var reader = insertMembService.ExecuteReader();

                if (reader.Read())
                {
                    return new MemberService()
                    {
                        Id = (int)reader["Id"],
                        ServiceId = (int)reader["ServiceId"],
                        UserId = (int)reader["UserId"]
                    };
                }
            }
            throw new Exception("buckets of tears");
        }
    }
}
