using System.Text.Json.Serialization;

namespace S2CDS.Api.Dtos.v1.Organization.Requests
{
    /// <summary>
    /// Operating Hours Request
    /// </summary>
    public class OperatingHoursRequest
    {
        /// <summary>
        /// Gets or sets the opening time.
        /// </summary>
        [JsonPropertyName("openingTime")]
        public TimeSpan OpeningTime { get; set; }
        /// <summary>
        /// Gets or sets the closing time.
        /// </summary>
        [JsonPropertyName("closingTime")]
        public TimeSpan ClosingTime { get; set; }
        /// <summary>
        /// Gets or sets the days open.
        /// </summary>
        [JsonPropertyName("daysOpen")]
        public List<DayOfWeek> DaysOpen { get; set; }
        /// <summary>
        /// Gets or sets the observation.
        /// </summary>
        [JsonPropertyName("observation")]
        public string Observation { get; set; }
    }
}
