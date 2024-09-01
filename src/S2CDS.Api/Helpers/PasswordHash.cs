namespace S2CDS.Api.Helpers
{
    /// <summary>
    /// Password Hash
    /// </summary>
    public static class PasswordHash
    {
        /// <summary>
        /// Encrypts the specified password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static string Encrypt(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// Compares the specified password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        public static bool Compare(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
