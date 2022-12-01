using Microsoft.Data.SqlClient;
using UnlimitedImprovement.Models;
using UnlimitedImprovement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnlimitedImprovement.Repositories
{
    public class ViceRepository : BaseRepository, IVice
    {
        private readonly string _baseSqlSelect = @"SELECT Id,
                                                    FirebaseId,
                                                    ViceName,
                                                    HowToFix,
                                                    Notes,
                                                    Benefits,
                                                    Date
                                                   FROM [Vice]";

        public ViceRepository(IConfiguration config) : base(config) { }

        public List<Vice> GetAllVices()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = _baseSqlSelect;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var results = new List<Vice>();
                        while (reader.Read())
                        {
                            var vice = LoadFromData(reader);

                            results.Add(vice);
                        }

                        return results;
                    }
                }
            }
        }

        public Vice? GetViceById(int id)
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
                        Vice? result = null;
                        if (reader.Read())
                        {
                            return LoadFromData(reader);
                        }

                        return result;

                    }
                }
            }
        }

        public Vice CreateVice(Vice vice)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [Vice] (FirebaseId, ViceName, HowToFix, Notes, Benefits, Date)
                    OUTPUT INSERTED.ID
                    VALUES (@FirebaseId, @ViceName, @HowToFix, @Notes, @Benefits, @Date);
                ";
                    cmd.Parameters.AddWithValue("@FirebaseId", vice.FirebaseId);
                    cmd.Parameters.AddWithValue("@ViceName", vice.ViceName);
                    cmd.Parameters.AddWithValue("@HowToFix", vice.HowToFix);
                    cmd.Parameters.AddWithValue("@Notes", vice.Notes);
                    cmd.Parameters.AddWithValue("@Benefits", vice.Benefits);
                    cmd.Parameters.AddWithValue("@Date", vice.Date);

                    int id = (int)cmd.ExecuteScalar();

                    vice.Id = id;
                    return vice;
                }
            }
        }

        public void UpdateVice(Vice vice)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE [Vice]
                            SET 
                                FirebaseId = @firebaseId, 
                                ViceName = @ViceName,
                                HowToFix = @HowToFix,
                                Notes = @notes,
                                Benefits = @Benefits,
                                Date = @Date
                            WHERE ID = @id";

                    cmd.Parameters.AddWithValue("@firebaseId", vice.FirebaseId);
                    cmd.Parameters.AddWithValue("@viceName", vice.ViceName);
                    cmd.Parameters.AddWithValue("@howToFix", vice.HowToFix);
                    cmd.Parameters.AddWithValue("@notes", vice.Notes);
                    cmd.Parameters.AddWithValue("@benefits", vice.Benefits);
                    cmd.Parameters.AddWithValue("@date", vice.Date);
                    cmd.Parameters.AddWithValue("@id", vice.Id);

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void DeleteVice(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Vice
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private Vice LoadFromData(SqlDataReader reader)
        {
            return new Vice
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                FirebaseId = reader.GetString(reader.GetOrdinal("FirebaseId")),
                ViceName = reader.GetString(reader.GetOrdinal("ViceName")),
                HowToFix = reader.GetString(reader.GetOrdinal("HowToFix")),
                Notes = reader.GetString(reader.GetOrdinal("Notes")),
                Benefits = reader.GetString(reader.GetOrdinal("Benefits")),
                Date = reader.GetDateTime(reader.GetOrdinal("Date"))
            };
        }


    }
}
