namespace AssignmentManager.Auth.API.Controllers
{
    using System;
    using System.Threading.Tasks;
    using AssignmentManager.Auth.Business;
    using AssignmentManager.Common;
    using AssignmentManager.DB.Storage.Repositories;
    using AssignmentManager.Entities.ApiModel.Request;
    using AssignmentManager.Entities.ApiModel.Response;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The Token Controller.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("token")]
    [ApiController]
    public class TokenController : Controller
    {
        /// <summary>
        /// The token generator.
        /// </summary>
        private readonly ITokenGenerator tokenGenerator;

        /// <summary>
        /// The service repository.
        /// </summary>
        private readonly IServiceRepository serviceRepository;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<TokenController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenController"/> class.
        /// </summary>
        /// <param name="tokenGenerator">The token generator.</param>
        /// <param name="serviceRepository">The service repository.</param>
        /// <param name="logger">The logger.</param>
        public TokenController(
            ITokenGenerator tokenGenerator,
            IServiceRepository serviceRepository,
            ILogger<TokenController> logger)
        {
            this.tokenGenerator = tokenGenerator;
            this.serviceRepository = serviceRepository;
            this.logger = logger;
        }

        /// <summary>
        /// returns auth token for the specified service creds.
        /// </summary>
        /// <param name="serviceCreds">The service creds.</param>
        /// <returns>Auth Token.</returns>
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> Token(ServiceCreds serviceCreds)
        {
            this.logger.LogInformation("Token Request Recived for {service}", serviceCreds?.ServiceName);

            try
            {
                if (serviceCreds != null
                && !string.IsNullOrWhiteSpace(serviceCreds.ServiceName)
                && !string.IsNullOrEmpty(serviceCreds.ServiceSecret))
                {
                    var service = await this.serviceRepository.GetServiceAsync(serviceCreds.ServiceName);

                    this.logger.LogDebug("Service Found : {serviceFound}", service != null);

                    if (service != null)
                    {
                        if (service.SecretHash.ToLowerInvariant()
                            == serviceCreds.ServiceSecret.Hash().ToLowerInvariant())
                        {
                            var token = await this.tokenGenerator.GenerateTokenForServiceAsync(service.Id);

                            if (token == null)
                            {
                                this.logger.LogInformation("Could not generate auth token");
                                return this.BadRequest(BaseResponse.Failure("Could not generate auth token"));
                            }

                            return this.Ok(LoginResponse.Success(token));
                        }

                        this.logger.LogInformation("Invalid Secret");
                        return this.Unauthorized(BaseResponse.Failure("Invalid Secret"));
                    }

                    this.logger.LogInformation("Invalid Service");
                    return this.Unauthorized(BaseResponse.Failure("Invalid Service"));
                }

                this.logger.LogInformation("Service creds not provided");
                return this.BadRequest(BaseResponse.Failure("Service creds not provided"));
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to process Token Request");
                return this.StatusCode(500, BaseResponse.Failure("Failed to process Token Request"));
            }
        }
    }
}
