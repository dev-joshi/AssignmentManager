namespace AssignmentManager.Auth.Business.AuthToken.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using AssignmentManager.DB.Storage.Repositories;
    using AssignmentManager.Entities;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;

    /// <inheritdoc />
    internal class TokenValidator : ITokenValidator
    {
        /// <summary>
        /// The token utils.
        /// </summary>
        private readonly TokenUtils tokenUtils;

        /// <summary>
        /// The role repository.
        /// </summary>
        private readonly IRoleRepository roleRepository;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<TokenValidator> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenValidator" /> class.
        /// </summary>
        /// <param name="tokenUtils">The token utils.</param>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="logger">The logger.</param>
        public TokenValidator(
            TokenUtils tokenUtils,
            IRoleRepository roleRepository,
            ILogger<TokenValidator> logger)
        {
            this.tokenUtils = tokenUtils;
            this.roleRepository = roleRepository;
            this.logger = logger;
        }

        /// <inheritdoc />
        public bool TryValidate(string token, out IEnumerable<Role> roles)
        {
            roles = Enumerable.Empty<Role>();

            try
            {
                roles = this.GetRoles(token);
                return true;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to validate token");
            }

            return false;
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Roles valid for the given token.</returns>
        private IEnumerable<Role> GetRoles(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var validation = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = this.tokenUtils.SecurityKey,
            };

            var claimsPrincipal = handler.ValidateToken(token, validation, out var securityToken);

            var claim = claimsPrincipal.Identity as ClaimsIdentity;

            var rolesIds = claim.Claims
                .Where(x => Regex.Match(x.Type, "Role_\\d+").Success)
                .Select(x => x.Type.Split('_')[1])
                .Select(x => int.TryParse(x, out var val) ? val : 0);

            var result = new List<Role>();

            foreach (var roleId in rolesIds)
            {
                if (roleId > 0)
                {
                    result.Add(this.roleRepository.GetRoleAsync(roleId)
                        .ConfigureAwait(false).GetAwaiter().GetResult());
                }
            }

            return result;
        }
    }
}
