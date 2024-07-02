using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace S2CDS.Api.Infrastruture.Repositories.Campaign
{
    public class CampaignEntity
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("age")]
        public int Age { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }
    }
}
