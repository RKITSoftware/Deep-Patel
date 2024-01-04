-- TCL Commands

USE
	company;

SET autocommit = 0;

CREATE TABLE COU02 (
	U02F01 INT PRIMARY KEY AUTO_INCREMENT COMMENT 'ID',
    U02F02 VARCHAR(20) NOT NULL COMMENT 'Course Name'
);

INSERT INTO
	COU02
	(U02F02)
VALUES
	('Science'),
	('Commerce'),
	('Arts');
    
COMMIT;

UPDATE
	COU02
SET
	U02F02 = 'Computer Science'
WHERE
	U02F01 = 1;
    
SAVEPOINT A;

INSERT INTO
	COU02
	(U02F02)
VALUES
	('BCA');
    
SAVEPOINT B;

ROLLBACK to b;

SELECT
	*
FROM
	COU02;
    
-- Index

CREATE INDEX 
	Student_Id_Name
ON
	STU01
    (U01F01, U01F02);
    
ALTER TABLE
	STU01
DROP INDEX
	Student_Id_Name;

-- Backup, Restore & Explain Keyword

EXPLAIN COU02;
EXPLAIN COU02 U02F01;

SELECT @@explain_format;

EXPLAIN
	SELECT
		*
	FROM
		COU02;
        
EXPLAIN ANALYZE
	FORMAT=TREE
	SELECT
		*
	FROM
		STU01;
            
-- -> Table scan on STU01  (cost=1.05 rows=8) (actual time=0.0577..0.0688 rows=8 loops=1)
-- -> Table scan on STU02  (cost=0.85 rows=6) (actual time=0.337..0.349 rows=6 loops=1)

-- '-> Table scan on STU01  (cost=1.05 rows=8) (actual time=0.0616..0.0741 rows=8 loops=1)\n'
