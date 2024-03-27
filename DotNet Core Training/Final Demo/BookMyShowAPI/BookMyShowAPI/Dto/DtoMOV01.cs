namespace BookMyShowAPI.Dto
{
    /// <summary>
    /// Data Transfer Object for POCO Model of Movie
    /// </summary>
    public class DtoMOV01
    {
        /// <summary>
        /// Movie Title
        /// </summary>
        public string V01101 { get; set; }

        /// <summary>
        /// Released Date
        /// </summary>
        public DateTime V01102 { get; set; }

        /// <summary>
        /// Duration of Movie
        /// </summary>
        public int V01103 { get; set; }
    }
}
