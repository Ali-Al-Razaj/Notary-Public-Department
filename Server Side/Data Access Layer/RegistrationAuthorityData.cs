using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Data_Access_Layer
{
    public class RegistrationAuthorityDTO
    {
        public RegistrationAuthorityDTO(int RegistrationAuthorityID, string RegistrationAuthorityName)
        {
            this.RegistrationAuthorityID = RegistrationAuthorityID;
            this.RegistrationAuthorityName = RegistrationAuthorityName;
        }

        public int RegistrationAuthorityID { get; set; }
        public string RegistrationAuthorityName { get; set; }
    }



    public class RegistrationAuthorityData
    {
        static string _connectionString = Settings.GetConnectionString();

        public static int AddRegistrationAuthority(RegistrationAuthorityDTO registrationAuthorityDTO)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_AddNewRegistrationAuthority", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RegistrationAuthorityName", registrationAuthorityDTO.RegistrationAuthorityName);

                    var outputIdParam = new SqlParameter("@NewRegistrationAuthorityID", SqlDbType.Int)
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

        public static List<RegistrationAuthorityDTO> GetAllRegistrationAuthorities()
        {
            var AuthoritiesList = new List<RegistrationAuthorityDTO>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetAllRegistrationAuthorities", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AuthoritiesList.Add(new RegistrationAuthorityDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("RegistrationAuthorityID")),
                                    reader.GetString(reader.GetOrdinal("RegistrationAuthorityName"))
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
            return AuthoritiesList;
        }

        public static RegistrationAuthorityDTO GetRegistrationAuthorityById(int RegistrationAuthorityId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_GetRegistrationAuthorityByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RegistrationAuthorityID", RegistrationAuthorityId);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new RegistrationAuthorityDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("RegistrationAuthorityID")),
                                reader.GetString(reader.GetOrdinal("RegistrationAuthorityName"))
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

        public static RegistrationAuthorityDTO GetRegistrationAuthorityByName(string RegistrationAuthorityName)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_GetRegistrationAuthorityByName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RegistrationAuthorityName", RegistrationAuthorityName);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new RegistrationAuthorityDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("RegistrationAuthorityID")),
                                reader.GetString(reader.GetOrdinal("RegistrationAuthorityName"))
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
