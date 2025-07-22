USE NotaryPublicDepartment;



--------------------------------------------------------------------------------------------- SP_GetAllGovernorates

CREATE PROCEDURE SP_GetAllGovernorates
AS
BEGIN
    SELECT * FROM Governorates
END



--------------------------------------------------------------------------------------------- SP_GetGovernorateByID

CREATE PROCEDURE SP_GetGovernorateByID
    @GovernorateID INT
AS
BEGIN
    SELECT * FROM Governorates WHERE GovernorateID = @GovernorateID
END



--------------------------------------------------------------------------------------------- SP_GetGovernorateByName

CREATE PROCEDURE SP_GetGovernorateByName
    @GovernorateName nvarchar(100)
AS
BEGIN
    SELECT * FROM Governorates WHERE GovernorateName = @GovernorateName
END



