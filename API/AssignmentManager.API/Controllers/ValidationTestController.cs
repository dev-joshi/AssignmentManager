namespace AssignmentManager.API.Controllers
{
    using System.Text;
    using AssignmentManager.Auth.Business;
    using AssignmentManager.Entities.ApiModel.Request;
    using AssignmentManager.Entities.ApiModel.Response;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The Validation Test controller.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("valid")]
    [ApiController]
    public class ValidationTestController : ControllerBase
    {
        /// <summary>
        /// The token validator.
        /// </summary>
        private readonly ITokenValidator tokenValidator;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<ValidationTestController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationTestController" /> class.
        /// </summary>
        /// <param name="tokenValidator">The token validator.</param>
        /// <param name="logger">The logger.</param>
        public ValidationTestController(
            ITokenValidator tokenValidator,
            ILogger<ValidationTestController> logger)
        {
            this.tokenValidator = tokenValidator;
            this.logger = logger;
        }

        /// <summary>
        /// Returns the roles.
        /// </summary>
        /// <param name="tokenRequest">The token request.</param>
        /// <returns>the roles.</returns>
        [HttpGet]
        [HttpPost]
        public ActionResult<BaseResponse> ReturnRoles(TokenValidationRequest tokenRequest)
        {
            this.logger.LogInformation("Token validation Request Received");

            if (string.IsNullOrWhiteSpace(tokenRequest.Token))
            {
                this.logger.LogWarning("Token not provided");
                return this.BadRequest(BaseResponse.Failure("Token not provided"));
            }

            if (this.tokenValidator.TryValidate(tokenRequest.Token, out var roles))
            {
                var sb = new StringBuilder("This Token is valid for following roles :");
                foreach (var role in roles)
                {
                    sb.Append('\n' + role.Name);
                }

                return this.Ok(TokenValidationResponse.Success(sb.ToString()));
            }
            else
            {
                this.logger.LogWarning("Token is invalid");
                return this.Unauthorized(BaseResponse.Failure("Token is invalid"));
            }
        }
    }
}
