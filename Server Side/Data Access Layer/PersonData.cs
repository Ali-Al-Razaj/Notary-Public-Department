using System;
using System.Data;
using Microsoft.Data.SqlClient;



namespace Data_Access_Layer
{
    public class PersonDTO
    {
        public PersonDTO
        (
            int PersonID, string NationalNumber, string FirstName, string LastName, string? FatherName, string? MotherName,
            int? BirthPlaceID, DateTime? DateOfBirth, int? RegistrationAuthorityID, int? RegistrationRecordID, bool Gender,
            string? Adress, string? Phone, DateTime? GrantHistory, string? CardNumber
        ) 
        {
            this.PersonID = PersonID;
            this.NationalNumber = NationalNumber;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.FatherName = FatherName;
            this.MotherName = MotherName;

            this.BirthPlaceID = BirthPlaceID;
            if(this.BirthPlaceID != null)
            {
                int id = this.BirthPlaceID.Value;
                this.BirthPlace = GovernorateData.GetGovernorateById(id);
            }
            else
            {
                this.BirthPlace = null;
            }

            this.DateOfBirth = DateOfBirth;

            this.RegistrationAuthorityID = RegistrationAuthorityID;
            if (this.RegistrationAuthorityID != null)
            {
                int id = this.RegistrationAuthorityID.Value;
                this.RegistrationAuthority = RegistrationAuthorityData.GetRegistrationAuthorityById(id);
            }
            else
            {
                this.RegistrationAuthority = null;
            }

            this.RegistrationRecordID = RegistrationRecordID;
            if (this.RegistrationRecordID != null)
            {
                int id = this.RegistrationRecordID.Value;
                this.RegistrationRecord = RegistrationRecordData.GetRegistrationRecordById(id);
            }
            else
            {
                this.RegistrationRecord = null;
            }

            this.Gender = Gender;
            this.Adress = Adress;
            this.Phone = Phone;
            this.GrantHistory = GrantHistory;
            this.CardNumber = CardNumber;
        }



        public int PersonID { get; set; }
        public string NationalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }


        public int? BirthPlaceID { get; set; }
        public GovernorateDTO? BirthPlace { get; set; }


        public DateTime? DateOfBirth { get; set; }


        public int? RegistrationAuthorityID { get; set; }
        public RegistrationAuthorityDTO? RegistrationAuthority { get; set; }


        public int? RegistrationRecordID { get; set; }
        public RegistrationRecordDTO? RegistrationRecord { get; set; }

        
        public bool Gender { get; set; }
        public string? Adress { get; set; }
        public string? Phone { get; set; }
        public DateTime? GrantHistory { get; set; }
        public string? CardNumber { get; set; }

    }


    public class PersonData
    {
        static string _connectionString = Settings.GetConnectionString();

        public static List<PersonDTO> GetAllPeople()
        {
            var PeopleList = new List<PersonDTO>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetAllPeople", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PeopleList.Add(new PersonDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("PersonID")),
                                    reader.GetString(reader.GetOrdinal("NationalNumber")),
                                    reader.GetString(reader.GetOrdinal("FirstName")),
                                    reader.GetString(reader.GetOrdinal("LastName")),
                                    reader.IsDBNull(reader.GetOrdinal("FatherName")) ? null : reader.GetString(reader.GetOrdinal("FatherName")),
                                    reader.IsDBNull(reader.GetOrdinal("MotherName")) ? null : reader.GetString(reader.GetOrdinal("MotherName")),
                                    reader.IsDBNull(reader.GetOrdinal("BirthPlaceID")) ? null : reader.GetInt32(reader.GetOrdinal("BirthPlaceID")),
                                    reader.IsDBNull(reader.GetOrdinal("DateOfBirth")) ? null : reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                    reader.IsDBNull(reader.GetOrdinal("RegistrationAuthorityID")) ? null : reader.GetInt32(reader.GetOrdinal("RegistrationAuthorityID")),
                                    reader.IsDBNull(reader.GetOrdinal("RegistrationRecordID")) ? null : reader.GetInt32(reader.GetOrdinal("RegistrationRecordID")),
                                    reader.GetBoolean(reader.GetOrdinal("Gender")),
                                    reader.IsDBNull(reader.GetOrdinal("Adress")) ? null : reader.GetString(reader.GetOrdinal("Adress")),
                                    reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                                    reader.IsDBNull(reader.GetOrdinal("GrantHistory")) ? null : reader.GetDateTime(reader.GetOrdinal("GrantHistory")),
                                    reader.IsDBNull(reader.GetOrdinal("CardNumber")) ? null : reader.GetString(reader.GetOrdinal("CardNumber"))
                                ));
                            }
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                // TODO: Log exception here or handle as needed
                throw;  // rethrowing for now
            }
            catch(Exception ex)
            {
                // TODO: Log exception here or handle as needed
                throw;  // rethrowing for now
            }
            return PeopleList;
        }

        public static PersonDTO GetPersontById(int PersonId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_GetPersonByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonId);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PersonDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetString(reader.GetOrdinal("NationalNumber")),
                                reader.GetString(reader.GetOrdinal("FirstName")),
                                reader.GetString(reader.GetOrdinal("LastName")),
                                reader.IsDBNull(reader.GetOrdinal("FatherName")) ? null : reader.GetString(reader.GetOrdinal("FatherName")),
                                reader.IsDBNull(reader.GetOrdinal("MotherName")) ? null : reader.GetString(reader.GetOrdinal("MotherName")),
                                reader.IsDBNull(reader.GetOrdinal("BirthPlaceID")) ? null : reader.GetInt32(reader.GetOrdinal("BirthPlaceID")),
                                reader.IsDBNull(reader.GetOrdinal("DateOfBirth")) ? null : reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                reader.IsDBNull(reader.GetOrdinal("RegistrationAuthorityID")) ? null : reader.GetInt32(reader.GetOrdinal("RegistrationAuthorityID")),
                                reader.IsDBNull(reader.GetOrdinal("RegistrationRecordID")) ? null : reader.GetInt32(reader.GetOrdinal("RegistrationRecordID")),
                                reader.GetBoolean(reader.GetOrdinal("Gender")),
                                reader.IsDBNull(reader.GetOrdinal("Adress")) ? null : reader.GetString(reader.GetOrdinal("Adress")),
                                reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                                reader.IsDBNull(reader.GetOrdinal("GrantHistory")) ? null : reader.GetDateTime(reader.GetOrdinal("GrantHistory")),
                                reader.IsDBNull(reader.GetOrdinal("CardNumber")) ? null : reader.GetString(reader.GetOrdinal("CardNumber"))
                            );
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                // TODO: Log exception here or handle as needed
                throw;  // rethrowing for now
            }
            catch(Exception ex)
            {
                // TODO: Log exception here or handle as needed
                throw;  // rethrowing for now
            }
        }

        public static PersonDTO GetPersontByNationalNumber(string NationalNumber)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_GetPersonByNationalNumber", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NationalNumber", NationalNumber);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PersonDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetString(reader.GetOrdinal("NationalNumber")),
                                reader.GetString(reader.GetOrdinal("FirstName")),
                                reader.GetString(reader.GetOrdinal("LastName")),
                                reader.IsDBNull(reader.GetOrdinal("FatherName")) ? null : reader.GetString(reader.GetOrdinal("FatherName")),
                                reader.IsDBNull(reader.GetOrdinal("MotherName")) ? null : reader.GetString(reader.GetOrdinal("MotherName")),
                                reader.IsDBNull(reader.GetOrdinal("BirthPlaceID")) ? null : reader.GetInt32(reader.GetOrdinal("BirthPlaceID")),
                                reader.IsDBNull(reader.GetOrdinal("DateOfBirth")) ? null : reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                reader.IsDBNull(reader.GetOrdinal("RegistrationAuthorityID")) ? null : reader.GetInt32(reader.GetOrdinal("RegistrationAuthorityID")),
                                reader.IsDBNull(reader.GetOrdinal("RegistrationRecordID")) ? null : reader.GetInt32(reader.GetOrdinal("RegistrationRecordID")),
                                reader.GetBoolean(reader.GetOrdinal("Gender")),
                                reader.IsDBNull(reader.GetOrdinal("Adress")) ? null : reader.GetString(reader.GetOrdinal("Adress")),
                                reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                                reader.IsDBNull(reader.GetOrdinal("GrantHistory")) ? null : reader.GetDateTime(reader.GetOrdinal("GrantHistory")),
                                reader.IsDBNull(reader.GetOrdinal("CardNumber")) ? null : reader.GetString(reader.GetOrdinal("CardNumber"))
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

        public static int AddPerson(PersonDTO personDTO)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_AddNewPerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@NationalNumber", personDTO.NationalNumber);
                    command.Parameters.AddWithValue("@FirstName", personDTO.FirstName);
                    command.Parameters.AddWithValue("@LastName", personDTO.LastName);
                    command.Parameters.AddWithValue("@FatherName", personDTO.FatherName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@MotherName", personDTO.MotherName ?? (object)DBNull.Value);

                    command.Parameters.AddWithValue("@BirthPlaceID", personDTO.BirthPlaceID.HasValue ? (object)personDTO.BirthPlaceID.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@DateOfBirth", personDTO.DateOfBirth.HasValue ? (object)personDTO.DateOfBirth.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@RegistrationAuthorityID", personDTO.RegistrationAuthorityID.HasValue ? (object)personDTO.RegistrationAuthorityID.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@RegistrationRecordID", personDTO.RegistrationRecordID.HasValue ? (object)personDTO.RegistrationRecordID.Value : DBNull.Value);

                    command.Parameters.AddWithValue("@Gender", personDTO.Gender);
                    command.Parameters.AddWithValue("@Adress", personDTO.Adress ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Phone", personDTO.Phone ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@GrantHistory", personDTO.GrantHistory.HasValue ? (object)personDTO.GrantHistory.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@CardNumber", personDTO.CardNumber ?? (object)DBNull.Value);

                    var outputIdParam = new SqlParameter("@NewPersonID", SqlDbType.Int)
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

       
        public static bool UpdatePerson(PersonDTO personDTO)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_UpdatePerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PersonID", personDTO.PersonID);
                    command.Parameters.AddWithValue("@NationalNumber", personDTO.NationalNumber);
                    command.Parameters.AddWithValue("@FirstName", personDTO.FirstName);
                    command.Parameters.AddWithValue("@LastName", personDTO.LastName);
                    command.Parameters.AddWithValue("@FatherName", personDTO.FatherName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@MotherName", personDTO.MotherName ?? (object)DBNull.Value);

                    command.Parameters.AddWithValue("@BirthPlaceID", personDTO.BirthPlaceID.HasValue ? (object)personDTO.BirthPlaceID.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@DateOfBirth", personDTO.DateOfBirth.HasValue ? (object)personDTO.DateOfBirth.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@RegistrationAuthorityID", personDTO.RegistrationAuthorityID.HasValue ? (object)personDTO.RegistrationAuthorityID.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@RegistrationRecordID", personDTO.RegistrationRecordID.HasValue ? (object)personDTO.RegistrationRecordID.Value : DBNull.Value);

                    command.Parameters.AddWithValue("@Gender", personDTO.Gender);
                    command.Parameters.AddWithValue("@Adress", personDTO.Adress ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Phone", personDTO.Phone ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@GrantHistory", personDTO.GrantHistory.HasValue ? (object)personDTO.GrantHistory.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@CardNumber", personDTO.CardNumber ?? (object)DBNull.Value);

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

        public static bool DeletePerson(int personId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_DeletePerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Input parameter
                    command.Parameters.AddWithValue("@PersonID", personId);

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


        public static bool CheckPersonExists(int personId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SP_CheckPersonExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PersonID", personId);

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
