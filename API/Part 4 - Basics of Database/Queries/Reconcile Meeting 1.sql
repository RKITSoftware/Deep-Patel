-- RKIT SQL Training 
-- Reconcile Meeting 1

CREATE DATABASE
	college;
    
USE
	college;

CREATE DATABASE IF NOT EXISTS 
	temp1;
DROP DATABASE IF EXISTS 
	temp1;

SHOW TABLES;

CREATE TABLE student (
	id INT PRIMARY KEY,
    name VARCHAR(50),
    age INT NOT NULL
);

INSERT INTO student
VALUES 
	(1, "Deep Patel", 20),
	(2, "Vishal Gohil", 20),
	(3, "Parth Ramoliya", 21);

DROP TABLE student;

SELECT 
	* 
FROM 
	student;

-- How to add primary key
CREATE TABLE temp2 (
	id INT,
    name VARCHAR(30),
    age TINYINT UNSIGNED,
    city VARCHAR(20),
    PRIMARY KEY (id)
    -- Combination primary key
    -- PRIMARY KEY (id, name)
);

-- Foreign key
CREATE TABLE customer (
	id INT PRIMARY KEY,
    name VARCHAR(20),
    
    -- Default Value
    salary INT DEFAULT 10000
);

INSERT INTO 
	customer (id, name) 
VALUES 
	(1, "Deep Patel");
    
SELECT * FROM customer;

CREATE TABLE temp3 (
	id INT PRIMARY KEY,
    cust_id INT,
    FOREIGN KEY (cust_id) REFERENCES customer(id)
);

-- Constraint
CREATE TABLE city (
	id INT PRIMARY KEY,
    city VARCHAR(20),
    age INT,
    CONSTRAINT age_check CHECK (age >= 18 AND city = "Delhi")
    
    -- Short Way
    -- age INT CHECK (age >= 18)
);

INSERT INTO 
	city (id, city, age) 
VALUES 
	(1, "Delhi", 18);
    
INSERT INTO 
	city (id, city, age) 
VALUES 
	(1, "Vadodara", 16);

CREATE TABLE student (
	rollno INT PRIMARY KEY,
    name VARCHAR(50),
    marks INT NOT NULL,
    grade VARCHAR(1),
    city VARCHAR(20)
);

INSERT INTO student
	(rollno, name, marks, grade, city)
VALUES
	(101, "anil", 78, "C", "Pune"),
	(102, "bhumika", 93, "A", "Mumbai"),
	(103, "chetan", 85, "B", "Mumbai"),
	(104, "dhruv", 96, "A", "Delhi"),
	(105, "emanuel", 12, "F", "Delhi"),
	(106, "farah", 82, "B", "Delhi");

-- Select by Column
SELECT 
	name, 
    marks 
FROM 
	student;
    
SELECT 
	DISTINCT city 
FROM 
	student;

-- Where Clause
SELECT * FROM student WHERE marks >= 80;
SELECT * FROM student WHERE city = "Mumbai";

-- Arithmetic Operator & Comparison Operator
SELECT * FROM student WHERE marks + 10 > 100;

-- AND operator
SELECT * FROM student WHERE marks > 80 AND city = "Delhi";

-- OR Operator
SELECT * FROM student WHERE marks > 90 OR city = "Delhi";

-- BETWEEN Operator
SELECT * FROM student WHERE marks BETWEEN 80 AND 90;

-- IN Operator
SELECT * FROM student WHERE city IN ("Delhi", "Pune");

-- NOT IN Operator
SELECT * FROM student WHERE city NOT IN ("Delhi", "Pune");

-- LIMIT Clause
SELECT * FROM student WHERE marks > 75 LIMIT 3;

-- ORDER BY Clause
SELECT * FROM student ORDER BY marks DESC LIMIT 3;

-- Count number of students in the city
SELECT city, COUNT(name) FROM student GROUP BY city;

-- Count number of students in each city where max marks cross 90
SELECT COUNT(name), city FROM student GROUP BY city HAVING MAX(marks) > 90;

-- Updates Query
SET SQL_SAFE_UPDATES = 0;

UPDATE student 
SET grade = "O" 
WHERE grade = "A";

UPDATE student
SET marks = 92, grade = "O"
WHERE rollno = 105;

UPDATE student
SET marks = marks + 1;

DELETE FROM student WHERE marks < 85;

-- CASCADE
CREATE TABLE dept (
	id INT PRIMARY KEY,
    name VARCHAR(50)
);

INSERT INTO 
	dept (id, name)
VALUES
	(101, "English"),
	(102, "IT");
    
-- Update Record on dept table
UPDATE 
	dept
SET 
	id = 103
WHERE 
	id = 102;

-- Delete Record in dept table
DELETE FROM 
	dept
WHERE 
	id = 101;

SELECT * FROM dept;
    
CREATE TABLE teacher (
	id INT PRIMARY KEY,
    name VARCHAR(50),
    dept_id INT,
    FOREIGN KEY (dept_id) REFERENCES dept(id)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

INSERT INTO 
	teacher (id, name, dept_id)
VALUES
	(101, "Shyam Kotecha", 101),
	(102, "Komal Shah", 101),
	(103, "Priyanka Raval", 102);
    
SELECT * FROM teacher;

-- ALTER 
ALTER TABLE student
ADD COLUMN age INT NOT NULL DEFAULT 19;

ALTER TABLE student
MODIFY age VARCHAR(2);

ALTER TABLE student
CHANGE age stu_age INT;

ALTER TABLE student
DROP COLUMN stu_age;

ALTER TABLE student
RENAME to stu;

-- Truncate delete tables data
TRUNCATE TABLE student;

SELECT 
	* 
FROM 
	student;

-- The MySQL IFNULL() function lets you return an alternative value if an expression is NULL
SELECT 
	ProductName, 
    UnitPrice * (UnitsInStock + IFNULL(UnitsOnOrder, 0))
FROM 
	Products;

-- or we can use the COALESCE() function, like this:
SELECT 
	ProductName, 
    UnitPrice * (UnitsInStock + COALESCE(UnitsOnOrder, 0))
FROM 
	Products;

-- AUTO INCREMENT
CREATE TABLE Persons (
    Personid int NOT NULL AUTO_INCREMENT,
    LastName varchar(255) NOT NULL,
    FirstName varchar(255),
    Age int,
    PRIMARY KEY (Personid)
);

INSERT INTO Persons 
	(LastName, FirstName, Age)
VALUES 
	("Patel", "Deep", 20),
    ("Ramoliya", "Parth", 21);
    
ALTER TABLE 
	Persons 
AUTO_INCREMENT = 100;

INSERT INTO Persons 
	(LastName, FirstName, Age)
VALUES 
	("Dhokiya", "Shyam", 22);

SELECT * FROM Persons;