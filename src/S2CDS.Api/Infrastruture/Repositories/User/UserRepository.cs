using MongoDB.Driver;

namespace S2CDS.Api.Infrastruture.Repositories.User
{
    /// <summary>
    /// User Repository
    /// </summary>
    /// <seealso cref="S2CDS.Api.Infrastruture.Repositories.User.IUserRepository" />
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// The collection
        /// </summary>
        private readonly IMongoCollection<UserEntity> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        public UserRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<UserEntity>("users");
        }

        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task AddAsync(UserEntity entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(Builders<UserEntity>.Filter.Eq("Id", id));
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            return await _collection.Find(Builders<UserEntity>.Filter.Empty).ToListAsync();
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<UserEntity> GetByIdAsync(string id)
        {
            return await _collection.Find(Builders<UserEntity>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        public async Task UpdateAsync(string id, UserEntity entity)
        {
            await _collection.ReplaceOneAsync(Builders<UserEntity>.Filter.Eq("Id", id), entity);
        }
    }
}
