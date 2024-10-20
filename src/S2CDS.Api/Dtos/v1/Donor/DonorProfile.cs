using AutoMapper;
using S2CDS.Api.Dtos.v1.Donor.Data;
using S2CDS.Api.Dtos.v1.Donor.Requests;
using S2CDS.Api.Dtos.v1.Donor.Responses;
using S2CDS.Api.Helpers;
using S2CDS.Api.Infrastructure.Repositories.Donor;
using S2CDS.Api.Infrastructure.Repositories.User;
using entity = S2CDS.Api.Infrastructure.Repositories.Donor.Entities;

namespace S2CDS.Api.Dtos.v1.Donor
{
    /// <summary>
    /// Donor Profile
    /// </summary>
    /// <seealso cref="Profile" />
    public class DonorProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DonorProfile"/> class.
        /// </summary>
        public DonorProfile()
        {
            CreateMap<CreateDonorRequest, DonorEntity>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(source => source.BirthDate.ToShortDateString()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(source => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(source => DateTime.UtcNow))
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<Contact, entity.Contact>();
            CreateMap<FullName, entity.FullName>();
            CreateMap<UserDonorRequest, UserEntity>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(source => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(source => DateTime.UtcNow))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(source => PasswordHash.Encrypt(source.Password)));

            CreateMap<DonorEntity, DonorResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(source => $"{source.FullName.First} {source.FullName.Last}"))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(source => $"{source.Contact.Email}"))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(source => $"{source.Contact.Phone1}"));
        }
    }
}
