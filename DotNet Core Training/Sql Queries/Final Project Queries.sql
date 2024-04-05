-- SP for Inserting Admin and User Details
DELIMITER //

CREATE PROCEDURE InsertAdminWithUser(
    IN p_username VARCHAR(45),
    IN p_email VARCHAR(45),
    IN p_password VARCHAR(45),
    IN p_role VARCHAR(45),
    IN p_first_name VARCHAR(20),
    IN p_last_name VARCHAR(20),
    IN p_date_of_birth DATETIME,
    IN p_gender VARCHAR(1)
)
BEGIN
    DECLARE user_id INT;

    -- Insert user into usr01 table
    INSERT INTO usr01 (R01F02, R01F03, R01F04, R01F05)
    VALUES (p_username, p_email, p_password, p_role);

    -- Get the ID of the inserted user
    SET user_id = LAST_INSERT_ID();

    -- Insert admin into adm01 table
    INSERT INTO adm01 (M01F02, M01F03, M01F04, M01F05, M01F06)
    VALUES (p_first_name, p_last_name, p_date_of_birth, p_gender, user_id);
END //

DELIMITER ;

-- SP for deleting admin details with the user also.
DELIMITER //

CREATE PROCEDURE DeleteAdmin(IN adminId INT)
BEGIN
    DECLARE user_id INT;
    DECLARE adminCount INT;

	-- Checking if Admin exist or not.
    SELECT COUNT(M01F06) INTO adminCount FROM adm01 WHERE M01F01 = adminId;
    
	IF adminCount = 0 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Admin not found';
    ELSE
		-- Retrieve the user ID associated with the admin
		SELECT M01F06 INTO user_id FROM adm01 WHERE M01F01 = adminId;

		-- Delete the admin details
		DELETE FROM adm01 WHERE M01F01 = adminId;

		-- Delete the corresponding user from usr01 if found
		IF user_id IS NOT NULL THEN
			DELETE FROM usr01 WHERE R01F01 = user_id;
		END IF;
	END IF;
END//

DELIMITER ;

-- Creating student and user data specific to that student.
DELIMITER //

CREATE PROCEDURE `Insert_Student_Data` (
    IN first_name VARCHAR(45),
    IN last_name VARCHAR(45),
    IN dob DATETIME,
    IN gender VARCHAR(1),
    IN aadhar_card_details VARCHAR(12),
    IN username VARCHAR(45),
    IN email VARCHAR(45),
    IN password VARCHAR(45),
    IN role VARCHAR(45)
)
BEGIN
    DECLARE user_id INT;

    -- Insert into usr01 table
    INSERT INTO usr01 (`R01F02`, `R01F03`, `R01F04`, `R01F05`)
    VALUES (username, email, password, role);

    -- Get the last inserted user_id
    SET user_id = LAST_INSERT_ID();

    -- Insert into stu01 table
    INSERT INTO stu01 (`U01F02`, `U01F03`, `U01F04`, `U01F05`, `U01F06`, `U01F07`)
    VALUES (first_name, last_name, dob, gender, aadhar_card_details, user_id);
END //

DELIMITER ;

-- SP For Deleting student record.
DELIMITER //

CREATE PROCEDURE DeleteStudent(IN studentId INT)
BEGIN
    DECLARE user_id INT;
    DECLARE studentCount INT;

	-- Checking if Student exist or not.
    SELECT COUNT(U01F01) INTO studentCount FROM stu01 WHERE U01F01 = studentId;
    
	IF studentCount = 0 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Student not found';
    ELSE
		-- Retrieve the user ID associated with the student
		SELECT U01F07 INTO user_id FROM stu01 WHERE U01F01 = studentId;

		-- Delete the admin details
		DELETE FROM stu01 WHERE U01F01 = studentId;

		-- Delete the corresponding user from usr01 if found
		IF user_id IS NOT NULL THEN
			DELETE FROM usr01 WHERE R01F01 = user_id;
		END IF;
	END IF;
END//

DELIMITER ;