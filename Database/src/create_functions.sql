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

IF EXISTS 
(
	SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'xmlRoles'
	AND ROUTINE_SCHEMA = 'dbo'
	AND ROUTINE_TYPE = 'FUNCTION'
)
BEGIN
	DROP FUNCTION dbo.xmlRoles
END
GO

IF EXISTS 
(
	SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'przelozonyToXmlLink'
	AND ROUTINE_SCHEMA = 'dbo'
	AND ROUTINE_TYPE = 'FUNCTION'
)
BEGIN
	DROP FUNCTION dbo.przelozonyToXmlLink
END
GO
IF EXISTS 
(
	SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'stringToBit'
	AND ROUTINE_SCHEMA = 'dbo'
	AND ROUTINE_TYPE = 'FUNCTION'
)
BEGIN
	DROP FUNCTION dbo.stringToBit
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
GO
/*------------------------------------------------------------------- */
CREATE FUNCTION dbo.xmlRoles
( 
	@EmployeeID int 
) 
RETURNS XML
AS
	BEGIN
	DECLARE @RoleList XML
	select @RoleList = 
		(
		  select 'Roles' as '@Name', (select  RoleName as Name, dbo.stringToBit(Role) as Value from RoleNames Item
			left join (SELECT Role FROM Roles  WHERE Roles.EmployeeId = @EmployeeID) roles 
			on roles.Role = Item.RoleName FOR XML AUTO, TYPE)
			for xml path('CheckedListBox')
		) 	
RETURN @RoleList
END

GO
/*------------------------------------------------------------------- */
CREATE FUNCTION dbo.przelozonyToXmlLink
( 
	@PrzelozonyID int 
) 
RETURNS XML
AS
	BEGIN
	DECLARE @Link XML
	declare @credId int;
	select @credId = (select CredentialsId from Employees where Id = @PrzelozonyID)
	select @Link = 
		(
			select 'Przelozony' as Name, Id, (select Name from Credentials where Id = @credId) as Text
			from Employees Link where Id = @PrzelozonyID
			FOR XML AUTO, TYPE
		) 	
RETURN @Link
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
----------------------------------------------------------
CREATE FUNCTION dbo.stringToBit
		(
		@string nvarchar(40)
		)
RETURNS bit
AS
BEGIN
	IF @string IS NULL
		return 0
	ELSE 
		return 1
	return 0
END
GO
