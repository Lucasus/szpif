/*
--Fragment Kodu odpowiedzialny za przyznawanie uprawnie�
--M�j pomys� jest taki: Tworzymy u�ytkownik�w o bardzo �ci�le wyznaczonych mo�liwo�ciach, Do ka�dej zak�adki dopisujemy
--stringa kt�ry okre�la dla kt�rego profilu przeznaczony jest dany element interface'u.
--Przy wywo�ywaniu metody z gui podawany jest ten string, jako argument i on jest u�ywany jako u�ytkownik wywo�uj�cy
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