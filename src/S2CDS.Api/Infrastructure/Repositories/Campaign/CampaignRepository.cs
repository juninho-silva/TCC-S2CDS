using MongoDB.Driver;

namespace S2CDS.Api.Infrastructure.Repositories.Campaign
{
    /// <summary>
    /// Campaign Repository
    /// </summary>
    /// <seealso cref="S2CDS.Api.Infrastructure.Repositories.Campaign.ICampaignRepository" />
    public class CampaignRepository : ICampaignRepository
    {
        /// <summary>
        /// The collection
        /// </summary>
        private readonly IMongoCollection<CampaignEntity> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="CampaignRepository"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        public CampaignRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<CampaignEntity>("campaigns");
        }

        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task AddAsync(CampaignEntity entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(Builders<CampaignEntity>.Filter.Eq("Id", id));
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CampaignEntity>> GetAllAsync()
        {
            return await _collection.Find(Builders<CampaignEntity>.Filter.Empty).ToListAsync();
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<CampaignEntity> GetByIdAsync(string id)
        {
            return await _collection.Find(Builders<CampaignEntity>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        public async Task UpdateAsync(string id, CampaignEntity entity)
        {
            await _collection.ReplaceOneAsync(Builders<CampaignEntity>.Filter.Eq("Id", id), entity);
        }
    }
}
