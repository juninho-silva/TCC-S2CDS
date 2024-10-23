using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace S2CDS.Api.Dtos.v1.Campaign
{
    /// <summary>
    /// Campaign View Model
    /// </summary>
    public class CampaignRequest
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the type of the blood.
        /// </summary>
        [Required]
        [JsonPropertyName("bloodType")]
        public string BloodType { get; set; }
    }
}
