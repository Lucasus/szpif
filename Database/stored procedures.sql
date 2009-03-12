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
--DROP PROCEDURE updateEmployeed