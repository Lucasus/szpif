use szpifDatabase
PRINT 'CREATING Projects VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getProjects') IS NOT NULL
	DROP PROCEDURE getProjectsNowy
GO
IF OBJECT_ID('updateProjects') IS NOT NULL
	DROP PROCEDURE updateProjectsNowy
GO
IF OBJECT_ID('insertProjects') IS NOT NULL
	DROP PROCEDURE insertProjectsNowy
GO
IF OBJECT_ID('deleteProjects') IS NOT NULL
	DROP PROCEDURE deleteProjectsNowy
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getProjectsNowy
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
  where Status like ('Nowy')
 GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateProjectsNowy
  @Id					int,
  @PM					xml,
  @ProjectName			nvarchar(40),
  @MaxHours				int,
  @MaxBudget			int,
  @StartDate			datetime,
  @ExpectedEndDate		datetime,
  @Status				nvarchar(100)
AS
	INSERT INTO Help values ('dupa')
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))

--    update Projects set ManagerId = @przelId, 
--						Status = @Status,
--						ProjectName = @ProjectName, 
--						MaxHours = @MaxHours,
--						MaxBudget = @MaxBudget,
--						StartDate = @StartDate,
--						ExpectedEndDate = @ExpectedEndDate
--						where Id = @Id and Status like ('Nowy')
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertProjectsNowy
  @Id					int,
  @PM					xml,
  @ProjectName			nvarchar(40),
  @MaxHours				int,
  @MaxBudget			int,
  @StartDate			datetime,
  @ExpectedEndDate		datetime
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
           'Nowy',--@Status, 
           @ProjectName, 
           @MaxHours, 
           @MaxBudget, 
           @StartDate, 
           @ExpectedEndDate)
GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteProjectsNowy
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  DELETE FROM Projects where Id = @Id and Status like ('Nowy')
GO
---------Przypisywanie schematów do niestandardowych typów danych-------------
INSERT INTO [ColumnsToTypes] VALUES ('ProjectsNowy','PM', 'Link', 'PMForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('ProjectsNowy','Status','Project State', null);

GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getProjectsNowy TO OwnerRole
GRANT EXECUTE ON updateProjectsNowy TO OwnerRole
GRANT EXECUTE ON insertProjectsNowy TO OwnerRole
GRANT EXECUTE ON deleteProjectsNowy TO OwnerRole
use szpifDatabase

