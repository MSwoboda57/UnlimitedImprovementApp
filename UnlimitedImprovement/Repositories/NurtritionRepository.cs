using Microsoft.Data.SqlClient;
using UnlimitedImprovement.Models;
using UnlimitedImprovement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnlimitedImprovement.Repositories
{
    public class NutritionRepository : BaseRepository, INutrition
    {
        private readonly string _baseSqlSelect = @"SELECT Id,
                                                    FirebaseId,
                                                    Notes,
                                                    Breakfast,
                                                    Lunch,
                                                    Dinner,
                                                    Misc
                                                    
                                                   FROM [Nutrition]";

        public NutritionRepository(IConfiguration config) : base(config) { }

        public List<Nutrition> GetAllNutrition()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = _baseSqlSelect;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var results = new List<Nutrition>();
                        while (reader.Read())
                        {
                            var nutrition = LoadFromData(reader);

                            results.Add(nutrition);
                        }

                        return results;
                    }
                }
            }
        }

        public Nutrition? GetNutritionById(int id)
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
                        Nutrition? result = null;
                        if (reader.Read())
                        {
                            return LoadFromData(reader);
                        }

                        return result;

                    }
                }
            }
        }

        public Nutrition CreateNutrition(Nutrition nutrition)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [Nutrition] (FirebaseId, Notes, Breakfast, Lunch, Dinner, Misc)
                    OUTPUT INSERTED.ID
                    VALUES (@FirebaseId, @Notes, @Breakfast, @Lunch, @Dinner, @Misc);
                ";
                    cmd.Parameters.AddWithValue("@FirebaseId", nutrition.FirebaseId);
                    cmd.Parameters.AddWithValue("@Notes", nutrition.Notes);
                    cmd.Parameters.AddWithValue("@Breakfast", nutrition.Breakfast);
                    cmd.Parameters.AddWithValue("@Lunch", nutrition.Lunch);
                    cmd.Parameters.AddWithValue("@Dinner", nutrition.Dinner);
                    cmd.Parameters.AddWithValue("@Misc", nutrition.Misc);


                    int id = (int)cmd.ExecuteScalar();

                    nutrition.Id = id;
                    return nutrition;
                }
            }
        }

        public void UpdateNutrition(Nutrition nutrition)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE [Nutrition]
                            SET 
                                FirebaseId = @firebaseId, 
                                Notes = @notes,
                                Breakfast = @breakfast,
                                Lunch = @lunch,
                                Dinner = @dinner,
                                Misc = @misc
                            WHERE ID = @id";

                    cmd.Parameters.AddWithValue("@firebaseId", nutrition.FirebaseId);
                    cmd.Parameters.AddWithValue("@notes", nutrition.Notes);
                    cmd.Parameters.AddWithValue("@Breakfast", nutrition.Breakfast);
                    cmd.Parameters.AddWithValue("@Lunch", nutrition.Lunch);
                    cmd.Parameters.AddWithValue("@Dinner", nutrition.Dinner);
                    cmd.Parameters.AddWithValue("@Misc", nutrition.Misc);
                    cmd.Parameters.AddWithValue("@id", nutrition.Id);


                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void DeleteNutrition(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Nutrition
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        private Nutrition LoadFromData(SqlDataReader reader)
        {
            return new Nutrition
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                FirebaseId = reader.GetString(reader.GetOrdinal("FirebaseId")),
                Notes = reader.GetString(reader.GetOrdinal("Notes")),
                Breakfast = reader.GetString(reader.GetOrdinal("Breakfast")),
                Lunch = reader.GetString(reader.GetOrdinal("Lunch")),
                Dinner = reader.GetString(reader.GetOrdinal("Dinner")),
                Misc = reader.GetString(reader.GetOrdinal("Misc"))
               
            };
        }


    }
}
