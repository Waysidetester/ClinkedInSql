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

        public List<MatchedUserService> GetMembersByService(int serviceId)
        {
            List<MatchedUserService> matchedUsers = new List<MatchedUserService>();

            using (var connection = new SqlConnection())
            {
                connection.Open();

                var getUsersByService = connection.CreateCommand();
                getUsersByService.CommandText = @"SELECT s.Name [Service], s.Price, u.*
                                                 FROM UserServices as us
                                                 JOIN Users as u on u.id = us.UserId
                                                 WHERE us.ServiceId = @serviceId";
                getUsersByService.Parameters.AddWithValue("serviceId", serviceId);

                var reader = getUsersByService.ExecuteReader();

                while (reader.Read())
                {
                    matchedUsers.Add(new MatchedUserService()
                    {
                        Id = (int)reader["Id"],
                        ServiceName = reader["Service"].ToString(),
                        Price = (double)reader["Price"],
                        UserName = reader["Name"].ToString(),
                        ReleaseDate = (DateTime)reader["ReleaseDate"],
                        Age = (int)reader["Age"],
                        IsPrisoner = (bool)reader["IsPrisoner"]
                    });
                }
            }
            return matchedUsers
        }
    }
}
