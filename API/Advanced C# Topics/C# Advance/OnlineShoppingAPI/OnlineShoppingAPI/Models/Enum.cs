namespace OnlineShoppingAPI.Models
{
    /// <summary>
    /// Enumeration representing different types of operations.
    /// </summary>
    public enum EnmOperation
    {
        /// <summary>
        /// Represents a add operation.
        /// </summary>
        A,

        /// <summary>
        /// Represents a delete operation.
        /// </summary>
        D,

        /// <summary>
        /// Represents a edit operation.
        /// </summary>
        E
    }

    /// <summary>
    /// Enumeration representing the status of a product.
    /// </summary>
    public enum EnmProductStatus
    {
        /// <summary>
        /// Product is out of stock.
        /// </summary>
        OutOfStock = 0,

        /// <summary>
        /// Product is in stock.
        /// </summary>
        InStock = 1
    }
}