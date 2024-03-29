use szpifDatabase
PRINT 'CREATING Projects VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getProjectsZakonczony') IS NOT NULL
	DROP PROCEDURE getProjectsZakonczony
GO
IF OBJECT_ID('updateProjectsZakonczony') IS NOT NULL
	DROP PROCEDURE updateProjectsZakonczony
GO
IF OBJECT_ID('insertProjectsZakonczony') IS NOT NULL
	DROP PROCEDURE insertProjectsZakonczony
GO
IF OBJECT_ID('deleteProjectsZakonczony') IS NOT NULL
	DROP PROCEDURE deleteProjectsZakonczony
GO

----------Procedura zwracaj�ca widok------------------------------
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
  where Status like ('Zako�czony')
 GO

---------Procedura update'uj�ca rekordy z widoku------------------
CREATE PROCEDURE updateProjectsZakonczony
  @Id					int,
--  @PM					xml,
--  @ProjectName			nvarchar(40),
--  @MaxHours				int,
--  @MaxBudget			int,
--  @StartDate			datetime,
--  @ExpectedEndDate		datetime,
  @Status				nvarchar(100)
AS
	BEGIN TRY 
	BEGIN TRAN
	
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))

    update Projects set 
						--ManagerId = @przelId, 
						
						--ProjectName = @ProjectName, 
						--MaxHours = @MaxHours,
						--MaxBudget = @MaxBudget,
						--StartDate = @StartDate,
						--ExpectedEndDate = @ExpectedEndDate,
						Status = @Status
						where Id = @Id --and Status like ('Zako�czony')

	COMMIT TRAN
	END TRY
	
	BEGIN CATCH
		ROLLBACK TRAN
	
		-- ponowne rzucenie wyjatku - do aplikacji
		DECLARE
			@ErrMsg NVARCHAR(4000),
			@ErrSeverity INT,
			@ErrState INT;

		SELECT	
			@ErrMsg = ERROR_MESSAGE(),
			@ErrSeverity = ERROR_SEVERITY(),
			@ErrState = ERROR_STATE();
		RAISERROR (@ErrMsg,@ErrSeverity,@ErrState)
		
	END CATCH

GO
---------Procedura dodaj�ca rekord do widoku---------------------
CREATE PROCEDURE insertProjectsZakonczony
 -- @Id					int,
 -- @PM					xml,
 -- @ProjectName			nvarchar(40),
 -- @MaxHours				int,
 -- @MaxBudget			int,
 -- @StartDate			datetime,
 -- @ExpectedEndDate		datetime,
 -- @Status				nvarchar(100)
AS
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))
	
--INSERT INTO [szpifDatabase].[dbo].[Projects]
  --         ([ManagerId]
  --       ,[Status]
  --       ,[ProjectName]
  --       ,[MaxHours]
--           ,[MaxBudget]
--           ,[StartDate]
--           ,[ExpectedEndDate])
--     VALUES
--           (@przelId, 
--           @Status, 
--           @ProjectName, 
--           @MaxHours, 
--           @MaxBudget, 
--           @StartDate, 
--           @ExpectedEndDate)
GO
---------Procedura usuwaj�ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteProjectsZakonczony


	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
	BEGIN TRY
	BEGIN TRAN
	DELETE FROM Projects where Id = @Id and Status like ('Zako�czony')

	COMMIT TRAN
	END TRY
	
	BEGIN CATCH
		ROLLBACK TRAN
	
		-- ponowne rzucenie wyjatku - do aplikacji
		DECLARE
			@ErrMsg NVARCHAR(4000),
			@ErrSeverity INT,
			@ErrState INT;

		SELECT	
			@ErrMsg = ERROR_MESSAGE(),
			@ErrSeverity = ERROR_SEVERITY(),
			@ErrState = ERROR_STATE();
		RAISERROR (@ErrMsg,@ErrSeverity,@ErrState)
		
	END CATCH


GO
---------Przypisywanie schemat�w do niestandardowych typ�w danych-------------
INSERT INTO [ColumnsToTypes] VALUES ('ProjectsZakonczony','PM', 'Link', 'PMForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('ProjectsZakonczony','Status','Project State', null);

GO
---------Nadawanie uprawnie�-------------------------------------
GRANT EXECUTE ON    getProjectsZakonczony TO OwnerRole
GRANT EXECUTE ON updateProjectsZakonczony TO OwnerRole
GRANT EXECUTE ON insertProjectsZakonczony TO OwnerRole
GRANT EXECUTE ON deleteProjectsZakonczony TO OwnerRole
use szpifDatabase

