using MongoDB.Driver;

namespace S2CDS.Api.Infrastruture.Repositories.Campaign
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly IMongoCollection<CampaignEntity> _collection;

        public CampaignRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<CampaignEntity>("campaigns");
        }

        public async Task AddAsync(CampaignEntity entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(Builders<CampaignEntity>.Filter.Eq("Id", id));
        }

        public async Task<IEnumerable<CampaignEntity>> GetAllAsync()
        {
            return await _collection.Find(Builders<CampaignEntity>.Filter.Empty).ToListAsync();
        }

        public async Task<CampaignEntity> GetByIdAsync(string id)
        {
            return await _collection.Find(Builders<CampaignEntity>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, CampaignEntity entity)
        {
            await _collection.ReplaceOneAsync(Builders<CampaignEntity>.Filter.Eq("Id", id), entity);
        }
    }
}
