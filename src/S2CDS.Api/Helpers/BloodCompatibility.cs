namespace S2CDS.Api.Helpers
{
    /// <summary>
    /// Blood Compatibility
    /// </summary>
    public static class BloodCompatibility
    {
        /// <summary>
        /// Blood Compatibility Entity
        /// </summary>
        internal class BloodCompatibilityEntity
        {
            public string Type { get; set; }
            public List<string> Receivers { get; set; }
            public List<string> Donors { get; set; }
        }

        private readonly static List<BloodCompatibilityEntity> bloodTypes = new()
        {
            new() { Type = "A+", Donors = new() { "A+", "AB+" }, Receivers = new() { "A+", "A-", "O+", "O-" } },
            new() { Type = "A-", Donors = new() { "A+", "A-", "AB+", "AB-" }, Receivers = new() { "A-", "O-" } },
            new() { Type = "B+", Donors = new() { "B+", "AB+" }, Receivers =  new() { "B+", "B-", "O+", "O-" } },
            new() { Type = "B-", Donors = new() { "B+", "B-", "AB+", "AB-" }, Receivers = new() { "B-", "O-" } },
            new() { Type = "AB+", Donors = new() { "AB+" }, Receivers = new() { "ALL" } },
            new() { Type = "AB-", Donors = new() { "AB+", "AB-" }, Receivers = new() { "A-", "B-", "AB-", "O-" } },
            new() { Type = "O+", Donors = new() { "A+", "B+", "AB+", "O+" }, Receivers = new() { "O+", "O-" } },
            new() { Type = "O-", Donors = new() { "ALL" }, Receivers = new() { "O-" } },
        };

        /// <summary>
        /// Gets the donors.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">not found type</exception>
        public static List<string> GetDonors(string type)
            => bloodTypes.Find(b => b.Type.Equals(type))?.Donors ?? throw new ArgumentException("not found blood type!");

        /// <summary>
        /// Gets the receivers.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">not found type</exception>
        public static List<string> GetReceivers(string type)
            => bloodTypes.Find(b => b.Type.Equals(type))?.Receivers ?? throw new ArgumentException("not found blood type!");
    }
}
