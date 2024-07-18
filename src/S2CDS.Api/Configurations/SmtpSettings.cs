namespace S2CDS.Api.Configurations
{
    /// <summary>
    /// SMTP Settings Entity
    /// </summary>
    public class SmtpSettings
    {
        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        public string Server { get; set; }
        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Gets or sets the name of the sender.
        /// </summary>
        public string SenderName { get; set; }
        /// <summary>
        /// Gets or sets the sender email.
        /// </summary>
        public string SenderEmail { get; set; }
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }
    }
}
