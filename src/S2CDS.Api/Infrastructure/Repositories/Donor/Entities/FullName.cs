using MongoDB.Bson.Serialization.Attributes;

namespace S2CDS.Api.Infrastructure.Repositories.Donor.Entities
{
    /// <summary>
    /// FullName
    /// </summary>
    public class FullName
    {
        /// <summary>
        /// Gets or sets the first.
        /// </summary>
        [BsonElement("first")]
        public string First { get; set; }
        /// <summary>
        /// Gets or sets the last.
        /// </summary>
        [BsonElement("last")]
        public string Last { get; set; }
    }
}
