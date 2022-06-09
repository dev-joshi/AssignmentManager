namespace AssignmentManager.Auth.Business.AuthToken.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using AssignmentManager.Auth.Business.AuthToken.Interface;
    using AssignmentManager.Entities;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;

    /// <inheritdoc />
    internal class TokenValidator : ITokenValidator
    {
        /// <summary>
        /// The token utils.
        /// </summary>
        private readonly ITokenUtils tokenUtils;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<TokenValidator> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenValidator" /> class.
        /// </summary>
        /// <param name="tokenUtils">The token utils.</param>
        /// <param name="logger">The logger.</param>
        public TokenValidator(
            ITokenUtils tokenUtils,
            ILogger<TokenValidator> logger)
        {
            this.tokenUtils = tokenUtils;
            this.logger = logger;
        }

        /// <inheritdoc />
        public bool TryValidate(string token, out IEnumerable<Roles> roles, out int userId, out int serviceId, out string userName, out string serviceName)
        {
            roles = Enumerable.Empty<Roles>();
            userId = 0;
            serviceId = 0;
            userName = string.Empty;
            serviceName = string.Empty;

            try
            {
                var validation = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = this.tokenUtils.GetSecurityKey(),
                };

                var claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(token, validation, out var securityToken);

                var claims = (claimsPrincipal.Identity as ClaimsIdentity).Claims;

                var userIdClaim = claims?.FirstOrDefault(c => c.Type == ClaimConstants.UserId);
                if (userIdClaim == null
                    || !int.TryParse(userIdClaim.Value, out userId))
                {
                    userId = 0;
                }

                var serviceIdClaim = claims?.FirstOrDefault(c => c.Type == ClaimConstants.ServiceId);
                if (serviceIdClaim == null
                    || !int.TryParse(serviceIdClaim.Value, out serviceId))
                {
                    serviceId = 0;
                }

                userName = claims?.FirstOrDefault(c => c.Type == ClaimConstants.UserName)?.Value;
                serviceName = claims?.FirstOrDefault(c => c.Type == ClaimConstants.ServiceName)?.Value;

                roles = claims
                    ?.Where(c => c.Type == ClaimTypes.Role && !string.IsNullOrWhiteSpace(c.Value))
                    ?.Select(c => int.TryParse(c.Value, out int value) ? (Roles)value : 0)
                    ?.Where(r => Enum.IsDefined(r));

                return true;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to validate token");
            }

            return false;
        }

        /// <inheritdoc />
        public bool TryValidateForUserRole(string token, string userName, Roles role)
        {
            return !string.IsNullOrWhiteSpace(userName)
                && role != Roles.None
                && this.TryValidate(token, out var roles, out _, out _, out var userNameInToken, out _)
                && !string.IsNullOrWhiteSpace(userNameInToken)
                && userName.Equals(userNameInToken, StringComparison.OrdinalIgnoreCase)
                && roles != null
                && roles.Contains(role);
        }

        /// <inheritdoc />
        public bool TryValidateForServiceRole(string token, string serviceName, Roles role)
        {
            return !string.IsNullOrWhiteSpace(serviceName)
                && role != Roles.None
                && this.TryValidate(token, out var roles, out _, out _, out _, out var serviceNameInToken)
                && !string.IsNullOrWhiteSpace(serviceNameInToken)
                && serviceName.Equals(serviceNameInToken, StringComparison.OrdinalIgnoreCase)
                && roles != null
                && roles.Contains(role);
        }
    }
}
