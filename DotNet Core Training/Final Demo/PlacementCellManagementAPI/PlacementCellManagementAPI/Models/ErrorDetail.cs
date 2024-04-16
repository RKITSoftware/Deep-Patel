using Newtonsoft.Json;

namespace PlacementCellManagementAPI.Models
{
    /// <summary>
    /// Represents details of an error response.
    /// </summary>
    public class ErrorDetail
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the message describing the error.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code associated with the error.
        /// </summary>
        public int StatusCode { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Overrides the default ToString method to serialize the ErrorDetail object to JSON format.
        /// </summary>
        /// <returns>A JSON string representing the ErrorDetail object.</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        #endregion Public Methods
    }
}