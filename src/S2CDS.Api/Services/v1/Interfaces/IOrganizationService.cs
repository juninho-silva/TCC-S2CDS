using S2CDS.Api.Dtos.v1.Organization.Requests;
using S2CDS.Api.Dtos.v1.Organization.Response;

namespace S2CDS.Api.Services.v1.Interfaces
{
    public interface IOrganizationService
    {
        Task<bool> Create(CreateOrganizationRequest request);
        Task<bool> Update(string id, UpdateOrganizationRequest request);
        Task<OrganizationResponse> GetById(string id);
        Task<List<OrganizationResponse>> GetAll();
        Task<bool> Delete(string id);
    }
}
