-- Part 8
use joinpractice;

SELECT name FROM employee
UNION
SELECT name FROM employee;

use college;

SELECT AVG(marks)
FROM student;

SELECT name, marks
FROM student
WHERE marks > (SELECT AVG(marks) FROM student);

SELECT rollno
FROM student
WHERE rollno % 2 = 0;

SELECT name
FROM student
WHERE rollno IN (
	SELECT rollno 
    FROM student 
    WHERE rollno % 2 = 0);
    
SELECT MAX(marks)
FROM (SELECT * FROM student WHERE city = "Delhi") AS temp;

SELECT (SELECT MAX(marks) FROM student) AS maximum_marks, name
FROM student;

-- Views
CREATE VIEW view1 AS
SELECT rollno, name, grade FROM student;

SELECT * FROM view1;

DROP VIEW view1;