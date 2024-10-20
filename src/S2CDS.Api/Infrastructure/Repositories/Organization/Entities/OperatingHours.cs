using MongoDB.Bson.Serialization.Attributes;

namespace S2CDS.Api.Infrastructure.Repositories.Organization.Entities
{
    /// <summary>
    /// Operating Hours
    /// </summary>
    public class OperatingHours
    {
        /// <summary>
        /// Gets or sets the opening time.
        /// </summary>
        [BsonElement("openingTime")]
        public TimeSpan OpeningTime { get; set; }
        /// <summary>
        /// Gets or sets the closing time.
        /// </summary>
        [BsonElement("closingTime")]
        public TimeSpan ClosingTime { get; set; }
        /// <summary>
        /// Gets or sets the days open.
        /// </summary>
        [BsonElement("daysOpen")]
        public List<DayOfWeek> DaysOpen { get; set; }
        /// <summary>
        /// Gets or sets the observation.
        /// </summary>
        [BsonElement("observation")]
        public string Observation { get; set; }
    }
}
