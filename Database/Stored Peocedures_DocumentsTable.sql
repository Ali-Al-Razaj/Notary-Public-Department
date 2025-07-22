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


--------------------------------------------------------------------------------------------- SP_GetAllDocuments

CREATE PROCEDURE SP_GetAllDocuments
AS
BEGIN
    SELECT * FROM Documents
END


--------------------------------------------------------------------------------------------- SP_GetDocumentByID

CREATE PROCEDURE SP_GetDocumentByID
    @DocumentID INT
AS
BEGIN
    SELECT * FROM Documents WHERE DocumentID = @DocumentID
END




--------------------------------------------------------------------------------------------- SP_DeleteDocument
CREATE PROCEDURE SP_DeleteDocument
    @DocumentID INT
AS
BEGIN
    DELETE FROM Documents WHERE DocumentID = @DocumentID

	RETURN @@ROWCOUNT;
END


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
