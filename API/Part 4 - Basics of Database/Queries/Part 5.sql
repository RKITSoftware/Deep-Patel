-- Part 5

USE college;

CREATE TABLE dept (
	id INT PRIMARY KEY,
    name VARCHAR(50)
);

INSERT INTO 
	dept (id, name)
VALUES
	(101, "English"),
	(102, "IT");

SET SQL_SAFE_UPDATES = 0;
    
UPDATE 
	dept
SET 
	id = 103
WHERE 
	id = 102;

DELETE FROM dept
WHERE id = 101;

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