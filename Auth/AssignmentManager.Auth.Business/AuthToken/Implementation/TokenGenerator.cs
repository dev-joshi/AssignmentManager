namespace AssignmentManager.Auth.Business.AuthToken.Implementation
{
    using System.Collections.Generic;
    using System.Linq;
    using AssignmentManager.Entities;

    /// <inheritdoc />
    internal class TokenGenerator : ITokenGenerator
    {
        /// <inheritdoc />
        public string GenerateToken(IEnumerable<Role> roles)
        {
            if (roles != null && roles.Count() > 0)
            {
                return string.Join(",", roles.Select(x => x.Id.ToString()));
            }

            return string.Empty;
        }
    }
}
