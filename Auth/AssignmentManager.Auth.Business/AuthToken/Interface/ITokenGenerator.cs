namespace AssignmentManager.Auth.Business
{
    using System;
    using System.Collections.Generic;
    using AssignmentManager.Entities;

    /// <summary>
    /// Used to Generate Tokens.
    /// </summary>
    public interface ITokenGenerator
    {
        /// <summary>
        /// Generates the token for given roles.
        /// </summary>
        /// <param name="roles">The roles.</param>
        /// <returns>
        /// The token and expiration time of the token.
        /// </returns>
        (string, DateTime) GenerateToken(IEnumerable<Role> roles);
    }
}
