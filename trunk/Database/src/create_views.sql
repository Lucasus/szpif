
PRINT 'CREATING VIEWS...'
GO
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.VIEWS 
			WHERE TABLE_NAME = 'EmployeeAdministrationView') 
BEGIN
	DROP VIEW dbo.EmployeeAdministrationView
END
GO

CREATE VIEW dbo.EmployeeAdministrationView
AS
SELECT DISTINCT em.Id, Login, FirstName, EMail,
           dbo.aggregateRolesFunction (em.Id) AS Uprawnienia
FROM Employees em
		inner join [Roles] perm on em.Id = perm.EmployeeId
		inner join [Credentials] creds on em.CredentialsId = creds.Id
GO