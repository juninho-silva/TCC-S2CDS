using MongoDB.Bson.Serialization.Attributes;

namespace S2CDS.Api.Infrastructure.Repositories.Organization.Entities
{
    /// <summary>
    /// Contact
    /// </summary>
    public class Contact
    {
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
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [BsonElement("emailAddress")]
        public string EmailAddress { get; set; }
        /// <summary>
        /// Gets or sets the responsible person.
        /// </summary>
        [BsonElement("responsiblePerson")]
        public string ResponsiblePerson { get; set; }
    }
}
