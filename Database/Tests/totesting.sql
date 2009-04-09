INSERT INTO student
        (id, name, ssn) VALUES (1, 'david', '999-12-1234')
       
        GRANT SELECT (id, name) ON student TO User1
        DENY SELECT (ssn) ON student TO User1    -- DENY SELECT on column SSN
        
        Execute AS USER  = 'User1'  -- IMPERSONATE using "Execute AS"
        SELECT SUSER_NAME(), USER_NAME()

        SELECT id, name FROM student

        SELECT ssn FROM student 

        REVERT  -- undo IMPERSONATE
