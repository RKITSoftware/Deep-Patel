using ServiceStack.DataAnnotations;

namespace PlacementCellManagementAPI.Models.POCO
{
    /// <summary>
    /// Represents a job posting.
    /// </summary>
    public class JOB01
    {
        /// <summary>
        /// Gets or sets the Job Id.
        /// </summary>
        [PrimaryKey]
        public int B01F01 { get; set; }

        /// <summary>
        /// Gets or sets the Job Title.
        /// </summary>
        public string? B01F02 { get; set; }

        /// <summary>
        /// Gets or sets the Job Capacity.
        /// </summary>
        public int B01F03 { get; set; }

        /// <summary>
        /// Gets or sets the Starting salary range for the job.
        /// </summary>
        public int B01F04 { get; set; }

        /// <summary>
        /// Gets or sets the Ending salary range for the job.
        /// </summary>
        public int B01F05 { get; set; }

        /// <summary>
        /// Gets or sets the Company Id associated with the job.
        /// </summary>
        public int B01F06 { get; set; }

        /// <summary>
        /// Gets or sets the last date for filling the job form.
        /// </summary>
        public DateTime B01F07 { get; set; }

        /// <summary>
        /// Gets or sets the link for the job.
        /// </summary>
        public string? B01F08 { get; set; }
    }
}
