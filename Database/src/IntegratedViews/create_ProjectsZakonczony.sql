use szpifDatabase
PRINT 'CREATING Projects VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getProjects') IS NOT NULL
	DROP PROCEDURE getProjectsZakonczony
GO
IF OBJECT_ID('updateProjects') IS NOT NULL
	DROP PROCEDURE updateProjectsZakonczony
GO
IF OBJECT_ID('insertProjects') IS NOT NULL
	DROP PROCEDURE insertProjectsZakonczony
GO
IF OBJECT_ID('deleteProjects') IS NOT NULL
	DROP PROCEDURE deleteProjectsZakonczony
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getProjectsZakonczony
AS
 declare @login varchar(40);
  select @login = SYSTEM_USER
  select	Id, 
		dbo.EmployeeToXmlLink(pr.ManagerId, 'PM', 'PMForSelect') AS  'PM',
		ProjectName,
		MaxHours,
		MaxBudget,
		StartDate,
		ExpectedEndDate,
		Status
  from Projects pr 
  where Status like ('Zakoñczony')
 GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateProjectsZakonczony
  @Id					int,
  @PM					xml,
  @ProjectName			nvarchar(40),
  @MaxHours				int,
  @MaxBudget			int,
  @StartDate			datetime,
  @ExpectedEndDate		datetime,
  @Status				nvarchar(100)
AS
	declare @przelId int;
	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
	from @PM.nodes('//Link') AS R(nref))

    update Projects set ManagerId = @przelId, 
						Status = @Status,
						ProjectName = @ProjectName, 
						MaxHours = @MaxHours,
						MaxBudget = @MaxBudget,
						StartDate = @StartDate,
						ExpectedEndDate = @ExpectedEndDate
						where Id = @Id and Status like ('Zakoñczony')
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertProjectsZakonczony
  @Id					int,
  @PM					xml,
  @ProjectName			nvarchar(40),
  @MaxHours				int,
  @MaxBudget			int,
  @StartDate			datetime,
  @ExpectedEndDate		datetime,
  @Status				nvarchar(100)
AS
	declare @przelId int;
	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
	from @PM.nodes('//Link') AS R(nref))
	
INSERT INTO [szpifDatabase].[dbo].[Projects]
           ([ManagerId]
           ,[Status]
           ,[ProjectName]
           ,[MaxHours]
           ,[MaxBudget]
           ,[StartDate]
           ,[ExpectedEndDate])
     VALUES
           (@przelId, 
           @Status, 
           @ProjectName, 
           @MaxHours, 
           @MaxBudget, 
           @StartDate, 
           @ExpectedEndDate)
GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteProjectsZakonczony
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  DELETE FROM Projects where Id = @Id and Status like ('Zakoñczony')
GO
---------Przypisywanie schematów do niestandardowych typów danych-------------
INSERT INTO [ColumnsToTypes] VALUES ('ProjectsZakonczony','PM', 'Link', 'PMForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('ProjectsZakonczony','Status','Project State', null);

GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getProjectsZakonczony TO OwnerRole
GRANT EXECUTE ON updateProjectsZakonczony TO OwnerRole
GRANT EXECUTE ON insertProjectsZakonczony TO OwnerRole
GRANT EXECUTE ON deleteProjectsZakonczony TO OwnerRole
use szpifDatabase

