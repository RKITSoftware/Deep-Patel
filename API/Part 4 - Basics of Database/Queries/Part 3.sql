-- Part 3

CREATE DATABASE IF NOT EXISTS college;
USE college;

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
SELECT name, marks FROM student;
SELECT DISTINCT city FROM student;

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

-- Aggregate Functions
SELECT MAX(marks) FROM student;
SELECT AVG(marks) FROM student;
SELECT MIN(marks) FROM student;
SELECT SUM(marks) FROM student;

-- Count number of students in the city
SELECT city, COUNT(name) FROM student GROUP BY city;

-- Count number of students in each city where max marks cross 90
SELECT COUNT(name), city FROM student GROUP BY city HAVING MAX(marks) > 90;