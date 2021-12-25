namespace AssignmentManager.Auth.Business.AuthToken.Implementation
{
    using System;
    using AssignmentManager.Auth.Business.AuthToken.Interface;
    using AssignmentManager.DB.Storage.Repositories;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;

    /// <inheritdoc />
    internal class TokenUtils : ITokenUtils
    {
        /// <summary>
        /// The key name.
        /// </summary>
        private const string KeyName = "JwTSecretKey";

        /// <summary>
        /// The key repository.
        /// </summary>
        private readonly IKeyRepository keyRepository;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<TokenUtils> logger;

        /// <summary>
        /// The security key.
        /// </summary>
        private SecurityKey securityKey;

        /// <summary>
        /// The signing credentials.
        /// </summary>
        private SigningCredentials signingCredentials;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenUtils" /> class.
        /// </summary>
        /// <param name="keyRepository">The key repository.</param>
        /// <param name="logger">The logger.</param>
        public TokenUtils(
            IKeyRepository keyRepository,
            ILogger<TokenUtils> logger)
        {
            this.keyRepository = keyRepository;
            this.logger = logger;
        }

        /// <inheritdoc />
        public TimeSpan GetExpirationLimit()
        {
            return TimeSpan.FromMinutes(30);
        }

        /// <inheritdoc />
        public SecurityKey GetSecurityKey()
        {
            if (this.securityKey is null)
            {
                this.Init();
            }

            return this.securityKey;
        }

        /// <inheritdoc />
        public SigningCredentials GetSigningCredentials()
        {
            if (this.signingCredentials is null)
            {
                this.Init();
            }

            return this.signingCredentials;
        }

        /// <summary>
        /// Initializes this instance by loading key from DB.
        /// </summary>
        private void Init()
        {
            this.logger.LogDebug("Token Util Initialization");

            var key = this.keyRepository.GetKeyAsync(KeyName)
                .ConfigureAwait(false).GetAwaiter().GetResult();

            if (key is null
                || string.IsNullOrWhiteSpace(key.Value))
            {
                throw new NullReferenceException($"Key {KeyName} not found in DB");
            }

            this.securityKey = new SymmetricSecurityKey(Convert.FromBase64String(key.Value));
            this.signingCredentials = new SigningCredentials(this.securityKey, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
