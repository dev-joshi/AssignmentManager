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
        /// Gets the roles from the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Roles valid for specfic token.</returns>
        IEnumerable<Role> GetRoles(string token);
    }
}
