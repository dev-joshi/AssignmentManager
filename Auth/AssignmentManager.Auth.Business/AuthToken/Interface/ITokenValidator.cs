namespace AssignmentManager.Auth.Business
{
    using System.Collections.Generic;
    using AssignmentManager.Entities;

    /// <summary>
    /// Used to Validate Tokens.
    /// </summary>
    public interface ITokenValidator
    {
        /// <summary>
        /// Validates and gets the roles from the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="roles">Then roles valid for specfic token.</param>
        /// <returns>true if token valid, false otherwise.</returns>
        bool TryValidate(string token, out IEnumerable<Role> roles);
    }
}
