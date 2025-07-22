using System;
using System.Data;
using Microsoft.Data.SqlClient;


namespace Data_Access_Layer
{
    public class DocumentPersonRecordDTO
    {
        public int RecordID { get; set; }
        public int DocumentID { get; set; }
        public DocumentDTO Document { get; set; }
        public int PersonID { get; set; }
        public PersonDTO Person { get; set; }

        public bool PersonRole { get; set; }

        public DocumentPersonRecordDTO(int RecordID, int DocumentID, int PersonID, bool PersonRole)
        {
            this.RecordID = RecordID;

            this.DocumentID = DocumentID;
            this.Document = DocumentData.GetDocumentById(DocumentID);

            this.PersonID = PersonID;
            this.Person = PersonData.GetPersontById(PersonID);


            this.PersonRole = PersonRole;
        }
    }


    public class DocumentPersonRecordData
    {
        static string _connectionString = Settings.GetConnectionString();

        public static int AddRecord(DocumentPersonRecordDTO RecordDTO)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_AddNewRecord", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DocumentID", RecordDTO.DocumentID);
                    command.Parameters.AddWithValue("@PersonID", RecordDTO.PersonID);
                    command.Parameters.AddWithValue("@PersonRole", RecordDTO.PersonRole);

                    var outputIdParam = new SqlParameter("@NewRecordID", SqlDbType.Int)
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

        public static List<DocumentPersonRecordDTO> GetAllRecords()
        {
            var RecordsList = new List<DocumentPersonRecordDTO>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetAllRecords", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RecordsList.Add(new DocumentPersonRecordDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("RecordID")),
                                    reader.GetInt32(reader.GetOrdinal("DocumentID")),
                                    reader.GetInt32(reader.GetOrdinal("PersonID")),
                                    reader.GetBoolean(reader.GetOrdinal("PersonRole"))
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
            return RecordsList;
        }

        public static DocumentPersonRecordDTO GetRecordById(int RecordId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_GetRecordByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RecordID", RecordId);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DocumentPersonRecordDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("RecordID")),
                                reader.GetInt32(reader.GetOrdinal("DocumentID")),
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetBoolean(reader.GetOrdinal("PersonRole"))
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


        public static bool DeleteRecord(int Id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_DeleteRecord", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Input parameter
                    command.Parameters.AddWithValue("@RecordID", Id);

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
