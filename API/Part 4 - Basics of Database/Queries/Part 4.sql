-- Part 4

-- Update Operation

USE college;

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

SELECT * FROM student;