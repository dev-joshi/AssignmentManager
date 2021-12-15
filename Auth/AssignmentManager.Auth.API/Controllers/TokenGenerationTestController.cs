namespace AssignmentManager.Auth.API.Controllers
{
    using System.Threading.Tasks;
    using AssignmentManager.Auth.Business;
    using AssignmentManager.DB.Storage.Repositories;
    using AssignmentManager.Entities;
    using AssignmentManager.Entities.ApiModel;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Token Generation Test Controller.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("generate")]
    [ApiController]
    public class TokenGenerationTestController : ControllerBase
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
        /// Initializes a new instance of the <see cref="TokenGenerationTestController" /> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="tokenGenerator">The token generator.</param>
        public TokenGenerationTestController(
            IUserRepository userRepository,
            ITokenGenerator tokenGenerator)
        {
            this.userRepository = userRepository;
            this.tokenGenerator = tokenGenerator;
        }

        /// <summary>
        /// Returns the token for a user.
        /// </summary>
        /// <param name="userCreds">The user creds.</param>
        /// <returns>the token.</returns>
        [HttpGet]
        public async Task<string> GetToken(UserCreds userCreds)
        {
            if (userCreds != null
                && !string.IsNullOrWhiteSpace(userCreds.UserName)
                && string.IsNullOrEmpty(userCreds.Password))
            {
                var user = await this.userRepository.GetUserAsync(userCreds.UserName);

                if (user != null)
                {
                    if (user.PasswordHash == userCreds.Password.Hash())
                    {
                        return this.tokenGenerator.GenerateToken(user.Roles);
                    }
                }
            }

            return string.Empty;
        }
    }
}
