using System.Text.Json.Serialization;

namespace S2CDS.Api.Dtos.v1.Authentication
{
    /// <summary>
    /// Authentication Data Transter Object
    /// </summary>
    public class AuthenticationRequest
    {
        /// <summary>
        /// Gets or sets the email or username.
        /// </summary>
        [JsonPropertyName("emailOrUsername")]
        public string EmailOrUsername { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
