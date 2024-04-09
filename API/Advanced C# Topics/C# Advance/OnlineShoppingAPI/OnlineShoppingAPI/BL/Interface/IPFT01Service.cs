using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.POCO;

namespace OnlineShoppingAPI.BL.Interface
{
    /// <summary>
    /// Interface for service handling operations of <see cref="PFT01"/>.
    /// </summary>
    public interface IPFT01Service
    {
        /// <summary>
        /// Retrieves profit data for each day of current running month.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void GetDayWiseData(out Response response);

        /// <summary>
        /// Retrieves aggregated profit data for each month.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void GetMonthData(out Response response);

        /// <summary>
        /// Retrieves aggregated profit data for each year.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void GetYearData(out Response response);

        /// <summary>
        /// Updates profit information based on the provided product and quantity.
        /// </summary>
        /// <param name="objPRO02">The product object for which profit is being updated.</param>
        /// <param name="quantity">The quantity of product being sold.</param>
        void UpdateProfit(PRO02 objPRO02, int quantity);
    }
}
