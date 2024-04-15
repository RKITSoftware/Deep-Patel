using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingAPI.Models.DTO
{
    /// <summary>
    /// DTO for SUP01 model.
    /// </summary>
    public class DTOSUP01
    {
        /// <summary>
        /// Suplier Id
        /// </summary>
        [Required(ErrorMessage = "Supplier id is required for the operation specifying.")]
        [Range(0, int.MaxValue, ErrorMessage = "Id's value needs to be greater than or equal to zero.")]
        [JsonProperty("P01101")]
        public string P01F01 { get; set; }

        /// <summary>
        /// Suplier name
        /// </summary>
        [Required(ErrorMessage = "Please provide supplier name.")]
        [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
        [JsonProperty("P01102")]
        public string P01F02 { get; set; }

        /// <summary>
        /// Suplier Email Id
        /// </summary>
        [Required(ErrorMessage = "Please provide email id.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email address.")]
        [JsonProperty("P01103")]
        public string P01F03 { get; set; }

        /// <summary>
        /// Suplier password
        /// </summary>
        [Required(ErrorMessage = "Plase enter password.")]
        [DataType(DataType.Password, ErrorMessage = "Password is in incorrect form.")]
        [JsonProperty("P01104")]
        public string P01F04 { get; set; }

        /// <summary>
        /// Suplier Mobile Number
        /// </summary>
        [Required(ErrorMessage = "Mobile number is required.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Mobile number is invalid.")]
        [JsonProperty("P01105")]
        public string P01F05 { get; set; }

        /// <summary>
        /// Suplier GST Number
        /// </summary>
        [Required(ErrorMessage = "GST number is required.")]
        [JsonProperty("P01106")]
        public string P01F06 { get; set; }
    }
}