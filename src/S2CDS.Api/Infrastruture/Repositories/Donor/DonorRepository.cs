using MongoDB.Driver;

namespace S2CDS.Api.Infrastruture.Repositories.Donor
{
    /// <summary>
    /// Donor Repository
    /// </summary>
    /// <seealso cref="S2CDS.Api.Infrastruture.Repositories.Donor.IDonorRepository" />
    /// <seealso cref="IDonorRepository" />
    public class DonorRepository : IDonorRepository
    {
        /// <summary>
        /// The collection
        /// </summary>
        private readonly IMongoCollection<DonorEntity> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DonorRepository"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        public DonorRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<DonorEntity>("donors");
        }

        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task AddAsync(DonorEntity entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(Builders<DonorEntity>.Filter.Eq("Id", id));
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DonorEntity>> GetAllAsync()
        {
            return await _collection.Find(Builders<DonorEntity>.Filter.Empty).ToListAsync();
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<DonorEntity> GetByIdAsync(string id)
        {
            return await _collection.Find(Builders<DonorEntity>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        public async Task UpdateAsync(string id, DonorEntity entity)
        {
            await _collection.ReplaceOneAsync(Builders<DonorEntity>.Filter.Eq("Id", id), entity);
        }
    }
}
