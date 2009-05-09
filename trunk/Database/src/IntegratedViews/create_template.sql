use szpifDatabase
PRINT 'CREATING <nazwa widoku> VIEW...'
GO
--nie pamiêtam po co to jest, ale kiedyœ coœ mi kiedyœ nie dzia³a³o, bo tej linijki nie by³o.
SET QUOTED_IDENTIFIER ON
GO
--j.w.
SET ANSI_NULLS ON
GO
-----------------------------------Usuwamy poprzednie wersje--------------------------------------
--je¿eli zrobimy ctrl+H i zmienimy "NazwaWidoku" na swoj¹ nazwê, to nic tu ju¿ nie trzeba zmieniaæ
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

----------------------------------Procedura zwracaj¹ca widok--------------------------------------
--procedura ta powinna zwracaæ wszystkie rekordy, które siê póŸniej uka¿¹ w skali 1:1 w gridzie.
--nie nalezy zwracaæ pól, które mog¹ byæ tylko update'owane a nie wyœwietlane, jak na przyk³ad
--has³a. Wyj¹tkiem jest Id kolumny, które zawsze powinno byæ zwracane (i tak jest inaczej formatowane
--w aplikacji)
CREATE PROCEDURE getNazwaWidoku
AS
-- standardowy select na koñcu procedury musi byæ, pamiêtaæ o spójnoœci typów i nazw kolumn!!!
-- Przyk³ad:
	SELECT 
		em.Id,	--zawsze najlepiej zwracaæ w pierwszej kolumnie Id.  
		em.Login, 
		creds.Name, 
		creds.EMail,
		dbo.EmployeeToXmlLink(em.Przelozony) AS  Przelozony, --przyk³ad pamiêtania o spójnoœci nazw i typów ;)
	    dbo.xmlRoles (em.Id) AS Roles
	FROM NazwaWidoku em
		inner join [Credentials] creds on em.CredentialsId = creds.Id
GO

---------Procedura update'uj¹ca rekordy z widoku------------------
--mo¿emy tutaj wœród parametrów umieœciæ równie¿ pola, których nie zwraca³a procedura "getNazwaWidoku", ale
--które równie¿ chcemy update'owaæ. Wa¿ne jest, ¿e ta procedura nie dostaje na wejœciu wiersza 
--jakiejœ tabeli, tylko wiersz naszego widoku, jej wywo³anie jest w aplikacji ca³kowicie zautomatyzowane.
CREATE PROCEDURE updateNazwaWidoku
  @Id			int,	--wa¿ne, zeby wiedzieæ, jaki wiersz modyfikujemy ;)
  @Login		nvarchar(40),
  @Name			nvarchar(40),
  @EMail		nvarchar(40),
  @Password		nvarchar(40),
  @Przelozony	xml, 
  @Roles		xml
WITH EXECUTE AS 'szpifadmin' --je¿eli do niektórych rzeczy nie mamy uprawnieñ, mo¿emy od biedy dodaæ tak¹ linijkê 
							 --na pocz¹tku funkcji
AS

	-- przyk³ad wy³uskiwania informacji z pliku Xml
	-- Tutaj wy³usujemy wartoœæ atrybutu Id w takim pliku Xml:
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

	-- kolejny przyk³ad wykorzystania Xml, tym razem z pliku takiego:
	-- <CheckedListBox> <Item Name="costam" Value="costam"> <Item ...> ... </CheckedListBox>
	-- wybieramy wszystkie wartoœci atrybutów "Name" z wêz³ów o nazwie "Item", których
	-- atrybut "Value" ma wartoœæ 1
	INSERT INTO Roles
	SELECT @Id, nref.value('@Name[1]', 'nvarchar(50)') Role
	FROM   @Roles.nodes('//Item') AS R(nref)
	WHERE  nref.value('@Value[1]', 'nvarchar(50)') = 1

    
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
--Co tu du¿o pisaæ, chyba nie ma nic nowego, procedura co ma robiæ ka¿dy widzi.
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
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
--No tu nie ma ¿adnej filozofii
CREATE PROCEDURE deleteNazwaWidoku
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  DELETE FROM NazwaWidoku where Id = @Id
GO
---------Przypisywanie schematów do niestandardowych typów danych-------------
--O co tutaj chodzi?
--Otó¿ niektóre pola s¹ na przyk³ad typu Xml. Problem polega na tym,
--¿e te pola typu Xml mog¹ mieæ bardzo ró¿n¹ strukturê, a aplikacja tej struktury nie zna,
--a musi j¹ znaæ, je¿eli chce automatycznie wygenerowaæ ró¿ne formatki.
--Poniewa¿ nie bawimy siê w pliki ze schematami XML, po prostu stworzy³em
--tabele z metadanymi dotycz¹cymi tych typów danych, oraz funkcjê getTypeSchema, za 
--pomoc¹ której aplikacja odczytuje sobie odpowiednie schematy. 

--Tak wiêc, je¿eli dodajemy sobie do funkcji powy¿ej jakieœ niestandardowe pole Xml,
--to musimy tutaj dodaæ mapowanie tego pola na odpowiedni jego schemat, podaj¹c po kolei:
--Nazwê widoku, nazwê pola, nazwê niestandardowego typu, oraz mo¿emy (czasem musimy) dodaæ
--jakieœ dodatkowe informacje

--Przyk³ady:
--Pole "Przelozony" jest kluczem obcym. Takie klucze obce wygodnie reprezentowaæ jako
--niestandardowy typ "Link", dziêki któremu w aplikacji mo¿emy na przyk³ad updateowaæ 
--takie pole, otwieraj¹c odpowedni¹ formatkê z wylistowanymi wszystkimi prze³o¿onymi itp.
--Dodatkowe informacje, jakie podajemy w ostatnim parametrze, to nazwa widoku zintegrowanego,
--z którego bêdziemy pobieraæ listê wszystkich prze³o¿onych do selecta.
INSERT INTO [ColumnsToTypes] VALUES ('NazwaWidoku','Przelozony', 'Link', 'PrzelozeniForSelect');
--Pole "Roles" jest typu "CheckedListBox". UWAGA!!! - tutaj nie ma jeszcze refaktoryzacji 
--i elastycznoœci niestety, w tej chwili wszystkie pola "CheckedListBox" bêd¹ zawieraæ role
--u¿ytkowników, nale¿a³oby to zrefaktoryzowaæ jakoœ przy pomocy pola z dodatkowymi informacjami,
--które teraz jest 'null' 
INSERT INTO [ColumnsToTypes] VALUES ('NazwaWidoku','Roles', 'CheckedListBox', null);
GO
---------Nadawanie uprawnieñ-------------------------------------
--Nadajemy uprawnienia odpowiednim grupom odbiorców (wszyscy, tylko szef, project managerowie etc)
GRANT EXECUTE ON    getNazwaWidoku TO OwnerRole
GRANT EXECUTE ON updateNazwaWidoku TO OwnerRole
GRANT EXECUTE ON insertNazwaWidoku TO OwnerRole
GRANT EXECUTE ON deleteNazwaWidoku TO OwnerRole
use szpifDatabase

