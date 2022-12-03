using Microsoft.Data.SqlClient;
using UnlimitedImprovement.Models;
using UnlimitedImprovement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnlimitedImprovement.Repositories
{
    public class NutritionDayOfTheWeekRepository : BaseRepository, INutritionDayOfTheWeek
    {
        private readonly string _baseSqlSelect = @"SELECT Id,
                                                          NutritionId,
                                                          DayOfTheWeekId
                                                   FROM [NutritionDayOfTheWeek]";

        public NutritionDayOfTheWeekRepository(IConfiguration config) : base(config) { }

        public List<NutritionDayOfTheWeek> GetAllNutritionDayOfTheWeek()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = _baseSqlSelect;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var results = new List<NutritionDayOfTheWeek>();
                        while (reader.Read())
                        {
                            var nutritionDayOfTheWeek = LoadFromData(reader);

                            results.Add(nutritionDayOfTheWeek);
                        }

                        return results;
                    }
                }
            }
        }

        public NutritionDayOfTheWeek? GetNutritionDayOfTheWeekById(int id)
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
                        NutritionDayOfTheWeek? result = null;
                        if (reader.Read())
                        {
                            return LoadFromData(reader);
                        }

                        return result;

                    }
                }
            }
        }

        public NutritionDayOfTheWeek CreateNutritionDayOfTheWeek(NutritionDayOfTheWeek nutritionDayOfTheWeek)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [NutritionDayOfTheWeek] (NutritionId, DayOfTheWeekId)
                    OUTPUT INSERTED.ID
                    VALUES (@nutritionId, @dayOfTheWeekId);
                ";
                    cmd.Parameters.AddWithValue("@dayOfTheWeekId", nutritionDayOfTheWeek.NutritionId);
                    cmd.Parameters.AddWithValue("@nutritionId", nutritionDayOfTheWeek.DayOfTheWeekId);


                    int id = (int)cmd.ExecuteScalar();

                    nutritionDayOfTheWeek.Id = id;
                    return nutritionDayOfTheWeek;
                }
            }
        }

        public void UpdateNutritionDayOfTheWeek(NutritionDayOfTheWeek nutritionDayOfTheWeek)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE [NutritionDayOfTheWeek]
                            SET 
                                NutritionId = @nutritionId
                                DayOfTheWeekId = @dayOfTheWeekId, 

                            WHERE ID = @id";
                    cmd.Parameters.AddWithValue("@nutritionId", nutritionDayOfTheWeek.NutritionId);
                    cmd.Parameters.AddWithValue("@dayOfTheWeekId", nutritionDayOfTheWeek.DayOfTheWeekId);
                    cmd.Parameters.AddWithValue("@id", nutritionDayOfTheWeek.Id);


                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void DeleteNutritionDayOfTheWeek(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM NutritionDayOfTheWeek
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private NutritionDayOfTheWeek LoadFromData(SqlDataReader reader)
        {
            return new NutritionDayOfTheWeek
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                NutritionId = reader.GetInt32(reader.GetOrdinal("NutritionId")),
                DayOfTheWeekId = reader.GetInt32(reader.GetOrdinal("DayOfTheWeekId")),
            };
        }


    }
}
