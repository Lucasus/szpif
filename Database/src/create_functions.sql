Use szpifDatabase
PRINT 'CREATING FUNCTIONS...'
-- --------------------usuwam jezeli istnieja-----------------------
GO
IF EXISTS 
(
	SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'aggregateRolesFunction'
	AND ROUTINE_SCHEMA = 'dbo'
	AND ROUTINE_TYPE = 'FUNCTION'
)
BEGIN
	DROP FUNCTION dbo.aggregateRolesFunction
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
CREATE FUNCTION dbo.aggregateRolesFunction 
( 
	@EmployeeID int 
) 
RETURNS varchar(8000)
AS
	BEGIN
	DECLARE @RoleList varchar(8000)
	SET @RoleList = ''
	SELECT @RoleList = @RoleList + ' ' + Role + ', '
		FROM Roles
		WHERE Roles.EmployeeId = @EmployeeID
	RETURN @RoleList
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
