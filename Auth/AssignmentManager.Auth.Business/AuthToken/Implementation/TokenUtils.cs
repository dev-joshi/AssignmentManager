namespace AssignmentManager.Auth.Business.AuthToken.Implementation
{
    using System;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// Utility methods for token.
    /// </summary>
    internal class TokenUtils
    {
        /// <summary>
        /// The secret key.
        /// </summary>
        private static readonly string Secret = "ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==";

        /// <summary>
        /// Gets the security key.
        /// </summary>
        /// <value>
        /// The security key.
        /// </value>
        public SecurityKey SecurityKey => new SymmetricSecurityKey(Convert.FromBase64String(Secret));

        /// <summary>
        /// Gets the credentials for signing JWT.
        /// </summary>
        /// <value>
        /// The credsentials for signing.
        /// </value>
        public SigningCredentials Creds => new (
            this.SecurityKey,
            SecurityAlgorithms.HmacSha256Signature);

        /// <summary>
        /// Gets the token expiration time limit.
        /// </summary>
        /// <value>
        /// The expiration limit.
        /// </value>
        public TimeSpan ExpirationLimit => TimeSpan.FromMinutes(30);
    }
}
