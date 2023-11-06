-- Learning Tutorial part 1

CREATE DATABASE temp1;
create database temp2;

DROP DATABASE temp1;
DROP DATABASE temp2;

CREATE DATABASE college;
USE college;

CREATE TABLE student (
	id INT PRIMARY KEY,
    name VARCHAR(50),
    age INT NOT NULL
);

INSERT INTO student VALUES (1, "Deep Patel", 20);
INSERT INTO student VALUES (2, "Vishal Gohil", 20);
INSERT INTO student VALUES (3, "Parth Ramoliya", 21);

SELECT * FROM student;