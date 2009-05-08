PRINT 'CREATING PROCEDURES...'
GO
----------Kasowanie poprzednich wersji--------------------------------
IF OBJECT_ID('checkPermissions') IS NOT NULL
   DROP PROCEDURE DBO.checkPermissions
GO
IF OBJECT_ID('changePassword') IS NOT NULL
	DROP PROCEDURE changePassword
GO
IF OBJECT_ID('changeEmail') IS NOT NULL
	DROP PROCEDURE changeEmail
GO
IF OBJECT_ID('getTablePriviliges') IS NOT NULL
	DROP PROCEDURE getTablePriviliges
GO
IF OBJECT_ID('mySelectAll') IS NOT NULL
	DROP PROCEDURE mySelectAll
GO
IF OBJECT_ID('getRolesViewForCurrentUser') IS NOT NULL
	DROP PROCEDURE getRolesViewForCurrentUser
GO

IF OBJECT_ID('getRolesViewForCurrentUser') IS NOT NULL
	DROP PROCEDURE getRolesViewForCurrentUser
GO

IF OBJECT_ID('getTypeSchema') IS NOT NULL
	DROP PROCEDURE getTypeSchema
GO

CREATE PROCEDURE getTypeSchema
	@ViewName nvarchar (40),
	@ColumnName nvarchar (40)
AS
	declare @typeName nvarchar(40);
	select @typeName = (select TypeName from ColumnsToTypes where
	  ViewName = @ViewName and ColumnName = @ColumnName)
	select Name, TypeSchema from UserTypes where Name = @TypeName		 
GO
CREATE PROCEDURE getRolesViewForCurrentUser
AS
	--SET NOCOUNT ON
    declare @login varchar(40);
    select @login = SYSTEM_USER
	select Role from [Employees] em
		inner join [Roles] perm on em.Id = perm.EmployeeId
		where Login = @login
GO