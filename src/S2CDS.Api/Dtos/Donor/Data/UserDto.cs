﻿using System.Text.Json.Serialization;

namespace S2CDS.Api.Dtos.Donor.Data
{
    /// <summary>
    /// Create User Data Transfer Object
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [JsonPropertyName("username")]
        public string Username { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}