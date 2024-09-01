using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace S2CDS.Api.Infrastruture.Repositories.User
{
    /// <summary>
    /// User Entity
    /// </summary>
    public class UserEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [BsonElement("email")]
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [BsonElement("username")]
        public string Username { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [BsonElement("password")]
        public string Password { get; set; }
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
