using S2CDS.Api.Dtos.v1.Donor.Requests;
using S2CDS.Api.Dtos.v1.Donor.Responses;

namespace S2CDS.Api.Services.v1.Interfaces
{
    public interface IDonorService
    {
        Task<bool> Create(CreateDonorRequest request);
        Task<bool> Update(string id, CreateDonorRequest request);
        Task<DonorResponse> GetById(string id);
        Task<List<DonorResponse>> GetAll();
        Task<(bool isSuccess, string message)> Delete(string id);
    }
}
