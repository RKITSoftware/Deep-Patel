-- Part 2

-- Database Related Queries

CREATE DATABASE IF NOT EXISTS college;

CREATE DATABASE IF NOT EXISTS temp1;
DROP DATABASE IF EXISTS temp1;

SHOW DATABASES;

USE college;
SHOW TABLES;

-- Table Related Queries

DROP TABLE student;

CREATE TABLE student (
	id INT PRIMARY KEY,
    name VARCHAR(20)
);

-- Insert
INSERT INTO student VALUES (1, "Deep Patel"), (2, "Vishal Gohil"), (3, "Parth Ramoliya");
INSERT INTO student (name, id) VALUES ("Tirth Shah", 4);

-- Select and view all columns
SELECT * FROM student;

CREATE TABLE temp1 (
	id INT UNIQUE
);

INSERT INTO temp1 VALUES (1), (2);
SELECT * FROM temp1;

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

INSERT INTO customer (id, name) VALUES (1, "Deep Patel");
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

INSERT INTO city (id, city, age) VALUES (1, "Delhi", 18);
INSERT INTO city (id, city, age) VALUES (1, "Vadodara", 16);