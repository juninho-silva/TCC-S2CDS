using System.Text.Json.Serialization;

namespace S2CDS.Api.Dtos.Donor.Data
{
    /// <summary>
    /// Full Name Data transfer object
    /// </summary>
    public class FullNameDto
    {
        /// <summary>
        /// Gets or sets the first.
        /// </summary>
        [JsonPropertyName("first")]
        public string First { get; set; }
        /// <summary>
        /// Gets or sets the last.
        /// </summary>
        [JsonPropertyName("last")]
        public string Last { get; set; }
    }
}
