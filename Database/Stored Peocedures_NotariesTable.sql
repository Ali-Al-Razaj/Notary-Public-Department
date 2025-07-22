USE NotaryPublicDepartment;

--------------------------------------------------------------------------------------------- SP_AddNewNotary

CREATE PROCEDURE SP_AddNewNotary
    @Number INT,
	@GovernorateID INT,
	@Name NVARCHAR(100),
    @NewNotaryID INT OUTPUT
AS
BEGIN
    INSERT INTO Notaries(Number, GovernorateID, Name)
    VALUES (@Number, @GovernorateID, @Name);


    SET @NewNotaryID = SCOPE_IDENTITY();
END
-------------------------------------
DECLARE @NewNotaryID INT;

EXEC SP_AddNewNotary
    @Number = 1,
    @GovernorateID = 1,
    @Name = 'أحمد طالب',
    @NewNotaryID = @NewNotaryID OUTPUT;

-- Check the new person ID
SELECT @NewNotaryID AS NewNotaryID;

--------------------------------------------------------------------------------------------- SP_GetAllNotaries

CREATE PROCEDURE SP_GetAllNotaries
AS
BEGIN
    SELECT * FROM Notaries
END
-------------------------------------
EXEC SP_GetAllNotaries;


--------------------------------------------------------------------------------------------- SP_GetNotaryByID

CREATE PROCEDURE SP_GetNotaryByID
    @NotaryID INT
AS
BEGIN
    SELECT * FROM Notaries WHERE NotaryPublicID = @NotaryID
END
-------------------------------------
EXEC SP_GetNotaryByID 
		@NotaryID = 1;

--------------------------------------------------------------------------------------------- SP_GetNotaryByName

CREATE PROCEDURE SP_GetNotaryByName
    @NotaryName NVARCHAR(100)
AS
BEGIN
    SELECT * FROM Notaries WHERE Name = @NotaryName
END
-------------------------------------
EXEC SP_GetNotaryByName 
		@NotaryName = 'أحمد طالب';


--------------------------------------------------------------------------------------------- SP_UpdateNotary

CREATE PROCEDURE SP_UpdateNotary
		@NotaryPublicID INT,
		@Number INT,
		@GovernorateID INT,
		@Name NVARCHAR(100)
AS
BEGIN
    UPDATE Notaries
    SET Number = @Number, GovernorateID = @GovernorateID, Name = @Name
    WHERE NotaryPublicID = @NotaryPublicID
END
-------------------------------------
EXEC SP_UpdateNotary
		@NotaryPublicID = 1,
		@Number = 1,
		@GovernorateID = 1,
		@Name = 'أحمد طالب';


--------------------------------------------------------------------------------------------- SP_DeleteNotary
CREATE PROCEDURE SP_DeleteNotary
    @NotaryPublicID INT
AS
BEGIN
    DELETE FROM Notaries WHERE NotaryPublicID = @NotaryPublicID

	RETURN @@ROWCOUNT;
END
-------------------------------------
EXEC SP_DeleteNotary
	@NotaryPublicID = 2;

