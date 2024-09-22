using System.Text.Json.Serialization;
using S2CDS.Api.Dtos.v1.Donor.Data;

namespace S2CDS.Api.Dtos.v1.Donor
{
    /// <summary>
    /// Create User Donor Data
    /// </summary>
    public class CreateDonorDto
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        [JsonPropertyName("user")]
        public UserDto User { get; set; }
        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        [JsonPropertyName("fullName")]
        public FullNameDto FullName { get; set; }
        /// <summary>
        /// Gets or sets the contact.
        /// </summary>
        [JsonPropertyName("contact")]
        public ContactDto Contact { get; set; }
        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        [JsonPropertyName("birthDate")]
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        [JsonPropertyName("gender")]
        public char Gender { get; set; }
        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        [JsonPropertyName("bloodType")]
        public string BloodType { get; set; }
    }
}
