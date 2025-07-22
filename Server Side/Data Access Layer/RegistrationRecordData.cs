using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Data_Access_Layer
{
    public class RegistrationRecordDTO
    {
        public RegistrationRecordDTO(int RegistrationRecordID, string RegistrationRecordName) 
        {
            this.RegistrationRecordID = RegistrationRecordID;
            this.RegistrationRecordName = RegistrationRecordName;
        }

        public int RegistrationRecordID { get; set; }
        public string RegistrationRecordName { get; set; }
    }


    public class RegistrationRecordData
    {

        static string _connectionString = Settings.GetConnectionString();

        public static int AddRegistrationRecord(RegistrationRecordDTO registrationRecordDTO)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_AddNewRegistrationRecord", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RegistrationRecordName", registrationRecordDTO.RegistrationRecordName);

                    var outputIdParam = new SqlParameter("@NewRegistrationRecordID", SqlDbType.Int)
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

        public static List<RegistrationRecordDTO> GetAllRegistrationRecords()
        {
            var RegistrationRecordsList = new List<RegistrationRecordDTO>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetAllRegistrationRecords", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RegistrationRecordsList.Add(new RegistrationRecordDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("RegistrationRecordID")),
                                    reader.GetString(reader.GetOrdinal("RegistrationRecordName"))
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
            return RegistrationRecordsList;
        }

        public static RegistrationRecordDTO GetRegistrationRecordById(int RegistrationRecordId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_GetRegistrationRecordByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RegistrationRecordID", RegistrationRecordId);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new RegistrationRecordDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("RegistrationRecordID")),
                                reader.GetString(reader.GetOrdinal("RegistrationRecordName"))
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

        public static RegistrationRecordDTO GetRegistrationRecordByName(string RegistrationRecordName)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_GetRegistrationRecordByName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RegistrationRecordName", RegistrationRecordName);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new RegistrationRecordDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("RegistrationRecordID")),
                                reader.GetString(reader.GetOrdinal("RegistrationRecordName"))
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
