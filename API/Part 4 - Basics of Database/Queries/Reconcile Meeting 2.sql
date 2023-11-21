-- RKIT SQL Training
-- Reconcile Meeting 2

CREATE DATABASE IF NOT EXISTS 
	company;
USE 
	company;

-- Student Table 
CREATE TABLE STU01 (
	U01F01 INT PRIMARY KEY AUTO_INCREMENT COMMENT 'Id',
    U01F02 VARCHAR(50) COMMENT 'Student Name',
    U01F03 INT NOT NULL COMMENT 'Student Marks',
    U01F04 VARCHAR(1) COMMENT 'Student Grade',
    U01F05 VARCHAR(20) COMMENT 'Student City'
);

-- Data Entry
INSERT INTO STU01
	(U01F02, U01F03, U01F04, U01F05)
VALUES
	("anil", 78, "C", "Pune"),
	("bhumika", 93, "A", "Mumbai"),
	("chetan", 85, "B", "Mumbai"),
	("dhruv", 96, "A", "Delhi"),
	("emanuel", 12, "F", "Delhi"),
	("farah", 82, "B", "Delhi");
    
INSERT INTO STU01
    (U01F02, U01F03, U01F04, U01F05)
VALUES
	("gaurav", 45, "E", "Pune");

-- Limit

-- Selects first 3 students which marks are greater than 75.
SELECT
	U01F01 AS ID,
    U01F02 AS Name,
    U01F03 AS Mark,
    U01F05 AS City
FROM
	STU01
WHERE 
	U01F03 > 75
LIMIT 
	3;

-- Selects top 3 students which get highest marks
SELECT
	U01F02 AS Name,
    U01F03 AS Mark
FROM 
	STU01
ORDER BY
	Mark DESC
LIMIT 
	3;
    
-- Aggregate Functions

-- MIN()
SELECT
	MIN(U01F03) AS 'Minimum marks'
FROM
	STU01;

-- MAX()
SELECT
	MAX(U01F03) AS 'Maximum marks'
FROM
	STU01;
    
-- AVG()
SELECT
	U01F02 AS 'Name',
	U01F03 AS 'Marks',
    U01F04 AS 'Grade'
FROM
	STU01
WHERE U01F03 > (
	SELECT
		AVG(U01F03) AS 'Average marks'
	FROM
		STU01
	);
    
-- COUNT()
SELECT
	COUNT(U01F01) AS 'Below AVG marks student'
FROM
	STU01
WHERE NOT U01F03 > (
	SELECT
		AVG(U01F03) AS 'Average marks'
	FROM
		STU01
	);

-- Join
CREATE TABLE COU01 (
	-- Course ID
	U01F01 INT PRIMARY KEY AUTO_INCREMENT,
    
    -- Course Name
    U01F02 VARCHAR(20)
);

CREATE TABLE STU02 (
	-- Student ID
	U02F01 INT PRIMARY KEY AUTO_INCREMENT,
    
    -- Student Name
    U02F02 VARCHAR(20),
    
    -- Course Foreign Key
    U02F03 INT,
    FOREIGN KEY (U02F03) REFERENCES COU01(U01F01)
);

-- Data Entry
INSERT INTO COU01
	(U01F02)
VALUES
	('Science'),
    ('Commerce'),
    ('Arts');
    
INSERT INTO STU02
	(U02F02, U02F03)
VALUES
	("anil", 3),
	("bhumika", 1),
	("chetan", 2),
	("dhruv", 2),
	("emanuel", 3),
	("farah", 3);
    
-- Inner join
EXPLAIN ANALYZE
SELECT
	Student.U02F01 AS 'Student Id',
	Student.U02F02 AS 'Student Name',
    Course.U01F02 AS 'Course'
FROM
	STU02 AS Student
INNER JOIN
	COU01 AS Course
ON
	Student.U02F03 = Course.U01F01;
    
/* 
-> Inner hash join (student.U02F03 = course.U01F01)  (cost=2.6 rows=6) (actual time=0.256..0.264 rows=6 loops=1)
     -> Table scan on Student  (cost=0.15 rows=6) (actual time=0.018..0.0231 rows=6 loops=1)
     -> Hash
         -> Table scan on Course  (cos...
*/
    
-- Left Join
SELECT
	Student.U02F01 AS 'Student Id',
	Student.U02F02 AS 'Student Name',
    Course.U01F02 AS 'Course'
FROM
	STU02 AS Student
LEFT JOIN
	COU01 AS Course
ON
	Student.U02F03 = Course.U01F01;
    
-- Right Join
INSERT INTO COU01
	(U01F02)
VALUES
	('Computer Science');

SELECT
	Student.U02F01 AS 'Student Id',
	Student.U02F02 AS 'Student Name',
    Course.U01F02 AS 'Course'
FROM
	STU02 AS Student
RIGHT JOIN
	COU01 AS Course
ON
	Student.U02F03 = Course.U01F01;
    
-- FULL JOIN With UNION
SELECT
	Student.U02F01 AS 'Student Id',
	Student.U02F02 AS 'Student Name',
    Course.U01F02 AS 'Course'
FROM
	STU02 AS Student 
LEFT JOIN COU01 AS Course ON Student.U02F03 = Course.U01F01
UNION
	SELECT
		Student.U02F01 AS 'Student Id',
		Student.U02F02 AS 'Student Name',
		Course.U01F02 AS 'Course'
	FROM
		STU02 AS Student
	RIGHT JOIN
		COU01 AS Course
	ON
		Student.U02F03 = Course.U01F01;
        
-- Views
CREATE VIEW
	VIE01
AS
	SELECT
		U01F01
	FROM
		STU01;
        
SELECT
	*
FROM
	VIE01;
    
DROP VIEW VIE01;