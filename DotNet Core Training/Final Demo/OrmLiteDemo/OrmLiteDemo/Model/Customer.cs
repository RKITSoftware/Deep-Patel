using ServiceStack.DataAnnotations;

namespace OrmLiteDemo.Model
{
    /// <summary>
    /// Customer table for storing customer details.
    /// Alias contains name for table in database
    /// </summary>
    [Alias("Customers")]
    public class Customer
    {
        /// <summary>
        /// Customer Id
        /// AutoIncrement attribute updates id one by one.
        /// </summary>
        [AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Customer first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Customer last name
        /// </summary>
        public string LastName { get; set; }
    }
}
