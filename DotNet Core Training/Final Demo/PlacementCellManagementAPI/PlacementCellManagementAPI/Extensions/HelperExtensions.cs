using Newtonsoft.Json;
using System.Data;

namespace PlacementCellManagementAPI.Extensions
{
    /// <summary>
    /// Extension methods for DataTable.
    /// </summary>
    public static class HelperExtensions
    {
        #region Public Methods

        /// <summary>
        /// Converts a DataTable to its JSON representation.
        /// </summary>
        /// <param name="dataTable">The DataTable to convert.</param>
        /// <returns>A JSON string representing the DataTable.</returns>
        public static string ToJson(this DataTable dataTable) => JsonConvert.SerializeObject(dataTable, Formatting.Indented);

        #endregion
    }
}