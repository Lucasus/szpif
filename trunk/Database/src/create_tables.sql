Use szpifDatabase
PRINT 'CREATING TABLES...'

IF Object_ID('Roles','U') IS NOT NULL 
BEGIN
	delete from [Roles];
	DROP TABLE [Roles];
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

CREATE TABLE [Roles]
(
		[Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
		[EmployeeId] [int] NOT NULL REFERENCES [Employees] ([Id]), 
		[Role] [nvarchar] (40) NOT NULL
);

CREATE TABLE [RoleNames]
(
		[Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
		[RoleName] [nvarchar] (40) NOT NULL
);

GO
