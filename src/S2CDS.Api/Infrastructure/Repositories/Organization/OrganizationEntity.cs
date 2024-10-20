using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using S2CDS.Api.Infrastructure.Repositories.Organization.Entities;

namespace S2CDS.Api.Infrastructure.Repositories.Organization
{
    /// <summary>
    /// Blood Bank Entity
    /// </summary>
    public class BloodBankEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [BsonElement("address")]
        public Address Address { get; set; }
        /// <summary>
        /// Gets or sets the contact.
        /// </summary>
        [BsonElement("contact")]
        public Contact Contact { get; set; }
        /// <summary>
        /// Gets or sets the operating hours.
        /// </summary>
        [BsonElement("operatingHours")]
        public OperatingHours OperatingHours { get; set; }
        /// <summary>
        /// Gets or sets the available blood types.
        /// </summary>
        [BsonElement("availableBloodTypes")]
        public List<string> AvailableBloodTypes { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        [BsonElement("isActive")]
        public bool IsActive { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        [BsonElement("userId")]
        public string UserId { get; set; }
        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        [BsonElement("updateAt")]
        public DateTime UpdatedAt { get; set; }
    }
}
