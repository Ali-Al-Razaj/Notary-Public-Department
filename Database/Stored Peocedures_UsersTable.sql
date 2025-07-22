USE NotaryPublicDepartment;

--------------------------------------------------------------------------------------------- SP_AddNewUser

CREATE PROCEDURE SP_AddNewUser
	@PersonID INT,
    @Username NVARCHAR(50),
	@Password NVARCHAR(50),
    @NewUserID INT OUTPUT
AS
BEGIN
    INSERT INTO Users(PersonID, Username, Password)
    VALUES (@PersonID, @Username, @Password);


    SET @NewUserID = SCOPE_IDENTITY();
END
-------------------------------------
DECLARE @NewUserID INT;

EXEC SP_AddNewUser
    @PersonID = 5,
    @Username = 'Aloosh',
    @Password = 'uu@per',
    @NewUserID = @NewUserID OUTPUT;

-- Check the new person ID
SELECT @NewUserID AS NewUserID;

--------------------------------------------------------------------------------------------- SP_GetAllUsers

CREATE PROCEDURE SP_GetAllUsers
AS
BEGIN
    SELECT * FROM Users
END
-------------------------------------
EXEC SP_GetAllUsers;

--------------------------------------------------------------------------------------------- SP_GetUserByID

CREATE PROCEDURE SP_GetUserByID
    @UserID INT
AS
BEGIN
    SELECT * FROM Users WHERE UserID = @UserID
END
-------------------------------------
EXEC SP_GetUserByID 
		@UserID = 2;


--------------------------------------------------------------------------------------------- SP_UpdateUser

CREATE PROCEDURE SP_UpdateUser
		@UserID INT,
		@PersonID INT,
		@Username NVARCHAR(50),
		@Password NVARCHAR(100)
AS
BEGIN
    UPDATE Users
    SET PersonID = @PersonID, Username = @Username, Password = @Password
    WHERE UserID = @UserID
END
-------------------------------------
EXEC SP_UpdateUser
		@UserID = 2,
		@PersonID = 5,
		@Username = AAAA,
		@Password = AAAA;


--------------------------------------------------------------------------------------------- SP_DeleteUser
CREATE PROCEDURE SP_DeleteUser
    @UserID INT
AS
BEGIN
    DELETE FROM Users WHERE UserID = @UserID

	RETURN @@ROWCOUNT;
END
-------------------------------------
EXEC SP_DeleteUser
	@UserID = 21;

--------------------------------------------------------------------------------------------- SP_CheckUserExists

CREATE PROCEDURE SP_CheckUserExists
    @UserID INT
AS
BEGIN
    IF EXISTS(SELECT * FROM Users WHERE UserID = @UserID)
        RETURN 1;  -- exists
    ELSE
        RETURN 0;  -- does not exist
END
-------------------------------------
DECLARE @IsExists INT;	
EXEC @IsExists = SP_CheckUserExists @UserID = 1002;
IF @IsExists = 1
    PRINT 'exists.';
ELSE
    PRINT 'does not exist.';


--------------------------------------------------------------------------------------------- SP_Login

CREATE PROCEDURE SP_Login
    @Username NVARCHAR(50),
	@Password NVARCHAR(50)
AS
BEGIN
    IF EXISTS(SELECT * FROM Users WHERE Username = @Username and Password = @Password)
        RETURN 1;  -- exists
    ELSE
        RETURN 0;  -- does not exist
END
-------------------------------------
DECLARE @IsExists INT;	
EXEC @IsExists = SP_Login @Username = 'Aloosh', @Password = '0034989903590';
IF @IsExists = 1
    PRINT 'exists.';
ELSE
    PRINT 'does not exist.';


