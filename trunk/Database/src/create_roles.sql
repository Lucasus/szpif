/*
--Fragment Kodu odpowiedzialny za przyznawanie uprawnieñ
--Mój pomys³ jest taki: Tworzymy u¿ytkowników o bardzo œciœle wyznaczonych mo¿liwoœciach, Do ka¿dej zak³adki dopisujemy
--stringa który okreœla dla którego profilu przeznaczony jest dany element interface'u.
--Przy wywo³ywaniu metody z gui podawany jest ten string, jako argument i on jest u¿ywany jako u¿ytkownik wywo³uj¹cy
--zapytanie z bazy.
*/
PRINT 'CREATING ROLES...'
GO

IF NOT EXISTS(SELECT name FROM sysusers WHERE name = 'BasicRole')
	CREATE ROLE BasicRole
GO
IF NOT EXISTS(SELECT name FROM sysusers WHERE name = 'OwnerRole')
	CREATE ROLE OwnerRole
GO

GRANT EXECUTE ON getRolesViewForCurrentUser TO BasicRole
GO