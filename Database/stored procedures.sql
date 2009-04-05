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
DROP PROCEDURE getTablePriviliges
GO
CREATE PROCEDURE getTablePriviliges
		@table nvarchar(40),
		@role nvarchar(40),
		@wynik nvarchar(2000) OUTPUT
AS
	DECLARE @temp TABLE([DataBase] nchar(40), [Owner] nchar(40), [Table] nchar(40), [Column] nchar(40), [Grantor] nchar(40), [Grantee] nchar(40), [Privilige] nchar(40), [Is Grantable] nchar(40))
	INSERT INTO @temp EXECUTE sp_column_privileges @table_name = @table
	DECLARE @columns TABLE([Columns] nchar(40))
	INSERT INTO @columns SELECT [Column] FROM @temp WHERE [Grantee] = @role
	
	SET @wynik = ''
	SELECT @wynik = @wynik + ' ' + [Columns] +  ','
	FROM @columns
	IF (LEN(@wynik) > 1) SET @wynik = LEFT (@wynik, LEN(@wynik)-1) 
GO
-------------------------------------------------------------------
DROP PROCEDURE mySelectAll
GO
CREATE PROCEDURE mySelectAll
		@table nvarchar(40),
		@role nvarchar(40),
		@where nvarchar(40) = ''
AS
	DECLARE @wynik nvarchar(2000)
	EXECUTE getTablePriviliges @table,@role,@wynik OUTPUT
	
	IF (LEN(@wynik) > 1)
	BEGIN
		DECLARE @sql nvarchar(2000)
		SET @sql = 'SELECT ' + @wynik + ' FROM ' + @table
		if(LEN(@where) > 1) SET @sql = @sql + ' WHERE ' + @where
		EXECUTE (@sql)
	END
GO

EXECUTE mySelectAll 'Employees', 'Employer'