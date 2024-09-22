using System.Text.Json.Serialization;

namespace S2CDS.Api.Dtos.v1.Donor.Data
{
    /// <summary>
    /// Contact View Model
    /// </summary>
    public class ContactDto
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the phone1.
        /// </summary>
        [JsonPropertyName("phone1")]
        public string Phone1 { get; set; }
        /// <summary>
        /// Gets or sets the phone2.
        /// </summary>
        [JsonPropertyName("phone2")]
        public string Phone2 { get; set; }
    }
}
