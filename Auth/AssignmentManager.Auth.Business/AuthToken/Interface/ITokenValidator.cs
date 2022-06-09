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
        /// Validates and gets the details from a given token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="roles">The roles inside given token.</param>
        /// <param name="userId">The user id inside given token.</param>
        /// <param name="serviceId">The service id inside given token.</param>
        /// <param name="userName">The user name inside given token.</param>
        /// <param name="serviceName">The service name inside given token.</param>
        /// <returns>true if token valid, false otherwise.</returns>
        bool TryValidate(string token, out IEnumerable<Roles> roles, out int userId, out int serviceId, out string userName, out string serviceName);

        /// <summary>
        /// Checks if the token is valid for given role and belongs to given user.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="userName">The user name.</param>
        /// <param name="role">The role.</param>
        /// <returns>true if token valid, false otherwise.</returns>
        bool TryValidateForUserRole(string token, string userName, Roles role);

        /// <summary>
        /// Checks if the token is valid for given role and belongs to given service.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="serviceName">The service name.</param>
        /// <param name="role">The role.</param>
        /// <returns>true if token valid, false otherwise.</returns>
        bool TryValidateForServiceRole(string token, string serviceName, Roles role);
    }
}
