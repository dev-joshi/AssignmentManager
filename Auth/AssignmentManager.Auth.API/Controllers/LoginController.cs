namespace AssignmentManager.Auth.API.Controllers
{
    using System;
    using System.Threading.Tasks;
    using AssignmentManager.Auth.Business;
    using AssignmentManager.Common;
    using AssignmentManager.DB.Storage.Repositories;
    using AssignmentManager.Entities.ApiModel;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The Login Controller.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// The token generator.
        /// </summary>
        private ITokenGenerator tokenGenerator;

        /// <summary>
        /// The user repository.
        /// </summary>
        private IUserRepository userRepository;

        /// <summary>
        /// The logger.
        /// </summary>
        private ILogger<LoginController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController" /> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="tokenGenerator">The token generator.</param>
        /// <param name="logger">The logger.</param>
        public LoginController(
            IUserRepository userRepository,
            ITokenGenerator tokenGenerator,
            ILogger<LoginController> logger)
        {
            this.userRepository = userRepository;
            this.tokenGenerator = tokenGenerator;
            this.logger = logger;
        }

        /// <summary>
        /// returns auth token for the specified user creds.
        /// </summary>
        /// <param name="userCreds">The user creds.</param>
        /// <returns>Auth Token.</returns>
        public async Task<ActionResult<string>> Login(UserCreds userCreds)
        {
            this.logger.LogInformation("Login Request Recived for {user}", userCreds?.UserName);

            try
            {
                if (userCreds != null
                && !string.IsNullOrWhiteSpace(userCreds.UserName)
                && !string.IsNullOrEmpty(userCreds.Password))
                {
                    var user = await this.userRepository.GetUserAsync(userCreds.UserName);

                    this.logger.LogDebug("User Found : {userFound}", user != null);

                    if (user != null)
                    {
                        this.logger.LogDebug("PasswordHashFromDb : {PasswordHash}", user.PasswordHash);
                        this.logger.LogDebug("PasswordHashFromReq : {PasswordHash}", userCreds.Password.Hash());

                        if (user.PasswordHash.ToLowerInvariant()
                            == userCreds.Password.Hash().ToLowerInvariant())
                        {
                            this.logger.LogDebug("User : {user}, Roles count : {user.Roles.Count}", user.UserName, user.Roles.Count);
                            var token = this.tokenGenerator.GenerateToken(user.Roles);

                            if (string.IsNullOrWhiteSpace(token))
                            {
                                this.logger.LogInformation("Could not generate auth token");
                                return this.BadRequest("Could not generate auth token");
                            }

                            return this.Ok(token.Base64Encode());
                        }

                        this.logger.LogInformation("Invalid Password");
                        return this.Unauthorized("Invalid Password");
                    }

                    this.logger.LogInformation("Invalid User");
                    return this.Unauthorized("Invalid User");
                }

                this.logger.LogInformation("User creds not provided");
                return this.BadRequest("User creds not provided");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to process Login Reuqeust");
                return this.StatusCode(500);
            }
        }
    }
}
