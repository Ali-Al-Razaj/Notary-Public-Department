using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Data_Access_Layer
{
    public class DocumentDTO
    {
        public int DocumentID { get; set; }
        public int DocumentTypeID { get; set; }
        public DocumentTypeDTO DocumentType { get; set; }
        public DateTime Date { get; set; }
        public int NotaryPublicID { get; set; }
        public NotaryDTO NotaryPublic { get; set; }

        public DocumentDTO(int DocumentID, int DocumentTypeID, DateTime Date, int NotaryPublicID)
        {
            this.DocumentID = DocumentID;

            this.DocumentTypeID = DocumentTypeID;
            this.DocumentType = DocumentTypeData.GetDocumentTypeById(DocumentTypeID);

            this.Date = Date;

            this.NotaryPublicID = NotaryPublicID;
            this.NotaryPublic = NotaryData.GetNotaryById(NotaryPublicID);
        }
    }



    public class DocumentData
    {
        static string _connectionString = Settings.GetConnectionString();

        public static List<DocumentDTO> GetAllDocuments()
        {
            var DocumentsList = new List<DocumentDTO>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetAllDocuments", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DocumentsList.Add(new DocumentDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("DocumentID")),
                                    reader.GetInt32(reader.GetOrdinal("DocumentTypeID")),
                                    reader.GetDateTime(reader.GetOrdinal("Date")),
                                    reader.GetInt32(reader.GetOrdinal("NotaryPublicID"))
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
            return DocumentsList;
        }

        public static DocumentDTO GetDocumentById(int Id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_GetDocumentByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DocumentID", Id);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DocumentDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("DocumentID")),
                                reader.GetInt32(reader.GetOrdinal("DocumentTypeID")),
                                reader.GetDateTime(reader.GetOrdinal("Date")),
                                reader.GetInt32(reader.GetOrdinal("NotaryPublicID"))
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


        public static int AddDocument(DocumentDTO documentDTO)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_AddNewDocument", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DocumentTypeID", documentDTO.DocumentTypeID);
                    command.Parameters.AddWithValue("@Date", documentDTO.Date);
                    command.Parameters.AddWithValue("@NotaryPublicID", documentDTO.NotaryPublicID);

                    var outputIdParam = new SqlParameter("@NewDocumentID", SqlDbType.Int)
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


        public static bool DeleteDocument(int Id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_DeleteDocument", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Input parameter
                    command.Parameters.AddWithValue("@DocumentID", Id);

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


        public static bool CheckDocumentExists(int Id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_CheckDocumentExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DocumentID", Id);

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
