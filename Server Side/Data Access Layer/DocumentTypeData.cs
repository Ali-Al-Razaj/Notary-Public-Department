using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Data_Access_Layer
{
    public class DocumentTypeDTO
    {
        public DocumentTypeDTO(int DocumentTypeID, string Title, string Body)
        { 
            this.DocumentTypeID = DocumentTypeID;
            this.Title = Title;
            this.Body = Body;
        }


        public int DocumentTypeID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }



    public class DocumentTypeData
    {
        static string _connectionString = Settings.GetConnectionString();

        public static List<DocumentTypeDTO> GetAllDocumentTypes()
        {
            var TypesList = new List<DocumentTypeDTO>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetAllDocumentTypes", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TypesList.Add(new DocumentTypeDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("DocumentTypeID")),
                                    reader.GetString(reader.GetOrdinal("Title")),
                                    reader.GetString(reader.GetOrdinal("Body"))
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
            return TypesList;
        }

        public static DocumentTypeDTO GetDocumentTypeById(int DocumentTypeId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_GetDocumentTypeByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DocumentTypeID", DocumentTypeId);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DocumentTypeDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("DocumentTypeID")),
                                reader.GetString(reader.GetOrdinal("Title")),
                                reader.GetString(reader.GetOrdinal("Body"))
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

        public static DocumentTypeDTO GetDocumentTypeByTitle(string DocumentTypeTitle)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_GetDocumentTypeByTitle", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Title", DocumentTypeTitle);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DocumentTypeDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("DocumentTypeID")),
                                reader.GetString(reader.GetOrdinal("Title")),
                                reader.GetString(reader.GetOrdinal("Body"))
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

        public static int AddDocumentType(DocumentTypeDTO documentTypeDTO)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_AddNewDocumentType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Title", documentTypeDTO.Title);
                    command.Parameters.AddWithValue("@Body", documentTypeDTO.Body);

                    var outputIdParam = new SqlParameter("@NewDocumentTypeID", SqlDbType.Int)
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

        public static bool UpdateDocumentType(DocumentTypeDTO documentTypeDTO)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_UpdateDocumentType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DocumentTypeID", documentTypeDTO.DocumentTypeID);
                    command.Parameters.AddWithValue("@Title", documentTypeDTO.Title);
                    command.Parameters.AddWithValue("@Body", documentTypeDTO.Body);

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

        public static bool DeleteDocumentType(int Id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_DeleteDocumentType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Input parameter
                    command.Parameters.AddWithValue("@DocumentTypeID", Id);

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

        public static bool CheckDocumentTypeExists(int Id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_CheckDocumentTypeExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DocumentTypeID", Id);

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
