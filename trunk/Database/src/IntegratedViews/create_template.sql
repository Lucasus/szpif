use szpifDatabase
PRINT 'CREATING <nazwa widoku> VIEW...'
GO
--nie pami�tam po co to jest, ale kiedy� co� mi kiedy� nie dzia�a�o, bo tej linijki nie by�o.
SET QUOTED_IDENTIFIER ON
GO
--j.w.
SET ANSI_NULLS ON
GO
-----------------------------------Usuwamy poprzednie wersje--------------------------------------
--je�eli zrobimy ctrl+H i zmienimy "NazwaWidoku" na swoj� nazw�, to nic tu ju� nie trzeba zmienia�
IF OBJECT_ID('getNazwaWidoku') IS NOT NULL
	DROP PROCEDURE getNazwaWidoku
GO
IF OBJECT_ID('updateNazwaWidoku') IS NOT NULL
	DROP PROCEDURE updateNazwaWidoku
GO
IF OBJECT_ID('insertNazwaWidoku') IS NOT NULL
	DROP PROCEDURE insertNazwaWidoku
GO
IF OBJECT_ID('deleteNazwaWidoku') IS NOT NULL
	DROP PROCEDURE deleteNazwaWidoku
GO

----------------------------------Procedura zwracaj�ca widok--------------------------------------
--procedura ta powinna zwraca� wszystkie rekordy, kt�re si� p�niej uka�� w skali 1:1 w gridzie.
--nie nalezy zwraca� p�l, kt�re mog� by� tylko update'owane a nie wy�wietlane, jak na przyk�ad
--has�a. Wyj�tkiem jest Id kolumny, kt�re zawsze powinno by� zwracane (i tak jest inaczej formatowane
--w aplikacji)
CREATE PROCEDURE getNazwaWidoku
AS
-- standardowy select na ko�cu procedury musi by�, pami�ta� o sp�jno�ci typ�w i nazw kolumn!!!
-- Przyk�ad:
	SELECT 
		em.Id,	--zawsze najlepiej zwraca� w pierwszej kolumnie Id.  
		em.Login, 
		creds.Name, 
		creds.EMail,
		dbo.EmployeeToXmlLink(em.Przelozony) AS  Przelozony, --przyk�ad pami�tania o sp�jno�ci nazw i typ�w ;)
	    dbo.xmlRoles (em.Id) AS Roles
	FROM NazwaWidoku em
		inner join [Credentials] creds on em.CredentialsId = creds.Id
GO

---------Procedura update'uj�ca rekordy z widoku------------------
--mo�emy tutaj w�r�d parametr�w umie�ci� r�wnie� pola, kt�rych nie zwraca�a procedura "getNazwaWidoku", ale
--kt�re r�wnie� chcemy update'owa�. Wa�ne jest, �e ta procedura nie dostaje na wej�ciu wiersza 
--jakiej� tabeli, tylko wiersz naszego widoku, jej wywo�anie jest w aplikacji ca�kowicie zautomatyzowane.
CREATE PROCEDURE updateNazwaWidoku
  @Id			int,	--wa�ne, zeby wiedzie�, jaki wiersz modyfikujemy ;)
  @Login		nvarchar(40),
  @Name			nvarchar(40),
  @EMail		nvarchar(40),
  @Password		nvarchar(40),
  @Przelozony	xml, 
  @Roles		xml
WITH EXECUTE AS 'szpifadmin' --je�eli do niekt�rych rzeczy nie mamy uprawnie�, mo�emy od biedy doda� tak� linijk� 
							 --na pocz�tku funkcji
AS

	-- przyk�ad wy�uskiwania informacji z pliku Xml
	-- Tutaj wy�usujemy warto�� atrybutu Id w takim pliku Xml:
	-- <Link Id="costam" Text="costam"> </Link>
	declare @przelId int;						
	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
	from @Przelozony.nodes('//Link') AS R(nref))

    update NazwaWidoku set Login = @Login, Przelozony = @przelId  where Id = @Id    
    IF @Password != '' and @Password is not null
    BEGIN
		update NazwaWidoku set Password = @Password  where Id = @Id    
    END
    
    update Credentials set Name = @Name, EMail = @EMail where Id = 
    (select CredentialsId from NazwaWidoku where Id = @Id)
    
    DELETE FROM Roles
	WHERE EmployeeId = @Id

	-- kolejny przyk�ad wykorzystania Xml, tym razem z pliku takiego:
	-- <CheckedListBox> <Item Name="costam" Value="costam"> <Item ...> ... </CheckedListBox>
	-- wybieramy wszystkie warto�ci atrybut�w "Name" z w�z��w o nazwie "Item", kt�rych
	-- atrybut "Value" ma warto�� 1
	INSERT INTO Roles
	SELECT @Id, nref.value('@Name[1]', 'nvarchar(50)') Role
	FROM   @Roles.nodes('//Item') AS R(nref)
	WHERE  nref.value('@Value[1]', 'nvarchar(50)') = 1

    
GO
---------Procedura dodaj�ca rekord do widoku---------------------
--Co tu du�o pisa�, chyba nie ma nic nowego, procedura co ma robi� ka�dy widzi.
CREATE PROCEDURE insertNazwaWidoku
  @Login		nvarchar(40),
  @Name			nvarchar(40),
  @EMail		nvarchar(40),
  @Password		nvarchar(40),
  @Przelozony	xml,
  @Roles		xml
WITH EXECUTE AS  'szpifadmin'
AS


	declare @przelId int;
	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
	from @Przelozony.nodes('//Link') AS R(nref))

	INSERT INTO [Credentials] VALUES (@Name,@EMail);
	declare @newId int;
	SELECT @newId = SCOPE_IDENTITY() 
	INSERT INTO [NazwaWidoku]  VALUES (@newId,@Login,@Password,@przelId);
	SELECT @newId = SCOPE_IDENTITY() 

	INSERT INTO Roles
	SELECT @newId, nref.value('@Name[1]', 'nvarchar(50)') Role
	FROM   @Roles.nodes('//Item') AS R(nref)
	WHERE  nref.value('@Value[1]', 'nvarchar(50)') = 1

GO
---------Procedura usuwaj�ca rekord z widoku--------------------- 
--No tu nie ma �adnej filozofii
CREATE PROCEDURE deleteNazwaWidoku
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  DELETE FROM NazwaWidoku where Id = @Id
GO
---------Przypisywanie schemat�w do niestandardowych typ�w danych-------------
--O co tutaj chodzi?
--Ot� niekt�re pola s� na przyk�ad typu Xml. Problem polega na tym,
--�e te pola typu Xml mog� mie� bardzo r�n� struktur�, a aplikacja tej struktury nie zna,
--a musi j� zna�, je�eli chce automatycznie wygenerowa� r�ne formatki.
--Poniewa� nie bawimy si� w pliki ze schematami XML, po prostu stworzy�em
--tabele z metadanymi dotycz�cymi tych typ�w danych, oraz funkcj� getTypeSchema, za 
--pomoc� kt�rej aplikacja odczytuje sobie odpowiednie schematy. 

--Tak wi�c, je�eli dodajemy sobie do funkcji powy�ej jakie� niestandardowe pole Xml,
--to musimy tutaj doda� mapowanie tego pola na odpowiedni jego schemat, podaj�c po kolei:
--Nazw� widoku, nazw� pola, nazw� niestandardowego typu, oraz mo�emy (czasem musimy) doda�
--jakie� dodatkowe informacje

--Przyk�ady:
--Pole "Przelozony" jest kluczem obcym. Takie klucze obce wygodnie reprezentowa� jako
--niestandardowy typ "Link", dzi�ki kt�remu w aplikacji mo�emy na przyk�ad updateowa� 
--takie pole, otwieraj�c odpowedni� formatk� z wylistowanymi wszystkimi prze�o�onymi itp.
--Dodatkowe informacje, jakie podajemy w ostatnim parametrze, to nazwa widoku zintegrowanego,
--z kt�rego b�dziemy pobiera� list� wszystkich prze�o�onych do selecta.
INSERT INTO [ColumnsToTypes] VALUES ('NazwaWidoku','Przelozony', 'Link', 'PrzelozeniForSelect');
--Pole "Roles" jest typu "CheckedListBox". UWAGA!!! - tutaj nie ma jeszcze refaktoryzacji 
--i elastyczno�ci niestety, w tej chwili wszystkie pola "CheckedListBox" b�d� zawiera� role
--u�ytkownik�w, nale�a�oby to zrefaktoryzowa� jako� przy pomocy pola z dodatkowymi informacjami,
--kt�re teraz jest 'null' 
INSERT INTO [ColumnsToTypes] VALUES ('NazwaWidoku','Roles', 'CheckedListBox', null);
GO
---------Nadawanie uprawnie�-------------------------------------
--Nadajemy uprawnienia odpowiednim grupom odbiorc�w (wszyscy, tylko szef, project managerowie etc)
GRANT EXECUTE ON    getNazwaWidoku TO OwnerRole
GRANT EXECUTE ON updateNazwaWidoku TO OwnerRole
GRANT EXECUTE ON insertNazwaWidoku TO OwnerRole
GRANT EXECUTE ON deleteNazwaWidoku TO OwnerRole
use szpifDatabase

