-- Day 1 Of Working on Project

CREATE DATABASE IF NOT EXISTS
	SQL_FINAL_TEST;
    
USE
	SQL_FINAL_TEST;

-- User Specific Table which containes how many types of users are going to use this app.
CREATE TABLE USE01 (
	E01F01 INT PRIMARY KEY COMMENT 'USER_ID',
    E01F02 VARCHAR(20) NOT NULL COMMENT 'USER_TYPE'
);

-- Inserting data into user type table.
INSERT INTO
	USE01
	(E01F01, E01F02)
VALUES
	(1, 'Student'),
    (2, 'Parent'),
    (3, 'Teacher');
    
-- Shows how many user type are there like student, teacher, etc.
SELECT 
	COUNT(E01F02)
FROM
	USE01;
    
CREATE TABLE PRT01 (
	T01F01 INT PRIMARY KEY AUTO_INCREMENT COMMENT 'Parent_Id',
    T01F02 VARCHAR(20) NOT NULL COMMENT 'First_Name',
    T01F03 VARCHAR(20) NOT NULL COMMENT 'Last_Name',
    T01F04 VARCHAR(40) NOT NULL UNIQUE COMMENT 'Email_ID',
    T01F05 VARCHAR(40) NOT NULL COMMENT 'Password',
	CONSTRAINT LENGTH_CHECK_OF_PWD_PRT CHECK (LENGTH(T01F05) >= 8),

    T01F06 DATE NOT NULL COMMENT 'DOB',
    T01F07 VARCHAR(10) NOT NULL UNIQUE COMMENT 'Mobile_Number',
    CONSTRAINT LENGTH_CHECK CHECK (LENGTH(T01F07) = 10),
    
    T01F08 INT NOT NULL DEFAULT 2 COMMENT 'FK_User_Id',
	FOREIGN KEY (T01F08) REFERENCES USE01(E01F01)
);

INSERT INTO
	PRT01
    (T01F02, T01F03, T01F04, T01F05, T01F06, T01F07)
VALUES
	('Rameshbhai', 'Patel', 'rpatel053@gmail.com', '@Ramesh1234', '1974-02-12', '8866231121');
    
-- Day 2 of Working on Project

-- Inserting Data on Parent Table
INSERT INTO
	PRT01
    (T01F02, T01F03, T01F04, T01F05, T01F06, T01F07)
VALUES
	('Hasmukhbhai', 'Patel', 'hasmukh123@gmail.com', '@Hasmukh1234', '1971-01-01', '9998458045'),
	('Jayeshbhai', 'Oganja', 'jayesh711@gmail.com', '@Jayesh711', '1973-11-07', '8780492926');

-- Creating a Teacher Table
CREATE TABLE TCR01 (
	R01F01 INT PRIMARY KEY AUTO_INCREMENT COMMENT 'Teacher_Id',
    R01F02 VARCHAR(20) NOT NULL COMMENT 'First_Name',
    R01F03 VARCHAR(20) NOT NULL COMMENT 'Last_Name',
    R01F04 VARCHAR(40) NOT NULL UNIQUE COMMENT 'Email_ID',
    R01F05 VARCHAR(40) NOT NULL COMMENT 'Password',
    CONSTRAINT LENGTH_CHECK_OF_PWD_TCR CHECK (LENGTH(R01F05) >= 8),
    R01F06 DATE NOT NULL COMMENT 'DOB',
    
    R01F07 VARCHAR(10) NOT NULL UNIQUE COMMENT 'Mobile_Number',
    CONSTRAINT LENGTH_CHECK_OF_TEACHER CHECK (LENGTH(R01F07) = 10),
    
    R01F08 INT NOT NULL DEFAULT 3 COMMENT 'User_Id',
	FOREIGN KEY (R01F08) REFERENCES USE01(E01F01)
);

-- Inserting data into teacher table
INSERT INTO
	TCR01
    (R01F02, R01F03, R01F04, R01F05, R01F06, R01F07)
VALUES
	('Jhanvi', 'Doshi', 'djhanvi@gmail.com', '@jhanvi12', '1980-10-26', '9825311823'),
	('Nirali', 'Madhak', 'nirali.gec@gmail.com', '@nirali6', '1985-04-14', '9173308953'),
	('Shyam', 'Kotecha', 'shyamkotecha@gmail.com', '@shyam15', '1982-05-17', '9879655821');
    
-- Creating a Student Table
CREATE TABLE STU01 (
	U01F01 INT PRIMARY KEY AUTO_INCREMENT COMMENT 'Student_Id',
    U01F02 VARCHAR(20) NOT NULL COMMENT 'First_Name',
    U01F03 VARCHAR(20) NOT NULL COMMENT 'Last_Name',
    U01F04 VARCHAR(40) NOT NULL UNIQUE COMMENT 'Email_ID',
    U01F05 VARCHAR(40) NOT NULL COMMENT 'Password',
	CONSTRAINT LENGTH_CHECK_OF_PWD_STU CHECK (LENGTH(U01F05) >= 8),

    U01F06 DATE NOT NULL COMMENT 'DOB',
    U01F07 VARCHAR(6) NOT NULL COMMENT 'Gender',
    
    U01F08 VARCHAR(10) NOT NULL UNIQUE COMMENT 'Mobile_Number',
    CONSTRAINT LENGTH_CHECK_OF_STUDENT CHECK (LENGTH(U01F08) = 10),
    
    U01F09 INT NOT NULL COMMENT 'FK_Parent_Id',
	FOREIGN KEY (U01F09) REFERENCES PRT01(T01F01),
    
    U01F10 INT NOT NULL DEFAULT 1 COMMENT 'FK_User_Id',
	FOREIGN KEY (U01F10) REFERENCES USE01(E01F01)
);

-- Adding Student Information
INSERT INTO
	STU01
    (U01F02, U01F03, U01F04, U01F05, U01F06, U01F07, U01F08, U01F09)
VALUES
	('Deep', 'Patel', 'dp3676991@gmail.com', '@Deep2513', '2003-05-25', 'Male', '9909583015', 1),
	('Dhruv', 'Patel', 'hari2509@gmail.com', '@Dhruv2509', '2001-09-25', 'Male', '8140190307', 2),
	('Yash', 'Patel', 'yash2212@gmail.com', '@Yash2212', '2003-12-22', 'Male', '7698376679', 2),
	('Karan', 'Oganja', 'kp10@gmail.com', '@Kjpatel10', '2009-07-10', 'Male', '8000813563', 3),
	('Pooja', 'Oganja', 'pooja17@gmail.com', '@Pooja1211', '2002-11-12', 'Female', '8785452563', 3);
    
-- Creating a Attendance table
CREATE TABLE ATD01(
	D01F01 DATE NOT NULL COMMENT 'Date',
    D01F02 INT NOT NULL COMMENT 'FK_Student_Id',
    FOREIGN KEY (D01F02) REFERENCES STU01(U01F01),
    
    D01F03 VARCHAR(1) NOT NULL COMMENT 'Status'
);

-- Adding Attendance of Students According to Date
INSERT INTO
	ATD01
VALUES
	('2024-01-01', 1, 'P'),
	('2024-01-02', 1, 'P'),
	('2024-01-03', 1, 'P'),
	('2024-01-01', 2, 'P'),
	('2024-01-02', 2, 'P'),
	('2024-01-03', 2, 'A'),
	('2024-01-01', 3, 'P'),
	('2024-01-02', 3, 'A'),
	('2024-01-03', 3, 'P'),
	('2024-01-01', 4, 'A'),
	('2024-01-02', 4, 'P'),
	('2024-01-03', 4, 'P'),
	('2024-01-01', 5, 'A'),
	('2024-01-02', 5, 'A'),
	('2024-01-03', 5, 'P');
    
-- Day 3

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

-- View Containing Student Parent Relation
CREATE VIEW 
	VWS_STU01
AS
	SELECT 
		U01.U01F01 AS 'Student ID',
        CONCAT(U01.U01F02, ' ', U01.U01F03) AS 'Student Name',
        CONCAT(T01.T01F02, ' ', T01.T01F03) AS 'Parent Name',
        U01.U01F08 AS 'Student Mobile Number',
        T01.T01f07 AS 'Parent Mobile Number'
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
    Attendance.D01F01 AS 'Attendance Date',
    Attendance.D01F03 AS 'Present/Absent'
FROM
	STU01 AS U01
JOIN
	ATD01 AS Attendance
ON 
	U01.U01F01 = Attendance.D01F02
ORDER BY
	Attendance.D01F01;
    
-- Creating a Course Table
CREATE TABLE COU01 (
	U01F01 INT PRIMARY KEY COMMENT 'Course Id',
    U01F02 VARCHAR(15) NOT NULL UNIQUE COMMENT 'Course Name'
);

-- Insering course information
INSERT INTO
	COU01
VALUES
	(1, 'Science'),
	(2, 'Commerce'),
	(3, 'Arts');
    
-- Adding Couse Column on Students Table
ALTER TABLE
	STU01
ADD COLUMN
	U01F11 INT NOT NULL DEFAULT 1,
ADD CONSTRAINT FK_COURSE FOREIGN KEY (U01F11) REFERENCES COU01(U01F01);

-- Updating Student Information
UPDATE
	STU01
SET
	U01F11 = 2
WHERE
	U01F01 = 3;

UPDATE
	STU01
SET
	U01F11 = 3
WHERE
	U01F01 BETWEEN 4 AND 5;
    
-- Show the students which are doing science.
-- '-> Index lookup on U01 using FK_COURSE (U01F11=\'1\')  (cost=0.70 rows=2) (actual time=0.053..0.056 rows=2 loops=1)\n'
EXPLAIN ANALYZE SELECT
	U01.U01F01 AS 'Student ID',
	CONCAT(U01.U01F02, ' ', U01.U01F03) AS 'Student Name',
    U01.U01F04 AS 'Email Id',
    U01.U01F06 AS 'Date of Birth',
    U01.U01F07 AS 'Gender',
    Course.U01F02 AS 'Course'
FROM
	STU01 AS U01
JOIN
	COU01 AS Course
ON 
	U01.U01F11 = Course.U01F01
WHERE
	Course.U01F02 = 'Science';
    
-- Show the students which are doing commerce.
SELECT
	U01.U01F01 AS 'Student ID',
	CONCAT(U01.U01F02, ' ', U01.U01F03) AS 'Student Name',
    U01.U01F04 AS 'Email Id',
    U01.U01F06 AS 'Date of Birth',
    U01.U01F07 AS 'Gender',
    Course.U01F02 AS 'Course'
FROM
	STU01 AS U01
JOIN
	COU01 AS Course
ON 
	U01.U01F11 = Course.U01F01
WHERE
	Course.U01F02 = 'Commerce';
    
-- View For Student Course Relation
CREATE VIEW 
	VWS_COU01
AS
	SELECT
		U01.U01F01 AS 'Student ID',
		CONCAT(U01.U01F02, ' ', U01.U01F03) AS 'Student Name',
		U01.U01F04 AS 'Email Id',
		U01.U01F06 AS 'Date of Birth',
		U01.U01F07 AS 'Gender',
		Course.U01F02 AS 'Course'
	FROM
		STU01 AS U01
	JOIN
		COU01 AS Course
	ON 
		Student.U01F11 = Course.U01F01;

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
    COUNT(Attendance.D01F03) AS 'Present Days'
FROM
	STU01 AS U01
JOIN
	ATD01 AS Attendance
ON 
	U01.U01F01 = Attendance.D01F02
WHERE
	Attendance.D01F03 = 'P'
GROUP BY
	U01.U01F02
HAVING count(Attendance.D01F03) > 1;

-- Subject Table
CREATE TABLE SUB01(
	B01F01 INT PRIMARY KEY AUTO_INCREMENT COMMENT 'Subject_Id',
    B01F02 VARCHAR(20) NOT NULL COMMENT 'Subject_Name',
    
    B01F03 INT NOT NULL COMMENT 'FK_Course_Id',
    FOREIGN KEY (B01F03) REFERENCES COU01(U01F01)
);

-- Subjects According to Course
INSERT INTO
	SUB01
    (B01F02, B01F03)
VALUES
	('Physics', 1),
	('Chemistry', 1),
	('Biology', 1),
	('Gujrati', 3),
	('Hindi', 3),
	('Sanskrit', 3),
	('Accounts', 2),
    ('Ecomonics', 2);
    
-- Exam Result Table
CREATE TABLE EXR01(
	R01F01 INT PRIMARY KEY AUTO_INCREMENT COMMENT 'Exam Result Id',
    
    R01F02 INT NOT NULL COMMENT 'FK_Student_ID',
    FOREIGN KEY (R01F02) REFERENCES STU01(U01F01),
    
    R01F03 INT NOT NULL COMMENT 'FK_Subject_ID',
    FOREIGN KEY (R01F03) REFERENCES SUB01(B01F01),
    
    R01F04 INT NOT NULL COMMENT 'Obtained Marks',
    R01F05 INT DEFAULT 100 COMMENT 'Exam Marks'
);

-- Updating Auto_Increment on subjects table
ALTER TABLE
	SUB01
		AUTO_INCREMENT=101;
        
-- Inserting Students Results Subect wise
INSERT INTO
	EXR01
	(R01F02, R01F03, R01F04)
VALUES
	(1, 1, 89),
    (1, 2, 78),
    (1, 3, 84),
    (2, 1, 57),
    (2, 2, 59),
    (2, 3, 50),
    (3, 4, 89),
    (3, 5, 78),
    (3, 6, 91),
    (4, 7, 67),
    (4, 8, 64),
    (5, 7, 63),
    (5, 8, 75);
    
-- Joining of Student, Exam Result, Course, and Subject Table
SELECT
	CONCAT(U01.U01f02, ' ', U01.U01F03) AS 'Student Name',
    U01.U01F07 AS 'Gender',
    U01.U01f08 AS 'Mobile Number',
    ExamResult.R01F03 AS 'Subject Id',
    ExamResult.R01F04 AS 'Obtained Marks',
    ExamResult.R01F05 AS 'Subject Marks'
FROM
	STU01 AS U01
JOIN
	EXR01 AS ExamResult
ON
	U01.U01F01 = ExamResult.R01F02;

SELECT
	B.Student_Name AS 'Student Name',
    B.Gender AS 'Gender',
    B.Mobile_Number AS 'Mobile Number',
    Subject.B01f02 AS 'Subject',
    B.Obtained_Marks AS 'Obtained Marks',
    B.Subject_Marks AS 'Subject Marks'
FROM
	SUB01 AS Subject
JOIN
	(SELECT
		CONCAT(U01.U01f02, ' ', U01.U01F03) AS 'Student_Name',
		U01.U01F07 AS 'Gender',
		U01.U01f08 AS 'Mobile_Number',
		ExamResult.R01F03 AS 'Subject_Id',
		ExamResult.R01F04 AS 'Obtained_Marks',
		ExamResult.R01F05 AS 'Subject_Marks'
	FROM
		STU01 AS U01
	JOIN
		EXR01 AS ExamResult
	ON
		U01.U01F01 = ExamResult.R01F02) AS B
ON
	Subject.B01F01 = B.Subject_Id;
    
CREATE INDEX 
	IDX_EMAIL_STU01
ON 
	STU01(U01F04);
    
SELECT
	U01F02
FROM
	STU01
WHERE
	U01F01 = 1;
    
EXPLAIN SELECT
	U01F02
FROM
	STU01
WHERE
	U01F04 = "dp3676991@gmail.com";