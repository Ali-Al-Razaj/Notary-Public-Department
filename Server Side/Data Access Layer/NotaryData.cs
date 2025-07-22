using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Data_Access_Layer
{
    public class NotaryDTO
    {
        public int NotaryPublicID { get; set; }
        public int Number { get; set; }
        public int GovernorateID { get; set; }
        public GovernorateDTO Governorate { get; set; }
        public string Name { get; set; }

        public NotaryDTO(int NotaryPublicID, int Number, int GovernorateID, string Name)
        {
            this.NotaryPublicID = NotaryPublicID;
            this.Number = Number;
            this.GovernorateID = GovernorateID;
            this.Governorate = GovernorateData.GetGovernorateById(GovernorateID);
            this.Name = Name;
        }
    }



    public class NotaryData
    {
        static string _connectionString = Settings.GetConnectionString();

        public static List<NotaryDTO> GetAllNotaries()
        {
            var NotariesList = new List<NotaryDTO>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetAllNotaries", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                NotariesList.Add(new NotaryDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("NotaryPublicID")),
                                    reader.GetInt32(reader.GetOrdinal("Number")),
                                    reader.GetInt32(reader.GetOrdinal("GovernorateID")),
                                    reader.GetString(reader.GetOrdinal("Name"))
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
            return NotariesList;
        }

        public static NotaryDTO GetNotaryById(int Id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_GetNotaryByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NotaryID", Id);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new NotaryDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("NotaryPublicID")),
                                reader.GetInt32(reader.GetOrdinal("Number")),
                                reader.GetInt32(reader.GetOrdinal("GovernorateID")),
                                reader.GetString(reader.GetOrdinal("Name"))
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

        public static NotaryDTO GetNotaryByName(string Name)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_GetNotaryByName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NotaryName", Name);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new NotaryDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("NotaryPublicID")),
                                reader.GetInt32(reader.GetOrdinal("Number")),
                                reader.GetInt32(reader.GetOrdinal("GovernorateID")),
                                reader.GetString(reader.GetOrdinal("Name"))
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

        public static int AddNotary(NotaryDTO notaryDTO)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_AddNewNotary", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Number", notaryDTO.Number);
                    command.Parameters.AddWithValue("@GovernorateID", notaryDTO.GovernorateID);
                    command.Parameters.AddWithValue("@Name", notaryDTO.Name);

                    var outputIdParam = new SqlParameter("@NewNotaryID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);

                    connection.Open();
                    command.ExecuteNonQuery();

                    return (int)outputIdParam.Value;
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
                throw; // rethrowing for now
            }
        }


        public static bool UpdateNotary(NotaryDTO notaryDTO)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_UpdateNotary", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@NotaryPublicID", notaryDTO.NotaryPublicID);
                    command.Parameters.AddWithValue("@Number", notaryDTO.Number);
                    command.Parameters.AddWithValue("@GovernorateID", notaryDTO.GovernorateID);
                    command.Parameters.AddWithValue("@Name", notaryDTO.Name);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    // Return true if update affected at least one row
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                // TODO: Log error as needed
                throw; // Rethrow for now
            }
            catch (Exception ex)
            {
                // TODO: Log error as needed
                throw; // Rethrow for now
            }
        }


        public static bool DeleteNotary(int Id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_DeleteNotary", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Input parameter
                    command.Parameters.AddWithValue("@NotaryPublicID", Id);

                    // Return value parameter
                    var returnParameter = new SqlParameter();
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnParameter);

                    connection.Open();
                    command.ExecuteNonQuery();

                    // Get the returned number of rows affected
                    int rowsAffected = (int)returnParameter.Value;

                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                // TODO: Log exception here or handle as needed
                throw;  // rethrowing for now
            }
            catch (Exception ex)
            {
                // TODO: Log error as needed
                throw; // Rethrow for now
            }
        }

    }
}
