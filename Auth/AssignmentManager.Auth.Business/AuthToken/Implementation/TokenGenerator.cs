namespace AssignmentManager.Auth.Business.AuthToken.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using AssignmentManager.Auth.Business.AuthToken.Interface;
    using AssignmentManager.Entities;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;

    /// <inheritdoc />
    internal class TokenGenerator : ITokenGenerator
    {
        /// <summary>
        /// The token utils.
        /// </summary>
        private readonly ITokenUtils tokenUtils;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<TokenGenerator> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenGenerator" /> class.
        /// </summary>
        /// <param name="tokenUtils">The token utils.</param>
        /// <param name="logger">The logger.</param>
        public TokenGenerator(
            ITokenUtils tokenUtils,
            ILogger<TokenGenerator> logger)
        {
            this.tokenUtils = tokenUtils;
            this.logger = logger;
        }

        /// <inheritdoc />
        public (string, DateTime) GenerateToken(IEnumerable<Role> roles)
        {
            var token = string.Empty;
            var expiry = DateTime.MinValue;

            try
            {
                expiry = DateTime.UtcNow.Add(this.tokenUtils.GetExpirationLimit());
                token = this.GenerateToken(roles, expiry);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to generate token");
            }

            return (token, expiry);
        }

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="roles">The roles.</param>
        /// <param name="expires">The expires.</param>
        /// <returns>Token that expires in specifed time.</returns>
        private string GenerateToken(IEnumerable<Role> roles, DateTime expires)
        {
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(),
                Expires = expires,
                SigningCredentials = this.tokenUtils.GetSigningCredentials(),
            };

            foreach (var role in roles)
            {
                descriptor.Subject.AddClaim(new Claim($"Role_{role.Id}", role.Name));
            }

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
