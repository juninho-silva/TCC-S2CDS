using Microsoft.EntityFrameworkCore;

namespace S2CDS.Api.Infrastruture.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        // Defina suas entidades como DbSets
        // public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações adicionais de modelos podem ser feitas aqui
        }
    }
}
