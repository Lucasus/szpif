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
		[Przelozony] [int] REFERENCES [Employees] ([Id]),
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

CREATE TABLE [Help]
(
		[Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
		[Message] [nvarchar] (40) NOT NULL
);

CREATE TABLE [UserTypes]
(
		[Id] [int] IDENTITY (1,1) NOT NULL PRIMARY KEY,
		[Name] [nvarchar] (40) NOT NULL,
		[TypeSchema] [nvarchar] (1000) NOT NULL
);

CREATE TABLE [ColumnsToTypes]
(
		[Id] [int] IDENTITY (1,1) NOT NULL PRIMARY KEY,
		[ViewName] [nvarchar] (40) NOT NULL,
		[ColumnName] [nvarchar] (40) NOT NULL,
		[TypeName] [nvarchar] (40) NOT NULL
);

GO
--creating types
INSERT INTO [UserTypes] VALUES ('CheckedListBox',
 '<CheckedListBox Name="ColumnName">
  <Item Name="W³aœciciel" Value="0 or 1" />
  <Item Name="Project Manager" Value="0 or 1" />
  <Item Name="Prze³o¿ony" Value="0 or 1" />
  <Item Name="Pracownik" Value="0 or 1" />
  <Item Name="Opiekun handlowy" Value="0 or 1" />
</CheckedListBox>');

INSERT INTO [UserTypes] VALUES ('Link','<Link Name="ColumnName" Id="Id rekordu do ktorego sie odwolujemy"
Text="Tekst wyswietlany na gridzie" />');




GO
alter database szpifDatabase set trustworthy on 

GO