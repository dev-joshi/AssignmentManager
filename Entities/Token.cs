namespace AssignmentManager.Entities
{
    using System;

    /// <summary>
    /// The Token.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// The Access Token.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// The Refresh Token.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// The Token Expires on.
        /// </summary>
        public DateTime Expires { get; set; }
    }
}
