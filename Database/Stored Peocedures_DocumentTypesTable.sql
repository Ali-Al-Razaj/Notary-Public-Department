USE NotaryPublicDepartment;

--------------------------------------------------------------------------------------------- SP_AddNewDocumentType

CREATE PROCEDURE SP_AddNewDocumentType
	@Title NVARCHAR(200),
	@Body NVARCHAR(MAX),
    @NewDocumentTypeID INT OUTPUT
AS
BEGIN
    INSERT INTO DocumentsTypes(Title, Body)
    VALUES (@Title, @Body);


    SET @NewDocumentTypeID = SCOPE_IDENTITY();
END


--------------------------------------------------------------------------------------------- SP_GetAllDocumentTypes

CREATE PROCEDURE SP_GetAllDocumentTypes
AS
BEGIN
    SELECT * FROM DocumentsTypes
END



--------------------------------------------------------------------------------------------- SP_GetDocumentTypeByID

CREATE PROCEDURE SP_GetDocumentTypeByID
    @DocumentTypeID INT
AS
BEGIN
    SELECT * FROM DocumentsTypes WHERE DocumentTypeID = @DocumentTypeID
END



--------------------------------------------------------------------------------------------- SP_GetDocumentTypeByTitle

CREATE PROCEDURE SP_GetDocumentTypeByTitle
    @Title NVARCHAR(200)
AS
BEGIN
    SELECT * FROM DocumentsTypes WHERE Title = @Title
END



--------------------------------------------------------------------------------------------- SP_UpdateDocumentType

CREATE PROCEDURE SP_UpdateDocumentType
		@DocumentTypeID INT,
		@Title NVARCHAR(200),
		@Body NVARCHAR(MAX)
AS
BEGIN
    UPDATE DocumentsTypes
    SET Title = @Title, Body = @Body
    WHERE DocumentTypeID = @DocumentTypeID
END


--------------------------------------------------------------------------------------------- SP_DeleteDocumentType
CREATE PROCEDURE SP_DeleteDocumentType
    @DocumentTypeID INT
AS
BEGIN
    DELETE FROM DocumentsTypes WHERE DocumentTypeID = @DocumentTypeID

	RETURN @@ROWCOUNT;
END


--------------------------------------------------------------------------------------------- SP_CheckDocumentTypeExists

CREATE PROCEDURE SP_CheckDocumentTypeExists
    @DocumentTypeID INT
AS
BEGIN
    IF EXISTS(SELECT * FROM DocumentsTypes WHERE DocumentTypeID = @DocumentTypeID)
        RETURN 1;  -- exists
    ELSE
        RETURN 0;  -- does not exist
END
