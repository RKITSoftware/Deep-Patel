-- Write the query to find the avg marks in each city in ascending order
SELECT city, AVG(marks) FROM student GROUP BY city ORDER BY AVG(marks) DESC;

SELECT mode, COUNT(customer) FROM payment GROUP BY mode;

use college;
SELECT grade, COUNT(grade) FROM student GROUP BY grade ORDER BY grade;