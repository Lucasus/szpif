use szpifDatabase
PRINT 'CREATING Projects VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getProjects') IS NOT NULL
	DROP PROCEDURE getProjects
GO
IF OBJECT_ID('updateProjects') IS NOT NULL
	DROP PROCEDURE updateProjects
GO
IF OBJECT_ID('insertProjects') IS NOT NULL
	DROP PROCEDURE insertProjects
GO
IF OBJECT_ID('deleteProjects') IS NOT NULL
	DROP PROCEDURE deleteProjects
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getProjects
AS
 declare @login varchar(40);
  select @login = SYSTEM_USER
  select	Id, 
		dbo.EmployeeToXmlLink(pr.ManagerId, 'PM', 'PMForSelect') AS  'PM',
		OrderId,
		ProjectStatusId,
		ProjectName,
		MaxHours,
		MaxBudget,
		StartDate,
		ExpectedEndDate
  from Projects pr 
 GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateProjects
  @Id					int,
  @PM					xml,
  @ProjectStatusId		int,  
  @ProjectName			nvarchar(40),
  @MaxHours				int,
  @MaxBudget			int,
  @StartDate			datetime,
  @ExpectedEndDate		datetime
AS
	declare @przelId int;
	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
	from @PM.nodes('//Link') AS R(nref))

    update Projects set ManagerId = @przelId, 
						ProjectStatusId = @ProjectStatusId,
						ProjectName = @ProjectName, 
						MaxHours = @MaxHours,
						MaxBudget = @MaxBudget,
						StartDate = @StartDate,
						ExpectedEndDate = @ExpectedEndDate
						where Id = @Id        
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertProjects
  @Id					int,
  @PM					xml,
  @ProjectStatusId		int,  
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
           ,OrderId
           ,[ProjectStatusId]
           ,[ProjectName]
           ,[MaxHours]
           ,[MaxBudget]
           ,[StartDate]
           ,[ExpectedEndDate])
     VALUES
           (@przelId, 
           null, 
           @ProjectStatusId, 
           @ProjectName, 
           @MaxHours, 
           @MaxBudget, 
           @StartDate, 
           @ExpectedEndDate)
GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteProjects
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  DELETE FROM Projects where Id = @Id
GO
---------Przypisywanie schematów do niestandardowych typów danych-------------
INSERT INTO [ColumnsToTypes] VALUES ('Projects','PM', 'Link', 'PMForSelect');

GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getProjects TO OwnerRole
GRANT EXECUTE ON updateProjects TO OwnerRole
GRANT EXECUTE ON insertProjects TO OwnerRole
GRANT EXECUTE ON deleteProjects TO OwnerRole
use szpifDatabase

