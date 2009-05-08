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

----------Procedura zwracaj�ca widok------------------------------
CREATE PROCEDURE getProjects
AS
	SELECT 
		Id, 
		Name
	FROM Projects 
GO

---------Procedura update'uj�ca rekordy z widoku------------------
CREATE PROCEDURE updateProjects
  @Id			int,
  @Name			nvarchar(40)
AS
    update Projects set Name = @Name where Id = @Id        
GO
---------Procedura dodaj�ca rekord do widoku---------------------
CREATE PROCEDURE insertProjects
  @Name			nvarchar(40)
AS
INSERT INTO [Projects] VALUES (@Name);
GO
---------Procedura usuwaj�ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteProjects
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  DELETE FROM Projects where Id = @Id
GO
---------Przypisywanie schemat�w do niestandardowych typ�w danych-------------

GO
---------Nadawanie uprawnie�-------------------------------------
GRANT EXECUTE ON    getProjects TO OwnerRole
GRANT EXECUTE ON updateProjects TO OwnerRole
GRANT EXECUTE ON insertProjects TO OwnerRole
GRANT EXECUTE ON deleteProjects TO OwnerRole
use szpifDatabase

