-- TRIGGERS --
GO
PRINT 'GENERATING TRIGGERS...'
GO
CREATE TRIGGER Employees_Insert ON Employees
FOR INSERT
AS

IF (SELECT COUNT(*) FROM Inserted) > 1
	BEGIN
		RAISERROR('Maksymalnie na raz mo¿na dodaæ jedno po³¹czenie', 16, 1)
		ROLLBACK TRANSACTION
    END
    declare @login varchar(40);
    declare @password varchar(40);
    select @login = (select Login from Inserted)
    select @password = (select Password from Inserted)
    
	   if USER_ID(@login) is not null
	   BEGIN
         PRINT 'Usuwam ' + @login;
	     EXEC('DROP USER ' + @login)
	   END     
    
	if not exists(select * from sys.server_principals
		where type IN ('S', 'U', 'G') and name = @login)
		begin
		EXEC('CREATE LOGIN ' + @login + ' WITH PASSWORD = "' + @password + '"')
	end

	if USER_ID(@login) is  null
	BEGIN
		PRINT 'Tworze login' + @login;
		EXEC('CREATE USER ' + @login + ' FOR LOGIN ' + @login + ';')
		EXEC sp_addrolemember 'BasicRole', @login
	END

GO


CREATE TRIGGER Employees_Delete ON Employees
FOR DELETE
AS
     DECLARE @login nvarchar(40);  
     DECLARE @Id int;  
     DECLARE @ICURSOR CURSOR;  
     SET @ICURSOR = CURSOR FOR SELECT Id, Login FROM Deleted;   
     OPEN @ICURSOR  
     FETCH NEXT FROM @ICURSOR INTO @Id, @login
     WHILE (@@FETCH_STATUS = 0)  
     BEGIN  
		if exists(select * from sys.server_principals
		where type IN ('S', 'U', 'G') and name = @login)
		begin
--			set @name = quotename('somelogin')
			exec('drop login ' + @login)
		end

	   if USER_ID(@login) is not null
	   BEGIN
         PRINT 'Usuwam ' + @login;
	     EXEC sp_droprolemember 'BasicRole', @login
	     EXEC('DROP USER ' + @login)
--	     EXEC('DROP LOGIN ' + @login)
	   END     
	   DELETE FROM Roles where EmployeeId = @Id	       
       FETCH NEXT FROM @ICURSOR INTO @Id, @login;  
     END  
     CLOSE @ICURSOR  
     DEALLOCATE @ICURSOR;  
GO

CREATE TRIGGER Roles_Insert ON Roles
FOR INSERT
AS
IF (SELECT COUNT(*) FROM Inserted) > 1
	BEGIN
		RAISERROR('Maksymalnie na raz mo¿na dodaæ jedno po³¹czenie', 16, 1)
		ROLLBACK TRANSACTION
    END
    declare @role varchar(40);
    declare @login varchar(40);
    select @role = (SELECT Role from Inserted)
    select @login = (SELECT Login from Employees where Id = 
			(select EmployeeId from Inserted))
    IF @role = 'Boss'
    BEGIN
		EXEC sp_addrolemember 'OwnerRole', @login
    END
GO

CREATE TRIGGER Roles_Delete ON Roles
FOR DELETE
AS
	 PRINT 'Usuwam rekord w Roles';
     DECLARE @role nvarchar(40);  
     DECLARE @login nvarchar(40);  
     DECLARE @Id int;  
     DECLARE @ICURSOR CURSOR;  
     SET @ICURSOR = CURSOR FOR SELECT EmployeeId, Role FROM Deleted;   
     OPEN @ICURSOR  
     FETCH NEXT FROM @ICURSOR INTO @Id, @login
     WHILE (@@FETCH_STATUS = 0)  
     BEGIN  
		select @login = (select Login from Employees where Id = @Id)
		IF @role = 'Boss'
		BEGIN			
			PRINT 'Usuwam role Owner dla ' + @login;
			EXEC sp_droprolemember 'OwnerRole', @login     
		END
	       
       FETCH NEXT FROM @ICURSOR INTO @Id, @role;  
     END  
     CLOSE @ICURSOR  
     DEALLOCATE @ICURSOR;  

GO
