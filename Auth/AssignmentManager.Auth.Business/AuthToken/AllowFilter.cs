namespace AssignmentManager.Auth.Business.AuthToken
{
    using System.Linq;
    using System.Security.Claims;
    using AssignmentManager.Entities;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Custom authorization Filter.
    /// </summary>
    public class AllowFilter : IAuthorizationFilter
    {
        /// <summary>
        /// The filter.
        /// </summary>
        private readonly Filter filter;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<AllowFilter> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AllowFilter"/> class.
        /// </summary>
        /// <param name="filter">the filter.</param>
        /// <param name="logger">the logger.</param>
        public AllowFilter(
            Filter filter,
            ILogger<AllowFilter> logger)
        {
            this.filter = filter;
            this.logger = logger;
        }

        /// <inheritdoc />
        void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext context)
        {
            if (!this.IsAllowed(context))
            {
                this.logger.LogWarning("Request not authorized for path : {requestPath}", context.HttpContext.Request.Path);
                context.Result = new ForbidResult();
            }
        }

        private bool IsAllowed(AuthorizationFilterContext context)
        {
            var claims = context.HttpContext.User.Claims;

            if (this.filter.Role != Roles.None
                && !claims.Any(c => c.Type == ClaimTypes.Role && c.Value == this.filter.Role.ToString("d")))
            {
                return false;
            }

            if (this.filter.UserId != 0
                && !claims.Any(c => c.Type == ClaimConstants.UserId && c.Value == this.filter.UserId.ToString()))
            {
                return false;
            }

            if (this.filter.ServiceId != 0
                && !claims.Any(c => c.Type == ClaimConstants.ServiceId && c.Value == this.filter.ServiceId.ToString()))
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(this.filter.UserName)
                && !claims.Any(c => c.Type == ClaimConstants.UserName && c.Value == this.filter.UserName))
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(this.filter.ServiceName)
                && !claims.Any(c => c.Type == ClaimConstants.ServiceName && c.Value == this.filter.ServiceName))
            {
                return false;
            }

            return true;
        }
    }
}
