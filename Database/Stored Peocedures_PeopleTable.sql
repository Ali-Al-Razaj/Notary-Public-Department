USE NotaryPublicDepartment;

--------------------------------------------------------------------------------------------- SP_AddNewPerson

CREATE PROCEDURE SP_AddNewPerson
    @NationalNumber NVARCHAR(50),
    @FirstName NVARCHAR(100),
	@LastName NVARCHAR(100),
	@FatherName NVARCHAR(100),
	@MotherName NVARCHAR(100),
	@BirthPlaceID INT,
    @DateOfBirth DATE,
	@RegistrationAuthorityID INT,
	@RegistrationRecordID INT,
	@Gender BIT,
	@Adress NVARCHAR(255),
	@Phone NVARCHAR(20),
	@GrantHistory DATE,
	@CardNumber NVARCHAR(50),
    @NewPersonID INT OUTPUT
AS
BEGIN
    INSERT INTO People(NationalNumber, FirstName, LastName, FatherName, MotherName, BirthPlaceID, DateOfBirth, RegistrationAuthorityID, RegistrationRecordID, Gender, Adress, Phone, GrantHistory, CardNumber)
    VALUES (@NationalNumber, @FirstName, @LastName, @FatherName, @MotherName, @BirthPlaceID, @DateOfBirth, @RegistrationAuthorityID, @RegistrationRecordID, @Gender, @Adress, @Phone, @GrantHistory, @CardNumber);


    SET @NewPersonID = SCOPE_IDENTITY();
END


--------------------------------------------------------------------------------------------- SP_GetAllPeople

CREATE PROCEDURE SP_GetAllPeople
AS
BEGIN
    SELECT * FROM People
END


--------------------------------------------------------------------------------------------- SP_GetPersonByID

CREATE PROCEDURE SP_GetPersonByID
    @PersonID INT
AS
BEGIN
    SELECT * FROM People WHERE PersonID = @PersonID
END



--------------------------------------------------------------------------------------------- SP_GetPersonByNationalNumber

CREATE PROCEDURE SP_GetPersonByNationalNumber
    @NationalNumber NVARCHAR(50)
AS
BEGIN
    SELECT * FROM People WHERE NationalNumber = @NationalNumber
END



--------------------------------------------------------------------------------------------- SP_UpdatePerson

CREATE PROCEDURE SP_UpdatePerson
		@PersonID INT,
		@NationalNumber NVARCHAR(50),
		@FirstName NVARCHAR(100),
		@LastName NVARCHAR(100),
		@FatherName NVARCHAR(100),
		@MotherName NVARCHAR(100),
		@BirthPlaceID INT,
		@DateOfBirth DATE,
		@RegistrationAuthorityID INT,
		@RegistrationRecordID INT,
		@Gender BIT,
		@Adress NVARCHAR(255),
		@Phone NVARCHAR(20),
		@GrantHistory DATE,
		@CardNumber NVARCHAR(50)
AS
BEGIN
    UPDATE People
    SET NationalNumber = @NationalNumber, FirstName = @FirstName, LastName = @LastName, FatherName = @FatherName, MotherName = @MotherName, BirthPlaceID = @BirthPlaceID,
		DateOfBirth = @DateOfBirth, RegistrationAuthorityID = @RegistrationAuthorityID, RegistrationRecordID = @RegistrationRecordID, Gender = @Gender,
		Adress = @Adress, Phone = @Phone, GrantHistory = @GrantHistory, CardNumber = @CardNumber
    WHERE PersonID = @PersonID
END


--------------------------------------------------------------------------------------------- SP_DeletePerson

CREATE PROCEDURE SP_DeletePerson
    @PersonID INT
AS
BEGIN
    DELETE FROM People WHERE PersonID = @PersonID

	RETURN @@ROWCOUNT;
END


--------------------------------------------------------------------------------------------- SP_CheckPersonExists

CREATE PROCEDURE SP_CheckPersonExists
    @PersonID INT
AS
BEGIN
    IF EXISTS(SELECT * FROM People WHERE PersonID = @PersonID)
        RETURN 1;  -- Person exists
    ELSE
        RETURN 0;  -- Person does not exist
END

		



