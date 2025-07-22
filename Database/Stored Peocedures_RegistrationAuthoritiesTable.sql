USE NotaryPublicDepartment;

--------------------------------------------------------------------------------------------- SP_AddNewRegistrationAuthority

CREATE PROCEDURE SP_AddNewRegistrationAuthority
	@RegistrationAuthorityName NVARCHAR(100),
    @NewRegistrationAuthorityID INT OUTPUT
AS
BEGIN
    INSERT INTO RegistrationAuthorities(RegistrationAuthorityName)
    VALUES (@RegistrationAuthorityName);


    SET @NewRegistrationAuthorityID = SCOPE_IDENTITY();
END
-------------------------------------
DECLARE @ID INT;

EXEC SP_AddNewRegistrationAuthority
    @RegistrationAuthorityName = 'الأمانة المركزية',
    @NewRegistrationAuthorityID = @ID OUTPUT;

-- Check the new person ID
SELECT @ID AS NewRID;

--------------------------------------------------------------------------------------------- SP_GetAllRegistrationAuthorities

CREATE PROCEDURE SP_GetAllRegistrationAuthorities
AS
BEGIN
    SELECT * FROM RegistrationAuthorities
END
-------------------------------------
EXEC SP_GetAllRegistrationAuthorities;


--------------------------------------------------------------------------------------------- SP_GetRegistrationAuthorityByID

CREATE PROCEDURE SP_GetRegistrationAuthorityByID
    @RegistrationAuthorityID INT
AS
BEGIN
    SELECT * FROM RegistrationAuthorities WHERE RegistrationAuthorityID = @RegistrationAuthorityID
END
-------------------------------------
EXEC SP_GetRegistrationAuthorityByID 
		@RegistrationAuthorityID = 1;


--------------------------------------------------------------------------------------------- SP_GetRegistrationAuthorityByName

CREATE PROCEDURE SP_GetRegistrationAuthorityByName
    @RegistrationAuthorityName NVARCHAR(100)
AS
BEGIN
    SELECT * FROM RegistrationAuthorities WHERE RegistrationAuthorityName = @RegistrationAuthorityName
END
-------------------------------------
EXEC SP_GetRegistrationAuthorityByName 
		@RegistrationAuthorityName = 'الميادين';


