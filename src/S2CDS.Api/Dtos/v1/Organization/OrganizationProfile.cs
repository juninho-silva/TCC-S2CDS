using AutoMapper;
using S2CDS.Api.Dtos.v1.Organization.Requests;
using S2CDS.Api.Dtos.v1.Organization.Response;
using S2CDS.Api.Helpers;
using S2CDS.Api.Infrastructure.Repositories.Organization;
using S2CDS.Api.Infrastructure.Repositories.Organization.Entities;
using S2CDS.Api.Infrastructure.Repositories.User;

namespace S2CDS.Api.Dtos.v1.Organization
{
    /// <summary>
    /// Organization Profile
    /// </summary>
    /// <seealso cref="Profile" />
    public class OrganizationProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationProfile"/> class.
        /// </summary>
        public OrganizationProfile()
        {
            CreateMap<CreateOrganizationRequest, BloodBankEntity>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(source => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(source => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(source => true))
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<UpdateOrganizationRequest, BloodBankEntity>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(source => DateTime.UtcNow));

            CreateMap<BloodBankEntity, OrganizationResponse>();

            CreateMap<AddressRequest, Address>();
            CreateMap<ContactRequest, Contact>();
            CreateMap<OperatingHoursRequest, OperatingHours>();

            CreateMap<Address, AddressRequest>();
            CreateMap<Contact, ContactRequest>();
            CreateMap<OperatingHours, OperatingHoursRequest>();

            CreateMap<UserOrganizationRequest, UserEntity>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(source => PasswordHash.Encrypt(source.Password)));
        }
    }
}
