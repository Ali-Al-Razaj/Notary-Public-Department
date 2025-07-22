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


--------------------------------------------------------------------------------------------- SP_GetAllRecords

CREATE PROCEDURE SP_GetAllRecords
AS
BEGIN
    SELECT * FROM DocumentPeopleRecords
END



--------------------------------------------------------------------------------------------- SP_GetRecordByID

CREATE PROCEDURE SP_GetRecordByID
    @RecordID INT
AS
BEGIN
    SELECT * FROM DocumentPeopleRecords WHERE RecordID = @RecordID
END




--------------------------------------------------------------------------------------------- SP_DeleteRecord
CREATE PROCEDURE SP_DeleteRecord
    @RecordID INT
AS
BEGIN
    DELETE FROM DocumentPeopleRecords WHERE RecordID = @RecordID

	RETURN @@ROWCOUNT;
END


