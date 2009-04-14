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

	EXEC('CREATE LOGIN ' + @login + ' WITH PASSWORD = "' + @password + '"')
	EXEC('CREATE USER ' + @login + ' FOR LOGIN ' + @login + ';')
	EXEC sp_addrolemember 'BasicRole', @login

GO


CREATE TRIGGER Employees_Delete ON Employees
FOR DELETE
AS
     DECLARE @login nvarchar(40);  
     DECLARE @ICURSOR CURSOR;  
     SET @ICURSOR = CURSOR FOR SELECT Login FROM Deleted;   
     OPEN @ICURSOR  
     FETCH NEXT FROM @ICURSOR INTO @login
     WHILE (@@FETCH_STATUS = 0)  
     BEGIN  
	   EXEC('DROP USER ' + @login)
	   EXEC('DROP LOGIN ' + @login)
  
         --opearacje, które chcemy przeprowadziæ dla ka¿dego wiersza  
         FETCH NEXT FROM @ICURSOR INTO @login;  
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
--    declare @login varchar(40);
--    declare @password varchar(40);
--    select @login = (select Login from Inserted)
--    select @password = (select Password from Inserted)

--	EXEC('CREATE LOGIN ' + @login + ' WITH PASSWORD = "' + @password + '"')
--	EXEC('CREATE USER ' + @login + ' FOR LOGIN ' + @login + ';')

GO