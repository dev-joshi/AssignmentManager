namespace AssignmentManager.API.Controllers
{
    using System.Collections.Generic;
    using AssignmentManager.Auth.Business;
    using AssignmentManager.Entities;
    using Microsoft.AspNetCore.Mvc;

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
        private ITokenValidator tokenValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationTestController"/> class.
        /// </summary>
        /// <param name="tokenValidator">The token validator.</param>
        public ValidationTestController(ITokenValidator tokenValidator)
        {
            this.tokenValidator = tokenValidator;
        }

        /// <summary>
        /// Returns the roles.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>the roles.</returns>
        [HttpGet]
        public IEnumerable<Role> ReturnRoles(string token)
        {
            return this.tokenValidator.GetRoles(token);
        }
    }
}
