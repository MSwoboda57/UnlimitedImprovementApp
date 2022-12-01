using Microsoft.Data.SqlClient;
using UnlimitedImprovement.Models;
using UnlimitedImprovement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnlimitedImprovement.Repositories
{
    public class ExerciseRepository : BaseRepository, IExercise
    {
        private readonly string _baseSqlSelect = @"SELECT Id,
                                                    FirebaseId,
                                                    ExerciseName,
                                                    Notes
                                                   FROM [Exercise]";

        public ExerciseRepository(IConfiguration config) : base(config) { }

        public List<Exercise> GetAllExercises()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = _baseSqlSelect;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var results = new List<Exercise>();
                        while (reader.Read())
                        {
                            var exercise = LoadFromData(reader);

                            results.Add(exercise);
                        }

                        return results;
                    }
                }
            }
        }

        public Exercise? GetExerciseById(int id)
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
                        Exercise? result = null;
                        if (reader.Read())
                        {
                            return LoadFromData(reader);
                        }

                        return result;

                    }
                }
            }
        }

        public Exercise CreateExercise(Exercise exercise)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [Exercise] (FirebaseId, ExerciseName, Notes)
                    OUTPUT INSERTED.ID
                    VALUES (@FirebaseId, @ExerciseName, @Notes);
                ";
                    cmd.Parameters.AddWithValue("@FirebaseId", exercise.FirebaseId);
                    cmd.Parameters.AddWithValue("@ExerciseName", exercise.ExerciseName);
                    cmd.Parameters.AddWithValue("@Notes", exercise.Notes);
                   
                    int id = (int)cmd.ExecuteScalar();

                    exercise.Id = id;
                    return exercise;
                }
            }
        }

        public void UpdateExercise(Exercise exercise)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                UPDATE [Exercise]
                                SET 
                                FirebaseId = @firebaseId, 
                                ExerciseName = @exerciseName,
                                Notes = @notes
                            WHERE ID = @id";

                    cmd.Parameters.AddWithValue("@firebaseId", exercise.FirebaseId);
                    cmd.Parameters.AddWithValue("@exerciseName", exercise.ExerciseName);
                    cmd.Parameters.AddWithValue("@notes", exercise.Notes);
                    cmd.Parameters.AddWithValue("@id", exercise.Id);


                    cmd.ExecuteNonQuery();
                }
            }

        }
        public void DeleteExercise(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Exercise
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private Exercise LoadFromData(SqlDataReader reader)
        {
            return new Exercise
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                FirebaseId = reader.GetString(reader.GetOrdinal("FirebaseId")),
                ExerciseName = reader.GetString(reader.GetOrdinal("ExerciseName")),
                Notes = reader.GetString(reader.GetOrdinal("Notes"))
            };
        }


    }
}
