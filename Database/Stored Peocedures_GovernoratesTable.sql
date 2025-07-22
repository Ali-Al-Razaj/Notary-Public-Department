USE NotaryPublicDepartment;



--------------------------------------------------------------------------------------------- SP_GetAllGovernorates

CREATE PROCEDURE SP_GetAllGovernorates
AS
BEGIN
    SELECT * FROM Governorates
END
-------------------------------------
EXEC SP_GetAllGovernorates;


--------------------------------------------------------------------------------------------- SP_GetGovernorateByID

CREATE PROCEDURE SP_GetGovernorateByID
    @GovernorateID INT
AS
BEGIN
    SELECT * FROM Governorates WHERE GovernorateID = @GovernorateID
END
-------------------------------------
EXEC SP_GetGovernorateByID 
		@GovernorateID = 1;


--------------------------------------------------------------------------------------------- SP_GetGovernorateByName

CREATE PROCEDURE SP_GetGovernorateByName
    @GovernorateName nvarchar(100)
AS
BEGIN
    SELECT * FROM Governorates WHERE GovernorateName = @GovernorateName
END
-------------------------------------
EXEC SP_GetGovernorateByName 
		@GovernorateName = 'دمشق';


