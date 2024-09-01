using MongoDB.Bson.Serialization.Attributes;

namespace S2CDS.Api.Infrastruture.Repositories.Donor.Entities
{
    /// <summary>
    /// Contact
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [BsonElement("email")]
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the phone1.
        /// </summary>
        [BsonElement("phone1")]
        public string Phone1 { get; set; }
        /// <summary>
        /// Gets or sets the phone2.
        /// </summary>
        [BsonElement("phone2")]
        public string Phone2 { get; set; }
    }
}
