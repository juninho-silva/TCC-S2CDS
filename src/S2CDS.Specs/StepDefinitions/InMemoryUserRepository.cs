using S2CDS.Api.Infrastructure.Repositories.User;

namespace S2CDS.Specs.StepDefinitions
{
    public class InMemoryUserRepository : IUserRepository
    {
        private List<UserEntity> _users { get; set; } = null!;

        public void Init(List<UserEntity> users)
        {
            _users = users.ToList();
        }

        public async Task AddAsync(UserEntity entity)
        {
            await Task.FromResult(0);
            _users.Add(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await Task.FromResult(0);
            var user = _users.Find(user => user.Id == id);
            if (user is not null)
                _ = _users.Remove(user);
        }

        public async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            return await Task.FromResult(_users);
        }

        public async Task<UserEntity> GetByIdAsync(string id)
        {
            if (_users is null)
                throw new ArgumentException("teste falhou!");

            var user = _users.Find(user => user.Id.Equals(id));

            if (user is null)
                throw new ArgumentException("teste falhou!");

            return await Task.FromResult(user);
        }

        public async Task UpdateAsync(string id, UserEntity entity)
        {
            var user = _users.Find(user => user.Id.Equals(id));
            if (user is null)
                throw new ArgumentException("teste falhou!");
            await Task.FromResult(0);
        }
    }
}
