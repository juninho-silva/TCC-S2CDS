﻿using S2CDS.Api.Dtos.v1.Campaign;
using S2CDS.Api.Infrastructure.Repositories.Campaign;

namespace S2CDS.Api.Services.v1.Interfaces
{
    public interface ICampaignService
    {
        Task<bool> Create(CampaignRequest request);
        Task<bool> Update(string id, CampaignRequest request);
        Task<CampaignEntity> GetById(string id);
        Task<List<CampaignEntity>> GetAll();
        Task<(bool isSuccess, string message)> Delete(string id);
    }
}
