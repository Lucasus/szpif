use szpifDatabase
PRINT 'CREATING TasksForPM VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getTasksForPrzelozonyOdrzucone') IS NOT NULL
	DROP PROCEDURE getTasksForPrzelozonyOdrzucone
GO
IF OBJECT_ID('updateTasksForPrzelozonyOdrzucone') IS NOT NULL
	DROP PROCEDURE updateTasksForPrzelozonyOdrzucone
GO
IF OBJECT_ID('insertTasksForPrzelozonyOdrzucone') IS NOT NULL
	DROP PROCEDURE insertTasksForPrzelozonyOdrzucone
GO
IF OBJECT_ID('deleteTasksForPrzelozonyOdrzucone') IS NOT NULL
	DROP PROCEDURE deleteTasksForPrzelozonyOdrzucone
GO

----------Procedura zwracaj�ca widok------------------------------
CREATE PROCEDURE getTasksForPrzelozonyOdrzucone
AS
  declare @login varchar(40);
  select @login = SYSTEM_USER
  SELECT tr.Id
	  ,emp.Login AS 'Imi� Podw�adnego'
      ,pr.ProjectName AS 'Nazwa Projektu'
      ,tr.[TaskName] AS 'Nazwa Zadania'
      ,tr.[MaxHours] AS 'Maksymalna ilo�� godzin'
      ,tr.[StartDate] AS 'Data Rozpocz�cia'
      ,tr.[ExpectedEndDate] As 'Oczekiwana Data Zako�czenia'
      ,tr.[Bonus] AS 'Bonus'
      ,tr.[Status] AS 'Status'
  FROM Tasks AS tr
  inner join Projects AS pr on tr.ProjectId = pr.Id
  inner join Employees AS emp on tr.EmployeeId = emp.Id
  where EmployeeId in (select Id from Employees where SuperiorId in (select Id from Employees where Login = @login)) 
  and tr.Status like('Odrzucone')

--  from Projects pr 
 GO

---------Procedura update'uj�ca rekordy z widoku------------------
CREATE PROCEDURE updateTasksForPrzelozonyOdrzucone
  @Id					int,
  @Status				nvarchar(100)
AS
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))     
GO
---------Procedura dodaj�ca rekord do widoku---------------------
CREATE PROCEDURE insertTasksForPrzelozonyOdrzucone
AS
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))
GO


GO
---------Procedura usuwaj�ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteTasksForPrzelozonyOdrzucone

WITH EXECUTE AS  'szpifadmin'
AS
  --DELETE FROM Tasks where Id = @Id
GO
---------Przypisywanie schemat�w do niestandardowych typ�w danych-------------
--INSERT INTO [ColumnsToTypes] VALUES ('TasksForPM','PM', 'Link', 'PMForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('TasksForPrzelozonyOdrzucone','Status','Task State', null);

GO
---------Nadawanie uprawnie�-------------------------------------
GRANT EXECUTE ON    getTasksForPrzelozonyOdrzucone TO BasicRole
GRANT EXECUTE ON updateTasksForPrzelozonyOdrzucone TO BasicRole
GRANT EXECUTE ON insertTasksForPrzelozonyOdrzucone TO BasicRole
GRANT EXECUTE ON deleteTasksForPrzelozonyOdrzucone TO BasicRole
use szpifDatabase

