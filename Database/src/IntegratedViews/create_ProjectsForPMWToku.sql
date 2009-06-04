use szpifDatabase
PRINT 'CREATING ProjectsForPM VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getProjectsForPMWToku') IS NOT NULL
	DROP PROCEDURE getProjectsForPMWToku
GO
IF OBJECT_ID('updateProjectsForPMWToku') IS NOT NULL
	DROP PROCEDURE updateProjectsForPMWToku
GO
IF OBJECT_ID('insertProjectsForPMWToku') IS NOT NULL
	DROP PROCEDURE insertProjectsForPMWToku
GO
IF OBJECT_ID('deleteProjectsForPMWToku') IS NOT NULL
	DROP PROCEDURE deleteProjectsForPMWToku
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getProjectsForPMWToku
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
  and Status like('W Toku')
 GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateProjectsForPMWToku
  @Id					int,
  @PM					xml,
  @ProjectName			nvarchar(40),
  @MaxHours				int,
  @MaxBudget			int,
  @StartDate			datetime,
  @ExpectedEndDate		datetime,
  @Status				nvarchar(100)
AS
	BEGIN TRY 
	BEGIN TRAN

	declare @przelId int;
	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
	from @PM.nodes('//Link') AS R(nref))

	-- sprawdzanie, ze maksymalna pula godzin nie przekracza maksymalnej sumy godzin wszystkich zadan nalezacych do projektu)
	IF (@MaxHours < (Select SUM(T.MaxHours) from Tasks as T Where T.ProjectId = Id))
		RAISERROR('Blad: Niepoprawna pula godzin', 11, 1)

	-- sprawdzanie budzetu projektu - nie moze byc mniejszy od kosztow
	IF (@MaxBudget < (Select SUM(T.MaxHours * E.RatePerHour + T.Bonus) from Tasks as T
		inner join Employees as E
		on E.id = T.EmployeeId) + @MaxHours * (Select E.RatePerHour from Employees as E where E.id = @przelId))
		RAISERROR('Blad: Niepoprawny budzet', 11, 1)
	-- sprawdzanie dat - data poczatkowa nie moze byc pozniejsza od dat poczatkowych zadan nalezacych do projektu
	IF EXISTS (Select T.StartDate from Tasks as T where T.ProjectId = Id and T.StartDate < @StartDate)  
		RAISERROR('Blad: Niepoprawna data poczatkowa', 11, 1)
	
	-- sprawdzanie dat - daty nie moga byc mniejsze od biezacej
	IF (@StartDate < getDate())
		RAISERROR('Blad: Niepoprawna data poczatkowa', 11, 1)

	IF (@ExpectedEndDate < getDate())
		RAISERROR('Blad: Niepoprawna data poczatkowa', 11, 1)	


	-- sprawdzanie dat - data koncowa nie moze byc wczesniejsza niz najdalsza z dat zadan
	IF EXISTS (Select T.StartDate from Tasks as T where T.ProjectId = Id and T.ExpectedEndDate > @ExpectedEndDate)
		RAISERROR('Blad: Niepoprawna data koncowa', 11, 1)

    update Projects set ManagerId = @przelId, 
						Status = @Status,
						ProjectName = @ProjectName, 
						MaxHours = @MaxHours,
						MaxBudget = @MaxBudget,
						StartDate = @StartDate,
						ExpectedEndDate = @ExpectedEndDate
						where Id = @Id and Status like ('W Toku')

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
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertProjectsForPMWToku
AS
GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteProjectsForPMWToku
AS
GO
---------Przypisywanie schematów do niestandardowych typów danych-------------
INSERT INTO [ColumnsToTypes] VALUES ('ProjectsForPMWToku','PM', 'Link', 'PMForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('ProjectsForPMWToku','Status','Project State', null);


GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getProjectsForPMWToku TO BasicRole
GRANT EXECUTE ON updateProjectsForPMWToku TO BasicRole
GRANT EXECUTE ON insertProjectsForPMWToku TO BasicRole
GRANT EXECUTE ON deleteProjectsForPMWToku TO BasicRole
use szpifDatabase

