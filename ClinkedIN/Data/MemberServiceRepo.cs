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

        public List<MemberService> AddMemberServices(int membId, List<int> serviceIds)
        {
            foreach (var service in serviceIds)
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    List<MemberService> memberServices = new List<MemberService>();


                    connection.Open();

                    var insertMembService = connection.CreateCommand();

                    insertMembService.CommandText = @"INSERT Into UserServices (ServiceId, UserId)
                                                      OUTPUT Inserted.*
                                                      VALUES (@servId, @userId)";
                    insertMembService.Parameters.AddWithValue("servId", service);
                    insertMembService.Parameters.AddWithValue("userId", membId);

                    var reader = insertMembService.ExecuteReader();



                    //while (reader.Read())
                    //{
                    //    memberServices.Add(new MemberService()
                    //    {
                    //        Id = (int)reader["Id"],
                    //        ServiceId = (int)reader["ServiceId"],
                    //        UserId = (int)reader["UserId"]
                    //    });
                    //}


                    return memberServices;
                }
            }

            throw new Exception("buckets of tears");
        }
    }
}
