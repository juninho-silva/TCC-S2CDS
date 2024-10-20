using System.Text.Json.Serialization;

namespace S2CDS.Api.Dtos.v1.Organization.Requests
{
    /// <summary>
    /// Create Organization Request
    /// </summary>
    public class CreateOrganizationRequest
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [JsonPropertyName("address")]
        public AddressRequest Address { get; set; }
        /// <summary>
        /// Gets or sets the contact.
        /// </summary>
        [JsonPropertyName("contact")]
        public ContactRequest Contact { get; set; }
        /// <summary>
        /// Gets or sets the operating hours.
        /// </summary>
        [JsonPropertyName("operatingHours")]
        public OperatingHoursRequest OperatingHours { get; set; }
        /// <summary>
        /// Gets or sets the available blood types.
        /// </summary>
        [JsonPropertyName("availableBloodTypes")]
        public List<string> AvailableBloodTypes { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        [JsonPropertyName("user")]
        public UserOrganizationRequest User { get; set; }
    }
}
