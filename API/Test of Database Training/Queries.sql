-- Shows how many user type are there like student, teacher, etc.
SELECT 
	COUNT(E01F02)
FROM
	USE01;

-- Show all Data of Student, Parent, and Teacher Table
SELECT
	*
FROM
	STU01;
    
SELECT
	*
FROM
	PRT01;
    
SELECT
	*
FROM
	TCR01;
    
-- Showing all the gmail id and password of users.
SELECT
	U01F04 AS 'Gmail'
FROM
	STU01
UNION
	SELECT
		T01F04
	FROM
		PRT01
UNION
	SELECT
		R01F04
	FROM
		TCR01;
        
-- Student Parent Relation
SELECT 
	U01.U01F02 AS 'Student Name',
	T01.T01F02 AS 'Parent Name'
FROM 
	STU01 AS U01
JOIN 
	PRT01 AS T01
ON U01.U01F09 = T01.T01F01;

-- ViE01 Data
SELECT 
	* 
FROM 
	VIE01;
    
-- Attendance Report
SELECT
	U01.U01F01 AS 'Student ID',
	CONCAT(U01.U01F02, ' ', U01.U01F03) AS 'U01 Name',
    D01.D01F01 AS 'Attendance Date',
    D01.D01F03 AS 'Present/Absent'
FROM
	STU01 AS U01
JOIN
	ATD01 AS D01
ON 
	U01.U01F01 = D01.D01F02
ORDER BY
	D01.D01F01;

-- '-> Index lookup on student using FK_COURSE (U01F11=\'1\')  (cost=0.70 rows=2) (actual time=0.019..0.022 rows=2 loops=1)\n'        
EXPLAIN ANALYZE SELECT
	*
FROM 
	VIE02
WHERE
	Course = 'Science';
    
-- Aggerate Functions
SELECT
	U01.U01F02 AS 'Student Name',
    COUNT(D01.D01F03) AS 'Present Days'
FROM
	STU01 AS U01
JOIN
	ATD01 AS D01
ON 
	U01.U01F01 = D01.D01F02
WHERE
	D01.D01F03 = 'P'
GROUP BY
	U01.U01F02
HAVING count(D01.D01F03) > 1;

-- Joining of Student, Exam Result, Course, and Subject Table
SELECT
	CONCAT(U01.U01f02, ' ', U01.U01F03) AS 'Student Name',
    U01.U01F07 AS 'Gender',
    U01.U01f08 AS 'Mobile Number',
    R01.R01F03 AS 'Subject Id',
    R01.R01F04 AS 'Obtained Marks',
    R01.R01F05 AS 'Subject Marks'
FROM
	STU01 AS U01
JOIN
	EXR01 AS R01
ON
	U01.U01F01 = R01.R01F02;

SELECT
	B.Student_Name AS 'Student Name',
    B.Gender AS 'Gender',
    B.Mobile_Number AS 'Mobile Number',
    B01.B01f02 AS 'Subject',
    B.Obtained_Marks AS 'Obtained Marks',
    B.Subject_Marks AS 'Subject Marks'
FROM
	SUB01 AS Subject
JOIN
	(SELECT
		CONCAT(U01.U01f02, ' ', U01.U01F03) AS 'Student_Name',
		U01.U01F07 AS 'Gender',
		U01.U01f08 AS 'Mobile_Number',
		R01.R01F03 AS 'Subject_Id',
		R01.R01F04 AS 'Obtained_Marks',
		R01.R01F05 AS 'Subject_Marks'
	FROM
		STU01 AS U01
	JOIN
		EXR01 AS R01
	ON
		U01.U01F01 = R01.R01F02) AS B
ON
	B01.B01F01 = B.Subject_Id;

-- '-> Rows fetched before execution  (cost=0.00..0.00 rows=1) (actual time=0.000..0.000 rows=1 loops=1)\n'
explain analyze SELECT
	U01F02
FROM
	STU01
WHERE
	U01F01 = 1;
    
-- '-> Rows fetched before execution  (cost=0.00..0.00 rows=1) (actual time=0.000..0.000 rows=1 loops=1)\n'
EXPLAIN analyze SELECT
	U01F02
FROM
	STU01
WHERE
	U01F04 = "dp3676991@gmail.com";