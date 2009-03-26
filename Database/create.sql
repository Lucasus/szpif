Use szpifDatabase

IF Object_ID('Permissions','U') IS NOT NULL 
BEGIN
	delete from [Permissions];
	DROP TABLE [Permissions];
END
IF Object_ID('Employees','U') IS NOT NULL 
BEGIN
	delete from [Employees];
	DROP TABLE [Employees];
END
IF Object_ID('Credentials','U') IS NOT NULL 
BEGIN
	delete from [Credentials];
	DROP TABLE [Credentials];
END

GO
CREATE TABLE [Credentials]
(
		[Id] [int] IDENTITY (1,1) NOT NULL PRIMARY KEY,
		[Name] [nvarchar] (40) NOT NULL,
		[EMail] [nvarchar] (40) NOT NULL
);
	
CREATE TABLE [Employees]
(
		[Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
		[CredentialsId] [int] NOT NULL REFERENCES [Credentials] ([Id]),
		[Login] [nvarchar] (40) NOT NULL,
		[Password] [nvarchar] (40) NOT NULL,
);

CREATE TABLE [Permissions]
(
		[Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
		[EmployeeId] [int] NOT NULL REFERENCES [Employees] ([Id]), 
		[Permission] [nvarchar] (40) NOT NULL
);
/*
--Fragment Kodu odpowiedzialny za przyznawanie uprawnieñ
--Mój pomys³ jest taki: Tworzymy u¿ytkowników o bardzo œciœle wyznaczonych mo¿liwoœciach, Do ka¿dej zak³adki dopisujemy
--stringa który okreœla dla którego profilu przeznaczony jest dany element interface'u.
--Przy wywo³ywaniu metody z gui podawany jest ten string, jako argument i on jest u¿ywany jako u¿ytkownik wywo³uj¹cy
--zapytanie z bazy.
*/

GO
-- Tutaj mamy ka¿dego u¿ytkownika.
EXEC sp_droprolemember 'EveryUser', 'GenericEmployer'
EXEC sp_droprolemember 'EveryUser', 'GenericEmployee'
DROP ROLE EveryUser;
CREATE ROLE EveryUser;
GRANT EXECUTE ON checkPermissions TO EveryUser;
GRANT EXECUTE ON changePassword TO EveryUser;
GRANT EXECUTE ON changeEmail TO EveryUser;

DROP USER GenericEveryUser;
CREATE USER GenericEveryUser WITHOUT LOGIN;

GO
--Tutaj mamy W³aœciciela
EXEC sp_droprolemember 'Employer', 'GenericEmployer'
DROP ROLE Employer;
CREATE ROLE Employer;
GRANT EXECUTE ON changePassword TO Employer;
GRANT SELECT ON EmployeeAdministrationView TO Employer;
GRANT SELECT (Login, Id) ON Employees TO Employer; 

DROP USER GenericEmployer;
CREATE USER GenericEmployer WITHOUT LOGIN;
EXEC sp_addrolemember 'Employer', 'GenericEmployer'
EXEC sp_addrolemember 'EveryUser', 'GenericEmployer' 

GO
--Tutaj mamy szeregowego Pracownika
EXEC sp_droprolemember 'Employee', 'GenericEmployee'
DROP ROLE Employee;
CREATE ROLE Employee;

DROP USER GenericEmployee;
CREATE USER GenericEmployee WITHOUT LOGIN;
EXEC sp_addrolemember 'Employee', 'GenericEmployee'
EXEC sp_addrolemember 'EveryUser', 'GenericEmployee' 
GO

Select * from Employees 
Select * from Permissions
Select * from Credentials
GO
