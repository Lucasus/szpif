Use szpifDatabase

-------------------------------------------------------------------
DROP FUNCTION dbo.agregatePermissionsFunction
GO
CREATE FUNCTION dbo.agregatePermissionsFunction 
( 
	@EmployeeID int 
) 
RETURNS varchar(8000)
AS

BEGIN

DECLARE @PermissionList varchar(8000)
SET @PermissionList = ''

SELECT @PermissionList = @PermissionList + ' ' + Permission + ', '
FROM Permissions
WHERE Permissions.EmployeeId = @EmployeeID

RETURN @PermissionList                --value will look something like 'Hdbk, Pbk'
END

GO