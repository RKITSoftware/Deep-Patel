-- Joins in SQL

CREATE DATABASE joinPractice;
USE joinPractice;

CREATE TABLE student (
	id INT PRIMARY KEY,
    name VARCHAR(50)
);

INSERT INTO 
	student (id, name)
VALUES
	(101, "adam"),
	(102, "bob"),
	(103, "casey");
    
CREATE TABLE course (
	id INT PRIMARY KEY,
    course VARCHAR(50)
);

INSERT INTO 
	course (id, course)
VALUES
	(102, "english"),
	(105, "math"),
	(103, "science"),
	(107, "computer science");
    
SELECT * FROM student;
SELECT * FROM course;

-- Inner Join
SELECT *
FROM student
INNER JOIN course
ON student.id = course.id;

-- Left Join
SELECT *
FROM student
LEFT JOIN course
ON student.id = course.id;

-- Right Join
SELECT *
FROM student
RIGHT JOIN course
ON student.id = course.id;

-- Full Join
SELECT * FROM student
LEFT JOIN course
ON student.id = course.id
UNION
SELECT * FROM student
RIGHT JOIN course
ON student.id = course.id;

-- Left Exclusive Join
SELECT * FROM student
LEFT JOIN course
ON student.id = course.id
WHERE course.id IS NULL;

-- Right Exclusive Join
SELECT * FROM student
RIGHT JOIN course
ON student.id = course.id
WHERE student.id IS NULL;

SELECT * FROM student
LEFT JOIN course
ON student.id = course.id
WHERE course.id IS NULL
UNION
SELECT * FROM student
RIGHT JOIN course
ON student.id = course.id
WHERE student.id IS NULL;

-- SELF JOIN
CREATE TABLE employee (
	id INT PRIMARY KEY,
    name VARCHAR(50),
    manager_id INT
);

INSERT INTO 
	employee (id, name, manager_id)
VALUES
	(101, "adam", 103),
	(102, "bob", 104),
	(103, "casey", NULL),
	(104, "donald", 103);
    
SELECT b.name, a.name as manager_name
FROM employee as a
JOIN employee as b
ON a.id = b.manager_id;