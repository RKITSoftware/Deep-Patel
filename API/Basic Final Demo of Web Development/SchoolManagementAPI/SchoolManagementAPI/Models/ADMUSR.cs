using SchoolManagementAPI.Models;
/// <summary>
/// Represents a combination of administrative and user information.
/// </summary>
public class ADMUSR
{
    /// <summary>
    /// Gets or sets the administrative information (ADM01).
    /// </summary>
    public ADM01 M01 { get; set; }

    /// <summary>
    /// Gets or sets the user information (USR01).
    /// </summary>
    public USR01 R01 { get; set; }
}
