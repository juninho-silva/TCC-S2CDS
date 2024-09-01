using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using S2CDS.Api.Infrastruture.Repositories.Donor.Entities;

namespace S2CDS.Api.Infrastruture.Repositories.Donor
{
    /// <summary>
    /// Donor Entity
    /// </summary>
    public class DonorEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        [BsonElement("fullName")]
        public FullName FullName { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        [BsonElement("userId")]
        public string UserId { get; set; }
        /// <summary>
        /// Gets or sets the contact.
        /// </summary>
        [BsonElement("contact")]
        public Contact Contact { get; set; }
        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        [BsonElement("birthDate")]
        public string BirthDate { get; set; }
        /// <summary>
        /// Gets or sets the type of the blood.
        /// </summary>
        [BsonElement("bloodType")]
        public string BloodType { get; set; }
        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        [BsonElement("gender")]
        public char Gender { get; set; }
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
