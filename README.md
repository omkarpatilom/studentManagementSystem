# studentManagementSystem



**********************************************************Stored Procedudre**********************************************************
#Stored Procedure sql query:

CREATE DEFINER=`root`@`localhost` PROCEDURE `GetEnrollDetailsByFacultyID`(IN FacultyId INT)
BEGIN
    SELECT
        e.EnrollId,
        e.EnrollDate,
        s.RollNo,
        s.UserName,
        s.FullName,
        s.DateOfBirth,
        s.Gender,
        s.PhoneNo AS StudentPhoneNo,
        s.Address AS StudentAddress,
        c.CourseId,
        c.CourseName,
        c.FacultyId AS CourseFacultyId,
        c.Fees,
        c.Description,
        c.StartDate,
        c.EndDate,
        f.FacultyId AS FacultyId,
        f.FacultyName,
        f.Email AS FacultyEmail,
        f.PhoneNo AS FacultyPhoneNo,
        f.Address AS FacultyAddress,
        f.Designation
    FROM
        enrolldetails e
    JOIN
        students s ON e.StudentId = s.RollNo
    JOIN
        courses c ON e.CourseId = c.CourseId
    JOIN
        facultydetails f ON e.FacultyDetailsFacultyId = f.FacultyId
    WHERE
        e.FacultyDetailsFacultyId = FacultyId;
END

**********************************************************************************************************************************************************************************************
