using Microsoft.Data.SqlClient;
using UnlimitedImprovement.Models;
using UnlimitedImprovement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnlimitedImprovement.Repositories
{
    public class DayOfTheWeekRepository : BaseRepository, IDayOfTheWeek
    {
        private readonly string _baseSqlSelect = @"SELECT Id,
                                                          Day
                                                   FROM [DayOfTheWeek]";

        public DayOfTheWeekRepository(IConfiguration config) : base(config) { }

        public List<DaysOfTheWeek> GetAllDayOfTheWeek()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = _baseSqlSelect;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var results = new List<DaysOfTheWeek>();
                        while (reader.Read())
                        {
                            var dayOfTheWeek = LoadFromData(reader);

                            results.Add(dayOfTheWeek);
                        }

                        return results;
                    }
                }
            }
        }

        public DaysOfTheWeek? GetDayOfTheWeekById(int id)
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
                        DaysOfTheWeek? result = null;
                        if (reader.Read())
                        {
                            return LoadFromData(reader);
                        }

                        return result;

                    }
                }
            }
        }

        public DaysOfTheWeek CreateDayOfTheWeek(DaysOfTheWeek dayOfTheWeek)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [DayOfTheWeek] (Day)
                    OUTPUT INSERTED.ID
                    VALUES (@day);
                ";
                    cmd.Parameters.AddWithValue("@day", dayOfTheWeek.Day);

                    int id = (int)cmd.ExecuteScalar();

                    dayOfTheWeek.Id = id;
                    return dayOfTheWeek;
                }
            }
        }

        public void UpdateDayOfTheWeek(DaysOfTheWeek dayOfTheWeek)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE [DayOfTheWeek]
                            SET 
                                Day = @day
                            WHERE ID = @id";

                    cmd.Parameters.AddWithValue("@day", dayOfTheWeek.Day);
                    cmd.Parameters.AddWithValue("@id", dayOfTheWeek.Id);


                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void DeleteDayOfTheWeek(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM DayOfTheWeek
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private DaysOfTheWeek LoadFromData(SqlDataReader reader)
        {
            return new DaysOfTheWeek
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Day = reader.GetString(reader.GetOrdinal("Day"))
            };
        }


    }
}
