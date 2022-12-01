using Microsoft.Data.SqlClient;
using UnlimitedImprovement.Models;
using UnlimitedImprovement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnlimitedImprovement.Repositories
{
    public class ExerciseDayOfTheWeekRepository : BaseRepository, IExerciseDayOfTheWeek
    {
        private readonly string _baseSqlSelect = @"SELECT Id,
                                                          DayOfTheWeekId,
                                                          ExerciseId
                                                   FROM [ExerciseDayOfTheWeek]";

        public ExerciseDayOfTheWeekRepository(IConfiguration config) : base(config) { }

        public List<ExerciseDayOfTheWeek> GetAllExerciseDayOfTheWeek()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = _baseSqlSelect;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var results = new List<ExerciseDayOfTheWeek>();
                        while (reader.Read())
                        {
                            var exerciseDayOfTheWeek = LoadFromData(reader);

                            results.Add(exerciseDayOfTheWeek);
                        }

                        return results;
                    }
                }
            }
        }

        public ExerciseDayOfTheWeek? GetExerciseDayOfTheWeekById(int id)
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
                        ExerciseDayOfTheWeek? result = null;
                        if (reader.Read())
                        {
                            return LoadFromData(reader);
                        }

                        return result;

                    }
                }
            }
        }

        public ExerciseDayOfTheWeek CreateExerciseDayOfTheWeek(ExerciseDayOfTheWeek exerciseDayOfTheWeek)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [ExerciseDayOfTheWeek] (DayOfTheWeekId, ExerciseId)
                    OUTPUT INSERTED.ID
                    VALUES (@dayOfTheWeekId, @exerciseId);
                ";
                    cmd.Parameters.AddWithValue("@dayOfTheWeekId", exerciseDayOfTheWeek.DayOfTheWeekId);
                    cmd.Parameters.AddWithValue("@exerciseId", exerciseDayOfTheWeek.ExerciseId);


                    int id = (int)cmd.ExecuteScalar();

                    exerciseDayOfTheWeek.Id = id;
                    return exerciseDayOfTheWeek;
                }
            }
        }

        public void UpdateExerciseDayOfTheWeek(ExerciseDayOfTheWeek exerciseDayOfTheWeek)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE [ExerciseDayOfTheWeek]
                            SET 
                                DayOfTheWeekId = @dayOfTheWeekId, 
                                ExerciseId = @exerciseId
                            WHERE ID = @id";

                    cmd.Parameters.AddWithValue("@dayOfTheWeekId", exerciseDayOfTheWeek.DayOfTheWeekId);
                    cmd.Parameters.AddWithValue("@exerciseId", exerciseDayOfTheWeek.ExerciseId);
                    cmd.Parameters.AddWithValue("@id", exerciseDayOfTheWeek.Id);


                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void DeleteExerciseDayOfTheWeek(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM ExerciseDayOfTheWeek
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private ExerciseDayOfTheWeek LoadFromData(SqlDataReader reader)
        {
            return new ExerciseDayOfTheWeek
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                DayOfTheWeekId = reader.GetInt32(reader.GetOrdinal("DayOfTheWeekId")),
                ExerciseId = reader.GetInt32(reader.GetOrdinal("ExerciseId")),
           };
        }


    }
}
