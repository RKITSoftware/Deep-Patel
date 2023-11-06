-- Part 6

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

SELECT * FROM student;