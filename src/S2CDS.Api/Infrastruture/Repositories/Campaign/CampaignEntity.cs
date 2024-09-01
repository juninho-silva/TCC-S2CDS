using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace S2CDS.Api.Infrastruture.Repositories.Campaign
{
    /// <summary>
    /// Campaign Entity
    /// </summary>
    public class CampaignEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [BsonElement("title")]
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [BsonElement("description")]
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the type of the blood.
        /// </summary>
        [BsonElement("bloodType")]
        public string BloodType { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [BsonElement("userId")]
        public string UserId { get; set; }
        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        [BsonElement("createdAt")]
        public DateTime CreatedAt {get; set; }
        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        [BsonElement("updateAt")]
        public DateTime UpdatedAt { get; set; }
    }
}
