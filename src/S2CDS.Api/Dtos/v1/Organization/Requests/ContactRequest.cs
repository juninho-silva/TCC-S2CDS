using System.Text.Json.Serialization;

namespace S2CDS.Api.Dtos.v1.Organization.Requests
{
    /// <summary>
    /// Contact Request
    /// </summary>
    public class ContactRequest
    {
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
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [JsonPropertyName("emailAddress")]
        public string EmailAddress { get; set; }
        /// <summary>
        /// Gets or sets the responsible person.
        /// </summary>
        [JsonPropertyName("responsiblePerson")]
        public string ResponsiblePerson { get; set; }
    }
}
