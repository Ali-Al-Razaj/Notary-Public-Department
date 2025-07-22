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
-------------------------------------
DECLARE @ID INT;

EXEC SP_AddNewRegistrationRecord
    @RegistrationRecordName = 'كيوان 86',
    @NewRegistrationRecordID = @ID OUTPUT;

-- Check the new person ID
SELECT @ID AS NewRID;

--------------------------------------------------------------------------------------------- SP_GetAllRegistrationRecords

CREATE PROCEDURE SP_GetAllRegistrationRecords
AS
BEGIN
    SELECT * FROM RegistrationRecords
END
-------------------------------------
EXEC SP_GetAllRegistrationRecords;


--------------------------------------------------------------------------------------------- SP_GetRegistrationRecordByID

CREATE PROCEDURE SP_GetRegistrationRecordByID
    @RegistrationRecordID INT
AS
BEGIN
    SELECT * FROM RegistrationRecords WHERE RegistrationRecordID = @RegistrationRecordID
END
-------------------------------------
EXEC SP_GetRegistrationRecordByID 
		@RegistrationRecordID = 2;


--------------------------------------------------------------------------------------------- SP_GetRegistrationRecordByName

CREATE PROCEDURE SP_GetRegistrationRecordByName
    @RegistrationRecordName NVARCHAR(100)
AS
BEGIN
    SELECT * FROM RegistrationRecords WHERE RegistrationRecordName = @RegistrationRecordName
END
-------------------------------------
EXEC SP_GetRegistrationRecordByName 
		@RegistrationRecordName = 'كيوان 86';


