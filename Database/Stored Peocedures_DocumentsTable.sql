USE NotaryPublicDepartment;

--------------------------------------------------------------------------------------------- SP_AddNewDocument

CREATE PROCEDURE SP_AddNewDocument
	@DocumentTypeID INT,
    @Date DATE,
	@NotaryPublicID INT,
    @NewDocumentID INT OUTPUT
AS
BEGIN
    INSERT INTO Documents(DocumentTypeID, Date, NotaryPublicID)
    VALUES (@DocumentTypeID, @Date, @NotaryPublicID);


    SET @NewDocumentID = SCOPE_IDENTITY();
END
-------------------------------------
DECLARE @NewDocumentID INT;

EXEC SP_AddNewDocument
    @DocumentTypeID = 1,
    @Date = '1990-01-01',
    @NotaryPublicID = 1,
    @NewDocumentID = @NewDocumentID OUTPUT;

-- Check the new person ID
SELECT @NewDocumentID AS NewDocumentID;

--------------------------------------------------------------------------------------------- SP_GetAllDocuments

CREATE PROCEDURE SP_GetAllDocuments
AS
BEGIN
    SELECT * FROM Documents
END
-------------------------------------
EXEC SP_GetAllDocuments;


--------------------------------------------------------------------------------------------- SP_GetDocumentByID

CREATE PROCEDURE SP_GetDocumentByID
    @DocumentID INT
AS
BEGIN
    SELECT * FROM Documents WHERE DocumentID = @DocumentID
END
-------------------------------------
EXEC SP_GetDocumentByID 
		@DocumentID = 1;



--------------------------------------------------------------------------------------------- SP_DeleteDocument
CREATE PROCEDURE SP_DeleteDocument
    @DocumentID INT
AS
BEGIN
    DELETE FROM Documents WHERE DocumentID = @DocumentID

	RETURN @@ROWCOUNT;
END
-------------------------------------
EXEC SP_DeleteDocument
	@DocumentID = 21;

--------------------------------------------------------------------------------------------- SP_CheckDocumentExists

CREATE PROCEDURE SP_CheckDocumentExists
    @DocumentID INT
AS
BEGIN
    IF EXISTS(SELECT * FROM Documents WHERE DocumentID = @DocumentID)
        RETURN 1;  -- exists
    ELSE
        RETURN 0;  -- does not exist
END
-------------------------------------
DECLARE @IsExists INT;	
EXEC @IsExists = SP_CheckDocumentExists @DocumentID = 1;
IF @IsExists = 1
    PRINT 'exists.';
ELSE
    PRINT 'does not exist.';