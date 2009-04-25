use szpifDatabase
PRINT 'DELETE DATA...'

SET NOCOUNT ON

Declare @login varchar(100)
DECLARE @ICURSOR CURSOR;  
SET @ICURSOR = CURSOR FOR select name from sys.server_principals where type IN ('S') and is_disabled = 0
				and name != 'szpifadmin';   
     OPEN @ICURSOR  
     FETCH NEXT FROM @ICURSOR INTO @login
     WHILE (@@FETCH_STATUS = 0)  
     BEGIN  
		Exec('DROP LOGIN ' + @login)

      FETCH NEXT FROM @ICURSOR INTO @login;  
     END  
     CLOSE @ICURSOR  
     DEALLOCATE @ICURSOR;  

DECLARE @ObjectTypes TABLE
(
vType char(2)
,vDescription varchar(50)
,iDropOrder integer
,Remove bit
)

--Items you may want to remove
INSERT @ObjectTypes VALUES ('C' ,'CHECK constraint' ,1 ,1)
INSERT @ObjectTypes VALUES ('D' ,'Default or DEFAULT constraint' ,2 ,1)
INSERT @ObjectTypes VALUES ('F' ,'FOREIGN KEY constraint' ,3 ,1)
INSERT @ObjectTypes VALUES ('K' ,'PRIMARY KEY or UNIQUE constraint' ,4 ,1)
INSERT @ObjectTypes VALUES ('FN','Scalar function' ,5 ,1)
INSERT @ObjectTypes VALUES ('IF','Inlined table-function' ,6 ,1)
INSERT @ObjectTypes VALUES ('P' ,'Stored procedure' ,7 ,1)
INSERT @ObjectTypes VALUES ('R' ,'Rule' ,8 ,1) --These are old probably not used...
INSERT @ObjectTypes VALUES ('RF','Replication filter stored procedure',9 ,1)
INSERT @ObjectTypes VALUES ('TF','Table function' ,10,1)
INSERT @ObjectTypes VALUES ('TR','Trigger' ,11,1)
INSERT @ObjectTypes VALUES ('V' ,'View' ,12,1)
INSERT @ObjectTypes VALUES ('U' ,'User table' ,13,1)

--Items you should NEVER want to remove
INSERT @ObjectTypes VALUES ('L' ,'Log' ,14,0)
INSERT @ObjectTypes VALUES ('X' ,'Extended stored procedure' ,15,0)
INSERT @ObjectTypes VALUES ('S' ,'System table' ,16,0)

SET NOCOUNT OFF

/*** drop (pretty much) everything before rebuilding the database ***/
DECLARE OBJECTS CURSOR FOR
SELECT
Name,Type,vDescription,(select name from sysobjects where a.parent_obj = id)
FROM
SysObjects a
JOIN
@ObjectTypes
ON
Type=vType
and
Remove=1
ORDER BY
iDropOrder ASC

OPEN OBJECTS
DECLARE @name as nvarchar(max)
DECLARE @type as nvarchar(2)
DECLARE @type_desc as nvarchar
DECLARE @parentName as nvarchar(max)
DECLARE @statement as nvarchar(max)
FETCH NEXT FROM OBJECTS INTO @name, @type, @type_desc, @parentName
WHILE @@FETCH_STATUS = 0
BEGIN
SET @statement = ''
IF(@type = 'F') or (@type = 'C') or (@type = 'D') or (@type='F') or (@type='K')
BEGIN
PRINT 'DROPPING FK: ' + @name + ' of type ' + @type + ' (' + @type_desc + ')'
SET @statement = 'ALTER TABLE ' + @parentName + ' DROP CONSTRAINT ' + @name
EXECUTE(@statement)
END
ELSE IF (@type = 'TR')
BEGIN
PRINT 'DROPPING TRIGGER: ' + @name + ' of type ' + @type + ' (' + @type_desc + ')'
SET @statement = 'DROP TRIGGER ' + @name
EXECUTE(@statement)
END
ELSE IF (@type = 'U')
BEGIN
PRINT 'DROPPING TABLE: ' + @name + ' of type ' + @type + ' (' + @type_desc + ')'
SET @statement = 'DROP TABLE ' + @name
EXECUTE(@statement)
END
ELSE IF (@type = 'FN') or (@type='IF') or (@type='TF')
BEGIN
PRINT 'DROPPING FUNCTION: ' + @name + ' of type ' + @type + ' (' + @type_desc + ')'
SET @statement = 'DROP FUNCTION ' + @name
EXECUTE(@statement)
END
ELSE IF (@type = 'P') or (@type='RF')
BEGIN
PRINT 'DROPPING Stored Procedure: ' + @name + ' of type ' + @type + ' (' + @type_desc + ')'
SET @statement = 'DROP PROCEDURE ' + @name
EXECUTE(@statement)
END
ELSE IF (@type = 'V')
BEGIN
PRINT 'DROPPING View: ' + @name + ' of type ' + @type + ' (' + @type_desc + ')'
SET @statement = 'DROP VIEW ' + @name
EXECUTE(@statement)
END
ELSE
PRINT 'Didn''t drop object ' + @name + ' of type ' + @type + ' (' + @type_desc + ')'
FETCH NEXT FROM OBJECTS INTO @name, @type, @type_desc, @parentName
END
CLOSE OBJECTS
DEALLOCATE OBJECTS