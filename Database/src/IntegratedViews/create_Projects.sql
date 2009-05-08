use szpifDatabase
PRINT 'CREATING PROJECTS VIEW...'
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
	SELECT 
		Id, 
		Name,
		dbo.EmployeeToXmlLink(pr.PM) AS  'PM'
	FROM Projects pr
GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateProjects
  @Id			int,
  @Name			nvarchar(40),
  @PM			xml
AS
	declare @przelId int;
	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
	from @PM.nodes('//Link') AS R(nref))

    update Projects set Name = @Name, PM = @przelId where Id = @Id        
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertProjects
  @Name			nvarchar(40),
  @PM			xml
AS
	declare @przelId int;
	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
	from @PM.nodes('//Link') AS R(nref))
	
	INSERT INTO [Projects] VALUES (@Name, @przelId);
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

