using Microsoft.Data.SqlClient;
using UnlimitedImprovement.Models;
using UnlimitedImprovement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnlimitedImprovement.Repositories
{
    public class MeditationRepository : BaseRepository, IMeditation
    {
        private readonly string _baseSqlSelect = @"SELECT Id,
                                                    FirebaseId,
                                                    Date,
                                                    SessionNotes
                                                   FROM [Meditation]";

        public MeditationRepository(IConfiguration config) : base(config) { }

        public List<Meditation> GetAllMeditations()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = _baseSqlSelect;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var results = new List<Meditation>();
                        while (reader.Read())
                        {
                            var meditation = LoadFromData(reader);

                            results.Add(meditation);
                        }

                        return results;
                    }
                }
            }
        }


        public Meditation CreateMeditation(Meditation meditation)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [Meditation] (FirebaseId, Date, SessionNotes)
                    OUTPUT INSERTED.ID
                    VALUES (@FirebaseId, @Date, @SessionNotes);
                ";
                    cmd.Parameters.AddWithValue("@FirebaseId", meditation.FirebaseId);
                    cmd.Parameters.AddWithValue("@Date", meditation.Date);
                    cmd.Parameters.AddWithValue("@SessionNotes", meditation.SessionNotes);

                    int id = (int)cmd.ExecuteScalar();

                    meditation.Id = id;
                    return meditation;
                }
            }
        }

        public void UpdateMeditation(Meditation meditation)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE [Meditation]
                            SET 
                                FirebaseId = @firebaseId, 
                                Date = @date,
                                SessionNotes = @Sessionnotes
                            WHERE ID = @id";

                    cmd.Parameters.AddWithValue("@firebaseId", meditation.FirebaseId);
                    cmd.Parameters.AddWithValue("@date", meditation.Date);
                    cmd.Parameters.AddWithValue("@sessionNotes", meditation.SessionNotes);
                    cmd.Parameters.AddWithValue("@id", meditation.Id);


                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void DeleteMeditation(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Meditation
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private Meditation LoadFromData(SqlDataReader reader)
        {
            return new Meditation
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                FirebaseId = reader.GetString(reader.GetOrdinal("FirebaseId")),
                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                SessionNotes = reader.GetString(reader.GetOrdinal("SessionNotes"))
            };
        }


    }
}
