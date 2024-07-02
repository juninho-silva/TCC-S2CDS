namespace S2CDS.Api.Infrastruture.Repositories.Campaign
{
    public interface ICampaignRepository
    {
        Task<IEnumerable<CampaignEntity>> GetAllAsync();
        Task<CampaignEntity> GetByIdAsync(string id);
        Task AddAsync(CampaignEntity entity);
        Task UpdateAsync(string id, CampaignEntity entity);
        Task DeleteAsync(string id);
    }
}
