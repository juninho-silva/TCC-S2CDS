using MongoDB.Bson.Serialization.Attributes;

namespace S2CDS.Api.Infrastructure.Repositories.Organization.Entities
{
    /// <summary>
    /// Address
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        [BsonElement("street")]
        public string Street { get; set; }
        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        [BsonElement("number")]
        public string Number { get; set; }
        /// <summary>
        /// Gets or sets the neighborhood.
        /// </summary>
        [BsonElement("neighborhood")]
        public string Neighborhood { get; set; }
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [BsonElement("city")]
        public string City { get; set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        [BsonElement("state")]
        public string State { get; set; }
        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        [BsonElement("zipCode")]
        public string ZipCode { get; set; }
    }
}
