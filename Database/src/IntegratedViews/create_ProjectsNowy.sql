use szpifDatabase
PRINT 'CREATING Projects VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getProjectsNowy') IS NOT NULL
	DROP PROCEDURE getProjectsNowy
GO
IF OBJECT_ID('updateProjectsNowy') IS NOT NULL
	DROP PROCEDURE updateProjectsNowy
GO
IF OBJECT_ID('insertProjectsNowy') IS NOT NULL
	DROP PROCEDURE insertProjectsNowy
GO
IF OBJECT_ID('deleteProjectsNowy') IS NOT NULL
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

	BEGIN TRY 
	BEGIN TRAN

	declare @przelId int;
	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
	from @PM.nodes('//Link') AS R(nref))
	
	-- sprawdzanie, ze maksymalna pula godzin nie przekracza maksymalnej sumy godzin wszystkich zadan nalezacych do projektu)
	IF (@MaxHours < (Select SUM(T.MaxHours) from Tasks as T Where T.ProjectId = Id))
		RAISERROR('Blad: Niepoprawna pula godzin', 1, 1)

	-- sprawdzanie budzetu projektu - nie moze byc mniejszy od kosztow
	IF (@MaxBudget < (Select SUM(T.MaxHours * E.RatePerHour + T.Bonus) from Tasks as T
		inner join Employees as E
		on E.id = T.EmployeeId) + @MaxHours * (Select E.RatePerHour from Employees as E where E.id = @przelId))
		RAISERROR('Blad: Niepoprawny budzet', 1, 1)
	-- sprawdzanie dat - data poczatkowa nie moze byc pozniejsza od dat poczatkowych zadan nalezacych do projektu
	IF EXISTS (Select T.StartDate from Tasks as T where T.ProjectId = Id and T.StartDate < @StartDate)  
		RAISERROR('Blad: Niepoprawna data poczatkowa', 1, 1)
	
	-- sprawdzanie dat - data koncowa nie moze byc wczesniejsza niz najdalsza z dat zadan
	IF EXISTS (Select T.StartDate from Tasks as T where T.ProjectId = Id and T.ExpectedEndDate > @ExpectedEndDate)
		RAISERROR('Blad: Niepoprawna data koncowa', 1, 1)

	-- sprawdzanie dat - daty nie moga byc mniejsze od biezacej
	IF (@StartDate < getDate())
		RAISERROR('Blad: Niepoprawna data poczatkowa', 1, 1)

	IF (@ExpectedEndDate < getDate())
		RAISERROR('Blad: Niepoprawna data poczatkowa', 1, 1)	
	

    update Projects set ManagerId = @przelId, 
						Status = @Status,
						ProjectName = @ProjectName, 
						MaxHours = @MaxHours,
						MaxBudget = @MaxBudget,
						StartDate = @StartDate,
						ExpectedEndDate = @ExpectedEndDate
						where Id = @Id and Status like ('Nowy')

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
CREATE PROCEDURE insertProjectsNowy
  @Id					int,
  @PM					xml,
  @ProjectName			nvarchar(40),
  @MaxHours				int,
  @MaxBudget			int,
  @StartDate			datetime,
  @ExpectedEndDate		datetime
AS
	BEGIN TRY 
	BEGIN TRAN

	declare @przelId int;
	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
	from @PM.nodes('//Link') AS R(nref))

	
	-- sprawdzanie, ze maksymalna pula godzin nie przekracza maksymalnej sumy godzin wszystkich zadan nalezacych do projektu)
	IF (@MaxHours < (Select SUM(T.MaxHours) from Tasks as T Where T.ProjectId = Id))
		RAISERROR('Blad: Niepoprawna pula godzin', 1, 1)

	-- sprawdzanie budzetu projektu - nie moze byc mniejszy od kosztow
	IF (@MaxBudget < (Select SUM(T.MaxHours * E.RatePerHour + T.Bonus) from Tasks as T
		inner join Employees as E
		on E.id = T.EmployeeId) + @MaxHours * (Select E.RatePerHour from Employees as E where E.id = @przelId))
		RAISERROR('Blad: Niepoprawny budzet', 1, 1)
	-- sprawdzanie dat - data poczatkowa nie moze byc pozniejsza od dat poczatkowych zadan nalezacych do projektu
	IF EXISTS (Select T.StartDate from Tasks as T where T.ProjectId = Id and T.StartDate < @StartDate)  
		RAISERROR('Blad: Niepoprawna data poczatkowa', 1, 1)
	
	-- sprawdzanie dat - data koncowa nie moze byc wczesniejsza niz najdalsza z dat zadan
	IF EXISTS (Select T.StartDate from Tasks as T where T.ProjectId = Id and T.ExpectedEndDate > @ExpectedEndDate)
		RAISERROR('Blad: Niepoprawna data koncowa', 1, 1)

	-- sprawdzanie dat - daty nie moga byc mniejsze od biezacej
	IF (@StartDate < getDate())
		RAISERROR('Blad: Niepoprawna data poczatkowa', 1, 1)

	IF (@ExpectedEndDate < getDate())
		RAISERROR('Blad: Niepoprawna data poczatkowa', 1, 1)	

	
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

