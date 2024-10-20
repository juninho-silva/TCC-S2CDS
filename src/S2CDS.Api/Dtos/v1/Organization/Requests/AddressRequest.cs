using System.Text.Json.Serialization;

namespace S2CDS.Api.Dtos.v1.Organization.Requests
{
    /// <summary>
    /// Address Request
    /// </summary>
    public class AddressRequest
    {
        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        [JsonPropertyName("street")]
        public string Street { get; set; }
        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        [JsonPropertyName("number")]
        public string Number { get; set; }
        /// <summary>
        /// Gets or sets the neighborhood.
        /// </summary>
        [JsonPropertyName("neighborhood")]
        public string Neighborhood { get; set; }
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [JsonPropertyName("city")]
        public string City { get; set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        [JsonPropertyName("state")]
        public string State { get; set; }
        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        [JsonPropertyName("zipCode")]
        public string ZipCode { get; set; }
    }
}
