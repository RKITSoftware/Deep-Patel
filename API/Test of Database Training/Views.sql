-- View Containing Student Parent Relation
CREATE VIEW 
	VWS_STU01
AS
	SELECT 
		U01.U01F01 AS 'Student ID',
        CONCAT(U01.U01F02, ' ', U01.U01F03) AS 'Student Name',
        CONCAT(T01.T01F02, ' ', T01.T01F03) AS 'Parent Name',
        U01.U01F08 AS 'Student Mobile Number',
        T01.T01f07 AS 'Parent Mobile Number'
	FROM 
		STU01 AS U01
	JOIN 
		PRT01 AS T01
	ON U01.U01F09 = T01.T01F01;

-- View For Student Course Relation
CREATE VIEW 
	VWS_COU01
AS
	SELECT
		U01.U01F01 AS 'Student ID',
		CONCAT(U01.U01F02, ' ', U01.U01F03) AS 'Student Name',
		U01.U01F04 AS 'Email Id',
		U01.U01F06 AS 'Date of Birth',
		U01.U01F07 AS 'Gender',
		Course.U01F02 AS 'Course'
	FROM
		STU01 AS U01
	JOIN
		COU01 AS Course
	ON 
		Student.U01F11 = Course.U01F01;
