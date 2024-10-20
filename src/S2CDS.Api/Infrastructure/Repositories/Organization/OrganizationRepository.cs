using MongoDB.Driver;

namespace S2CDS.Api.Infrastructure.Repositories.Organization
{
    /// <summary>
    /// Organization Repository
    /// </summary>
    /// <seealso cref="IOrganizationRepository" />
    public class OrganizationRepository : IOrganizationRepository
    {
        /// <summary>
        /// The collection
        /// </summary>
        private readonly IMongoCollection<BloodBankEntity> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DonorRepository"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        public OrganizationRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<BloodBankEntity>("bloodbanks");
        }

        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task AddAsync(BloodBankEntity entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(Builders<BloodBankEntity>.Filter.Eq("Id", id));
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BloodBankEntity>> GetAllAsync()
        {
            return await _collection.Find(Builders<BloodBankEntity>.Filter.Empty).ToListAsync();
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<BloodBankEntity> GetByIdAsync(string id)
        {
            return await _collection.Find(Builders<BloodBankEntity>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        public async Task UpdateAsync(string id, BloodBankEntity entity)
        {
            await _collection.ReplaceOneAsync(Builders<BloodBankEntity>.Filter.Eq("Id", id), entity);
        }
    }
}
