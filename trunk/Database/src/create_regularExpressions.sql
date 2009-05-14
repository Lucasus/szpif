EXEC sp_configure 'clr enabled', 1
RECONFIGURE WITH OVERRIDE

Use szpifDatabase

CREATE ASSEMBLY regX
FROM 'C:\Users\Chieredan\Documents\Visual Studio 2008\Projects\Bazy 2\Database\src\TextFunctions\TextFunctions\bin\Release\TextFunctions.dll'
GO



CREATE Function RegExMatch(@Input NVARCHAR(512),@Pattern NVARCHAR(127))
RETURNS BIT EXTERNAL NAME regX.RegularExpressions.RegExMatch
GO 