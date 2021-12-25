namespace AssignmentManager.Auth.Business.AuthToken.Interface
{
    using System;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// Auth Token Utilities.
    /// </summary>
    public interface ITokenUtils
    {
        /// <summary>
        /// Gets the signing credentials.
        /// </summary>
        /// <returns>Signing credenatils.</returns>
        SigningCredentials GetSigningCredentials();

        /// <summary>
        /// Gets the security key.
        /// </summary>
        /// <returns>Security key.</returns>
        SecurityKey GetSecurityKey();

        /// <summary>
        /// Gets the expiration limit for a token.
        /// </summary>
        /// <returns>expiration limit.</returns>
        TimeSpan GetExpirationLimit();
    }
}
