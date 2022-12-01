using Microsoft.Data.SqlClient;
using UnlimitedImprovement.Models;
using UnlimitedImprovement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnlimitedImprovement.Repositories
{
    public class LearningTypeRepository : BaseRepository, ILearningType
    {
        private readonly string _baseSqlSelect = @"SELECT 
                                                    Id,
                                                    Type
                                                   FROM [LearningType]";

        public LearningTypeRepository(IConfiguration config) : base(config) { }

        public List<LearningType> GetAllLearningTypes()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = _baseSqlSelect;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var results = new List<LearningType>();
                        while (reader.Read())
                        {
                            var learningType = LoadFromData(reader);

                            results.Add(learningType);
                        }

                        return results;
                    }
                }
            }
        }

        public LearningType? GetLearningTypeById(int id)
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
                        LearningType? result = null;
                        if (reader.Read())
                        {
                            return LoadFromData(reader);
                        }

                        return result;

                    }
                }
            }
        }

        public LearningType CreateLearningType(LearningType learningType)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [LearningType] (Type)
                    OUTPUT INSERTED.ID
                    VALUES (@type);
                ";
                    cmd.Parameters.AddWithValue("@type", learningType.Type);

                    int id = (int)cmd.ExecuteScalar();

                    learningType.Id = id;
                    return learningType;
                }
            }
        }

        public void UpdateLearningType(LearningType learningType)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE [LearningType]
                            SET 
                            Type = @type
                            WHERE ID = @id";


                    cmd.Parameters.AddWithValue("@type", learningType.Type);
                    cmd.Parameters.AddWithValue("@id", learningType.Id);

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void DeleteLearningType(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM LearningType
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private LearningType LoadFromData(SqlDataReader reader)
        {
            return new LearningType
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Type = reader.GetString(reader.GetOrdinal("Type"))
            };
        }


    }
}