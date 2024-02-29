using SchoolManagementAPI.Models;
/// <summary>
/// Represents a combination of student and user information.
/// </summary>
public class STUUSR
{
    /// <summary>
    /// Gets or sets the student information (STU01).
    /// </summary>
    public STU01 U01 { get; set; }

    /// <summary>
    /// Gets or sets the user information (USR01).
    /// </summary>
    public USR01 R01 { get; set; }
}