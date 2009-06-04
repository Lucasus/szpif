use szpifDatabase
PRINT 'CREATING ProjectsForPM VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getProjectsForPMZakonczony') IS NOT NULL
	DROP PROCEDURE getProjectsForPMZakonczony
GO
IF OBJECT_ID('updateProjectsForPMZakonczony') IS NOT NULL
	DROP PROCEDURE updateProjectsForPMZakonczony
GO
IF OBJECT_ID('insertProjectsForPMZakonczony') IS NOT NULL
	DROP PROCEDURE insertProjectsForPMZakonczony
GO
IF OBJECT_ID('deleteProjectsForPMZakonczony') IS NOT NULL
	DROP PROCEDURE deleteProjectsForPMZakonczony
GO

----------Procedura zwracaj�ca widok------------------------------
CREATE PROCEDURE getProjectsForPMZakonczony
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
  where ManagerId in (select Id from Employees where Login = @login)
  and Status like('Zako�czony')
 GO

---------Procedura update'uj�ca rekordy z widoku------------------
CREATE PROCEDURE updateProjectsForPMZakonczony
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

	--declare @przelId int;
	--select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
	--from @PM.nodes('//Link') AS R(nref))

    update Projects set 
						--ManagerId = @przelId, 
						
--						ProjectName = @ProjectName, 
--						MaxHours = @MaxHours,
--						MaxBudget = @MaxBudget,
--						StartDate = @StartDate,
--						ExpectedEndDate = @ExpectedEndDate,
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
CREATE PROCEDURE insertProjectsForPMZakonczony
AS
GO
---------Procedura usuwaj�ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteProjectsForPMZakonczony
AS
GO
---------Przypisywanie schemat�w do niestandardowych typ�w danych-------------
INSERT INTO [ColumnsToTypes] VALUES ('ProjectsForPMZakonczony','PM', 'Link', 'PMForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('ProjectsForPMZakonczony','Status','Project State', null);

GO
---------Nadawanie uprawnie�-------------------------------------
GRANT EXECUTE ON    getProjectsForPMZakonczony TO BasicRole
GRANT EXECUTE ON updateProjectsForPMZakonczony TO BasicRole
GRANT EXECUTE ON insertProjectsForPMZakonczony TO BasicRole
GRANT EXECUTE ON deleteProjectsForPMZakonczony TO BasicRole
use szpifDatabase

