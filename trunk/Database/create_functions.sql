Use szpifDatabase
PRINT 'CREATING FUNCTIONS...'
-- --------------------usuwam jezeli istnieja-----------------------
GO
IF Object_ID('dbo.aggregatePermissionsFunction','FN') IS NOT NULL 
BEGIN
	DROP FUNCTION dbo.aggregatePermissionsFunction
END

IF EXISTS 
(
	SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'getEmployeeByLoginAndPassword'
	AND ROUTINE_SCHEMA = 'dbo'
	AND ROUTINE_TYPE = 'FUNCTION'
)
BEGIN
	DROP FUNCTION dbo.getEmployeeByLoginAndPassword
END
GO
/* ------------------------------------------------------------------
   Poni¿sza funkcja zwraca stringa z lista uprawnien danego pracownika
   w postaci 'uprawnienie1, uprawnienie2, ...'
------------------------------------------------------------------- */
CREATE FUNCTION dbo.aggregatePermissionsFunction 
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
	RETURN @PermissionList
END

-------------------------------------------------------------------
GO
CREATE FUNCTION dbo.getEmployeeByLoginAndPassword
		(
		@Login nvarchar(40),
		@Password nvarchar(40) 
		)
RETURNS TABLE
AS
RETURN 
	(	
		SELECT *
			FROM Employees
			WHERE Login=@Login AND Password=@Password
	)
GO
