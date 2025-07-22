USE NotaryPublicDepartment;

--------------------------------------------------------------------------------------------- SP_AddNewRecord

CREATE PROCEDURE SP_AddNewRecord
	@DocumentID INT,
    @PersonID INT,
	@PersonRole BIT,
    @NewRecordID INT OUTPUT
AS
BEGIN
    INSERT INTO DocumentPeopleRecords(DocumentID, PersonID, PersonRole)
    VALUES (@DocumentID, @PersonID, @PersonRole);


    SET @NewRecordID = SCOPE_IDENTITY();
END
-------------------------------------
DECLARE @NewRecordID INT;

EXEC SP_AddNewRecord
    @DocumentID = 1,
    @PersonID = 5,
    @PersonRole = 0,
    @NewRecordID = @NewRecordID OUTPUT;

-- Check the new person ID
SELECT @NewRecordID AS NewRecordID;

--------------------------------------------------------------------------------------------- SP_GetAllRecords

CREATE PROCEDURE SP_GetAllRecords
AS
BEGIN
    SELECT * FROM DocumentPeopleRecords
END
-------------------------------------
EXEC SP_GetAllRecords;


--------------------------------------------------------------------------------------------- SP_GetRecordByID

CREATE PROCEDURE SP_GetRecordByID
    @RecordID INT
AS
BEGIN
    SELECT * FROM DocumentPeopleRecords WHERE RecordID = @RecordID
END
-------------------------------------
EXEC SP_GetRecordByID 
		@RecordID = 12;



--------------------------------------------------------------------------------------------- SP_DeleteRecord
CREATE PROCEDURE SP_DeleteRecord
    @RecordID INT
AS
BEGIN
    DELETE FROM DocumentPeopleRecords WHERE RecordID = @RecordID

	RETURN @@ROWCOUNT;
END
-------------------------------------
EXEC SP_DeleteRecord
	@RecordID = 21;

