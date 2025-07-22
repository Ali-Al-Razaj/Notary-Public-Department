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


--------------------------------------------------------------------------------------------- SP_GetAllUsers

CREATE PROCEDURE SP_GetAllUsers
AS
BEGIN
    SELECT * FROM Users
END

--------------------------------------------------------------------------------------------- SP_GetUserByID

CREATE PROCEDURE SP_GetUserByID
    @UserID INT
AS
BEGIN
    SELECT * FROM Users WHERE UserID = @UserID
END



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



--------------------------------------------------------------------------------------------- SP_DeleteUser
CREATE PROCEDURE SP_DeleteUser
    @UserID INT
AS
BEGIN
    DELETE FROM Users WHERE UserID = @UserID

	RETURN @@ROWCOUNT;
END


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



