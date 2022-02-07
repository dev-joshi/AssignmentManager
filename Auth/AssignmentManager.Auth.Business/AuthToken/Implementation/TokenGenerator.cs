namespace AssignmentManager.Auth.Business.AuthToken.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AssignmentManager.Auth.Business.AuthToken.Interface;
    using AssignmentManager.DB.Storage.Repositories;
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
        /// The user repository.
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// The service repository.
        /// </summary>
        private readonly IServiceRepository serviceRepository;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<TokenGenerator> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenGenerator" /> class.
        /// </summary>
        /// <param name="tokenUtils">The token utils.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="serviceRepository">The service repository.</param>
        /// <param name="logger">The logger.</param>
        public TokenGenerator(
            ITokenUtils tokenUtils,
            IUserRepository userRepository,
            IServiceRepository serviceRepository,
            ILogger<TokenGenerator> logger)
        {
            this.tokenUtils = tokenUtils;
            this.userRepository = userRepository;
            this.serviceRepository = serviceRepository;
            this.logger = logger;
        }

        /// <inheritdoc />
        public async Task<Token> GenerateTokenForUserAsync(int userId)
        {
            try
            {
                var user = await this.userRepository.GetUserAsync(userId).ConfigureAwait(false);

                if (user != null)
                {
                    var expiry = DateTime.UtcNow.Add(this.tokenUtils.GetExpirationLimit());

                    return new Token
                    {
                        AccessToken = this.GenerateToken(user.Id, user.Name, user.Roles, expiry),
                        Expires = expiry,
                    };
                }
                else
                {
                    this.logger.LogError("User with id {0} not found", userId);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to generate token for userId {0}", userId);
            }

            return null;
        }

        /// <inheritdoc />
        public async Task<Token> GenerateTokenForServiceAsync(int serviceId)
        {
            try
            {
                var service = await this.serviceRepository.GetServiceAsync(serviceId).ConfigureAwait(false);

                if (service != null)
                {
                    var expiry = DateTime.UtcNow.Add(this.tokenUtils.GetExpirationLimit());

                    return new Token
                    {
                        AccessToken = this.GenerateToken(service.Id, service.ServiceName, service.Roles, expiry, true),
                        Expires = expiry,
                    };
                }
                else
                {
                    this.logger.LogError("Service with id {0} not found", serviceId);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to generate token for serviceId {0}", serviceId);
            }

            return null;
        }

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="id">user or service id.</param>
        /// <param name="name">user or service name.</param>
        /// <param name="roles">The roles.</param>
        /// <param name="expiry">The expiration time.</param>
        /// <param name="isService">Generate token for service.</param>
        /// <returns>Token that expires in specifed time.</returns>
        private string GenerateToken(int id, string name, IEnumerable<Role> roles, DateTime expiry, bool isService = false)
        {
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(),
                Expires = expiry,
                SigningCredentials = this.tokenUtils.GetSigningCredentials(),
            };

            if (isService)
            {
                descriptor.Subject.AddClaim(new Claim(ClaimConstants.ServiceId, id.ToString()));
                descriptor.Subject.AddClaim(new Claim(ClaimConstants.ServiceName, name));
            }
            else
            {
                descriptor.Subject.AddClaim(new Claim(ClaimConstants.UserId, id.ToString()));
                descriptor.Subject.AddClaim(new Claim(ClaimConstants.UserName, name));
            }

            foreach (var role in roles)
            {
                descriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role.Id.ToString("d")));
            }

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
