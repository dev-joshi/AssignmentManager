namespace AssignmentManager.API.Controllers
{
    using System.Linq;
    using System.Text;
    using AssignmentManager.Auth.Business;
    using AssignmentManager.Auth.Business.AuthToken;
    using AssignmentManager.Common;
    using AssignmentManager.Entities;
    using AssignmentManager.Entities.ApiModel.Response;
    using Microsoft.AspNetCore.Authorization;
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
        public ActionResult<BaseResponse> ReturnRoles()
        {
            this.logger.LogInformation("Token validation Request Received");

            if (!this.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeaders)
                || string.IsNullOrWhiteSpace(authHeaders.FirstOrDefault()))
            {
                this.logger.LogWarning("Token not provided");
                return this.BadRequest(BaseResponse.Failure("Token not provided"));
            }

            if (this.tokenValidator.TryValidate(authHeaders.FirstOrDefault().Replace("Bearer ", string.Empty), out var roles, out int userId, out int serviceId, out string userName, out string serviceName))
            {
                var sb = new StringBuilder("This Token has following details :");

                if (userId > 0)
                {
                    sb.AppendLine("UserId : " + userId);
                }

                if (!string.IsNullOrWhiteSpace(userName))
                {
                    sb.AppendLine("UserName : " + userName);
                }

                if (serviceId > 0)
                {
                    sb.AppendLine("ServiceId : " + serviceId);
                }

                if (!string.IsNullOrWhiteSpace(serviceName))
                {
                    sb.AppendLine("ServiceName : " + serviceName);
                }

                sb.AppendLine("Roles :");

                foreach (var role in roles)
                {
                    sb.AppendLine(role.GetDescription());
                }

                return this.Ok(TokenValidationResponse.Success(sb.ToString()));
            }
            else
            {
                this.logger.LogWarning("Token is invalid");
                return this.Unauthorized(BaseResponse.Failure("Token is invalid"));
            }
        }

        [HttpGet("RoleCheck/CreateAssignment")]
        [HttpPost("RoleCheck/CreateAssignment")]
        [Authorize]
        [Allow(Role = Roles.CreateAssignment)]
        public ActionResult<BaseResponse> ValidateRole1()
        {
            return this.Ok(TokenValidationResponse.Success("You are Authorized!!"));
        }

        [HttpGet("RoleCheck/ViewAssignment")]
        [HttpPost("RoleCheck/ViewAssignment")]
        [Authorize]
        [Allow(Role = Roles.ViewAssignment)]
        public ActionResult<BaseResponse> ValidateRole2()
        {
            return this.Ok(TokenValidationResponse.Success("You are Authorized!!"));
        }
    }
}
