using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace S2CDS.Api.Infrastruture.Repositories.Campaign
{
    /// <summary>
    /// Campaign Entity
    /// </summary>
    public class CampaignEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        [BsonElement("age")]
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [BsonElement("email")]
        public string Email { get; set; }
    }
}
