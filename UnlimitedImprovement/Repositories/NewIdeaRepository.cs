using Microsoft.Data.SqlClient;
using UnlimitedImprovement.Models;
using UnlimitedImprovement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnlimitedImprovement.Repositories
{
    public class NewIdeaRepository : BaseRepository, INewIdea
    {
        private readonly string _baseSqlSelect = @"SELECT Id,
                                                    FirebaseId,
                                                    Date,
                                                    NewIdea
                                                   FROM [NewIdeas]";

        public NewIdeaRepository(IConfiguration config) : base(config) { }

        public List<NewIdeas> GetAllNewIdeas()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = _baseSqlSelect;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var results = new List<NewIdeas>();
                        while (reader.Read())
                        {
                            var newIdea = LoadFromData(reader);

                            results.Add(newIdea);
                        }

                        return results;
                    }
                }
            }
        }


        public NewIdeas CreateNewIdea(NewIdeas newIdeas)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [NewIdeas] (FirebaseId, Date, NewIdea)
                    OUTPUT INSERTED.ID
                    VALUES (@FirebaseId, @Date, @NewIdea);
                ";
                    cmd.Parameters.AddWithValue("@FirebaseId", newIdeas.FirebaseId);
                    cmd.Parameters.AddWithValue("@Date", newIdeas.Date);
                    cmd.Parameters.AddWithValue("@NewIdea", newIdeas.NewIdea);

                    int id = (int)cmd.ExecuteScalar();

                    newIdeas.Id = id;
                    return newIdeas;
                }
            }
        }

        public void UpdateNewIdea(NewIdeas newIdeas)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE [NewIdeas]
                            SET 
                                FirebaseId = @firebaseId, 
                                Date = @date,
                                NewIdea = @newIdea
                            WHERE ID = @id";

                    cmd.Parameters.AddWithValue("@firebaseId", newIdeas.FirebaseId);
                    cmd.Parameters.AddWithValue("@date", newIdeas.Date);
                    cmd.Parameters.AddWithValue("@newIdea", newIdeas.NewIdea);
                    cmd.Parameters.AddWithValue("@id", newIdeas.Id);


                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void DeleteNewIdea(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM NewIdeas
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private NewIdeas LoadFromData(SqlDataReader reader)
        {
            return new NewIdeas
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                FirebaseId = reader.GetString(reader.GetOrdinal("FirebaseId")),
                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                NewIdea = reader.GetString(reader.GetOrdinal("NewIdea"))
            };
        }


    }
}
