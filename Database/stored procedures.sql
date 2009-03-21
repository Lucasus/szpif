use szpifDatabase;

-------------------------------------------------------------------
DROP PROCEDURE checkPermissions
GO
CREATE PROCEDURE checkPermissions
		@Login nvarchar(40),
		@Password nvarchar(40)
AS
	select Permission from [Employees] em
		inner join [Permissions]perm on em.Id = perm.EmployeeId
		where Login = @Login AND Password = @Password
GO
-------------------------------------------------------------------
DROP PROCEDURE changePassword
GO
CREATE PROCEDURE changePassword
		@Login nvarchar(40),
		@currentPassword nvarchar(40),
		@Password nvarchar(40)
AS
	update Employees 
	set Password=@Password
	where Login=@Login AND Password=@currentPassword
GO
-------------------------------------------------------------------
DROP PROCEDURE getEmployeeIdByLoginAndPassword
GO
CREATE PROCEDURE getEmployeeIdByLoginAndPassword
		@Login nvarchar(40),
		@Password nvarchar(40) 
AS
	SELECT Id
	FROM Employees
	WHERE Login=@Login AND Password=@Password
GO
-------------------------------------------------------------------
DROP PROCEDURE changeEmail
GO
CREATE PROCEDURE changeEmail
		@Login nvarchar(40),
		@Password nvarchar(40),
		@newEmail nvarchar(40)
AS
	DECLARE @Id int

	SELECT @Id = CredentialsId
	FROM getEmployeeByLoginAndPassword(@Login, @Password)
	
	update Credentials
	set EMail=@newEmail
	where Id=@Id
GO
-------------------------------------------------------------------
/*
DROP PROCEDURE getAttributeFromTable
GO
CREATE PROCEDURE getAttributeFromTable
		@what nvarchar(40),
		@from nvarchar(40),
		@where nvarchar(400)
AS
	DECLARE @SQL nvarchar(4000)
	SET @SQL = 'select ' + @what + ' from ' + @from + ' where ' + @where
	EXEC (@SQL)
GO*/
-------------------------------------------------------------------
/*DROP PROCEDURE changeAtributeFromTable
GO
CREATE PROCEDURE changeAtributeFromTable
		@tableName nvarchar(40),
		@tableAttribute nvarchar(40),
		@value nvarchar(40),
		@conditionColumn nvarchar(40),
		@conditionValue nvarchar(10)
AS
	DECLARE @SQL nvarchar(4000)
	SET @SQL = 'update ' + @tableName + ' set ' + @tableAttribute + '=''' + @value + ''' where ' + @conditionColumn + '=' + @conditionValue
	EXEC (@SQL)
GO*/

--EXEC changeAtributeFromTable 'Employees', 'Login', 'MooseMaster', 'Id', '1'
--EXEC getAttributeFromTable 'Employees', 'Login', 'Id', '1'