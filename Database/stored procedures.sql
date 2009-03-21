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
/*DROP PROCEDURE changePassword
GO
CREATE PROCEDURE changePassword
		@Login nvarchar(40),
		@currentPassword nvarchar(40),
		@Password nvarchar(40)
AS
	update Employees 
	set Password=@Password
	where Login=@Login AND Password=@currentPassword
GO*/
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
DROP PROCEDURE changeAtributeFromTable
GO
CREATE PROCEDURE changeAtributeFromTable
		@TableName nvarchar(40),
		@TableAttribute nvarchar(40),
		@value nvarchar(40),
		@ChangeRowId nvarchar(10)
AS
	declare @SQL nvarchar(4000)
	set @SQL = 'update ' + @TableName + ' set ' + @TableAttribute + '=''' + @value + ''' where Id=' + @ChangeRowId
	EXEC (@SQL)
GO

--EXEC changeAtributeFromTable 'Employees', 'Login', 'MooseMaster', '1'