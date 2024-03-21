namespace RoutingDemo.Model
{
    /// <summary>
    /// POCO Model for storing a company's employee details
    /// </summary>
    public class EMP01
    {
        /// <summary>
        /// Employee Id
        /// </summary>
        public int P01F01 { get; set; }

        /// <summary>
        /// Employee Name
        /// </summary>
        public string? P01F02 { get; set; }

        /// <summary>
        /// Employee Age
        /// </summary>
        public int P01F03 { get; set; }

        /// <summary>
        /// Company Name
        /// </summary>
        public string? P01F04 { get; set; }
    }
}
