using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Data_Access_Layer
{
    public class UserDTO
    {
        public int UserID { get; set; }

        public int PersonID { get; set; }
        public PersonDTO Person { get; set; }

        public string Username { get; set; }
        //public string Password { get; set; }

        public UserDTO(int UserID, int PersonID, string Username)//, string Password)
        {
            this.UserID = UserID;

            this.PersonID = PersonID;
            this.Person = PersonData.GetPersontById(PersonID);

            this.Username = Username;
            //this.Password = Password;
        }
    }


    public class UserData
    {
        static string _connectionString = Settings.GetConnectionString();

        public static List<UserDTO> GetAllUser()
        {
            var UsersList = new List<UserDTO>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetAllUsers", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UsersList.Add(new UserDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("UserID")),
                                    reader.GetInt32(reader.GetOrdinal("PersonID")),
                                    reader.GetString(reader.GetOrdinal("Username"))
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
            return UsersList;
        }

        public static UserDTO GetUserById(int UserId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_GetUserByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserId);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("UserID")),
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetString(reader.GetOrdinal("Username"))
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

        public static string GetPasswordById(int UserId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_GetUserByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserId);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetString(reader.GetOrdinal("Password"));
                        }
                        else
                        {
                            return "";
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

        public static int AddUser(UserDTO userDTO, string password)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_AddNewUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PersonID", userDTO.PersonID);
                    command.Parameters.AddWithValue("@Username", userDTO.Username);
                    command.Parameters.AddWithValue("@Password", password);

                    var outputIdParam = new SqlParameter("@NewUserID", SqlDbType.Int)
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


        public static bool UpdateUser(UserDTO userDTO, string password)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_UpdateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", userDTO.UserID);
                    command.Parameters.AddWithValue("@PersonID", userDTO.PersonID);
                    command.Parameters.AddWithValue("@Username", userDTO.Username);
                    command.Parameters.AddWithValue("@Password", password);

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


        public static bool DeleteUser(int userId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_DeleteUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Input parameter
                    command.Parameters.AddWithValue("@UserID", userId);

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


        public static bool CheckUserExists(int userId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_CheckUserExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", userId);

                    var returnParameter = new SqlParameter();
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnParameter);

                    connection.Open();
                    command.ExecuteNonQuery();

                    int result = (int)returnParameter.Value;

                    return result == 1;
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

        public static bool CheckLoginCredentials(string username, string password)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_Login", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    var returnParameter = new SqlParameter();
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnParameter);

                    connection.Open();
                    command.ExecuteNonQuery();

                    int result = (int)returnParameter.Value;

                    return result == 1;
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
