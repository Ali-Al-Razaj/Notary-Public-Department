USE NotaryPublicDepartment;

--------------------------------------------------------------------------------------------- SP_AddNewRegistrationRecord

CREATE PROCEDURE SP_AddNewRegistrationRecord
	@RegistrationRecordName NVARCHAR(100),
    @NewRegistrationRecordID INT OUTPUT
AS
BEGIN
    INSERT INTO RegistrationRecords(RegistrationRecordName)
    VALUES (@RegistrationRecordName);


    SET @NewRegistrationRecordID = SCOPE_IDENTITY();
END


--------------------------------------------------------------------------------------------- SP_GetAllRegistrationRecords

CREATE PROCEDURE SP_GetAllRegistrationRecords
AS
BEGIN
    SELECT * FROM RegistrationRecords
END



--------------------------------------------------------------------------------------------- SP_GetRegistrationRecordByID

CREATE PROCEDURE SP_GetRegistrationRecordByID
    @RegistrationRecordID INT
AS
BEGIN
    SELECT * FROM RegistrationRecords WHERE RegistrationRecordID = @RegistrationRecordID
END



--------------------------------------------------------------------------------------------- SP_GetRegistrationRecordByName

CREATE PROCEDURE SP_GetRegistrationRecordByName
    @RegistrationRecordName NVARCHAR(100)
AS
BEGIN
    SELECT * FROM RegistrationRecords WHERE RegistrationRecordName = @RegistrationRecordName
END



