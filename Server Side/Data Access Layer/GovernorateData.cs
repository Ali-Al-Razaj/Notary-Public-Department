using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Data_Access_Layer
{
    public class GovernorateDTO
    {
        public GovernorateDTO(int GovernorateID, string GovernorateName)
        {
            this.GovernorateID = GovernorateID;
            this.GovernorateName = GovernorateName;
        }


        public int GovernorateID { get; set; }
        public string GovernorateName { get; set; }
    }


    public class GovernorateData
    {

        private static string _connectionString = Settings.GetConnectionString();

        public static List<GovernorateDTO> GetAllGovernorates()
        {
            var GovernoratesList = new List<GovernorateDTO>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetAllGovernorates", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GovernoratesList.Add(new GovernorateDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("GovernorateID")),
                                    reader.GetString(reader.GetOrdinal("GovernorateName"))
                                ));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // TODO: Log exception here or handle as needed
                throw;  // rethrowing for now
            }
            catch (Exception ex)
            {
                // TODO: Log exception here or handle as needed
                throw;  // rethrowing for now
            }
            return GovernoratesList;
        }

        public static GovernorateDTO GetGovernorateById(int GovernorateId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_GetGovernorateByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@GovernorateID", GovernorateId);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new GovernorateDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("GovernorateID")),
                                reader.GetString(reader.GetOrdinal("GovernorateName"))
                            );
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // TODO: Log exception here or handle as needed
                throw;  // rethrowing for now
            }
            catch (Exception ex)
            {
                // TODO: Log exception here or handle as needed
                throw;  // rethrowing for now
            }
        }

        public static GovernorateDTO GetGovernorateByName(string GovernorateName)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_GetGovernorateByName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@GovernorateName", GovernorateName);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new GovernorateDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("GovernorateID")),
                                reader.GetString(reader.GetOrdinal("GovernorateName"))
                            );
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // TODO: Log exception here or handle as needed
                throw;  // rethrowing for now
            }
            catch (Exception ex)
            {
                // TODO: Log exception here or handle as needed
                throw;  // rethrowing for now
            }
        }


    }
}
