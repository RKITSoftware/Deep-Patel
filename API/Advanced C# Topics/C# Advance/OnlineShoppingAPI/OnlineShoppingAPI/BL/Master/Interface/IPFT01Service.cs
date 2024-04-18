using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.POCO;

namespace OnlineShoppingAPI.BL.Master.Interface
{
    /// <summary>
    /// Interface for service handling operations of <see cref="PFT01"/>.
    /// </summary>
    public interface IPFT01Service
    {
        /// <summary>
        /// Retrieves profit data for each day of current running month.
        /// </summary>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response GetDayWiseData();

        /// <summary>
        /// Retrieves aggregated profit data for each month.
        /// </summary>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response GetMonthData();

        /// <summary>
        /// Retrieves aggregated profit data for each year.
        /// </summary>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response GetYearData();

        /// <summary>
        /// Updates profit information based on the provided product and quantity.
        /// </summary>
        /// <param name="objPRO02">The product object for which profit is being updated.</param>
        /// <param name="quantity">The quantity of product being sold.</param>
        void UpdateProfit(PRO02 objPRO02, int quantity);
    }
}