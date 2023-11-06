-- Practice Quiz 3

ALTER TABLE student
CHANGE name full_name VARCHAR(20);

DELETE FROM student
WHERE marks < 80;

ALTER TABLE student
DROP COLUMN grade;

DROP TABLE student;

SELECT * FROM student;