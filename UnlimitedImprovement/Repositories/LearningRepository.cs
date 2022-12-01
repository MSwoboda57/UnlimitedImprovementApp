using Microsoft.Data.SqlClient;
using UnlimitedImprovement.Models;
using UnlimitedImprovement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnlimitedImprovement.Repositories
{
    public class LearningRepository : BaseRepository, ILearning
    {
        private readonly string _baseSqlSelect = @"SELECT Id,
                                                    FirebaseId,
                                                    LearningTypeId,
                                                    Notes,
                                                    Title,
                                                    Link
                                                   FROM [Learning]";

        public LearningRepository(IConfiguration config) : base(config) { }
        
        public List<Learning> GetAllLearning()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = _baseSqlSelect;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var results = new List<Learning>();
                        while (reader.Read())
                        {
                            var learning = LoadFromData(reader);

                            results.Add(learning);
                        }

                        return results;
                    }
                }
            }
        }

        public Learning? GetLearningById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"{_baseSqlSelect} WHERE Id" +
                        $" = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Learning? result = null;
                        if (reader.Read())
                        {
                            return LoadFromData(reader);
                        }

                        return result;

                    }
                }
            }
        }

        public Learning CreateLearning(Learning learning)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [Learning] (LearningTypeId, FirebaseId, Title, Link, Notes)
                    OUTPUT INSERTED.ID
                    VALUES (@LearningTypeId, @FirebaseId, @Title, @Link, @Notes);
                ";
                    cmd.Parameters.AddWithValue("@LearningTypeId", learning.LearningTypeId);
                    cmd.Parameters.AddWithValue("@FirebaseId", learning.FirebaseId);
                    cmd.Parameters.AddWithValue("@Title", learning.Title);
                    cmd.Parameters.AddWithValue("@Link", learning.Link);
                    cmd.Parameters.AddWithValue("@Notes", learning.Notes);

                    int id = (int)cmd.ExecuteScalar();

                    learning.Id = id;
                    return learning;
                }
            }
        }

        public void UpdateLearning(Learning learning)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE [Learning]
                            SET 
                                LearningTypeId = @learningTypeId,
                                FirebaseId = @firebaseId, 
                                Title = @title,
                                Link = @Link,
                                Notes = @notes
                            WHERE ID = @id";

                    
                    cmd.Parameters.AddWithValue("@LearningTypeId", learning.LearningTypeId);
                    cmd.Parameters.AddWithValue("@firebaseId", learning.FirebaseId);
                    cmd.Parameters.AddWithValue("@title", learning.Title);
                    cmd.Parameters.AddWithValue("@link", learning.Link);
                    cmd.Parameters.AddWithValue("@notes", learning.Notes);
                    cmd.Parameters.AddWithValue("@id", learning.Id);


                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void DeleteLearning(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Learning
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private Learning LoadFromData(SqlDataReader reader)
        {
            return new Learning
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                FirebaseId = reader.GetString(reader.GetOrdinal("FirebaseId")),
                LearningTypeId = reader.GetInt32(reader.GetOrdinal("LearningTypeId")),
                Title = reader.GetString(reader.GetOrdinal("Title")),
                Link = reader.GetString(reader.GetOrdinal("Link")),
                Notes = reader.GetString(reader.GetOrdinal("Notes"))
            };
        }


    }
}