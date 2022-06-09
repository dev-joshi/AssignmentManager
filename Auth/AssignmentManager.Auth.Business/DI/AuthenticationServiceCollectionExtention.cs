namespace AssignmentManager.Auth.Business.DI
{
    using AssignmentManager.Auth.Business.AuthToken.Implementation;
    using AssignmentManager.Auth.Business.AuthToken.Interface;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// DI extentions for adding Authentication Business.
    /// </summary>
    public static class AuthenticationServiceCollectionExtention
    {
        /// <summary>
        /// Adds the token generation logic to service collection.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>service collection.</returns>
        public static IServiceCollection AddTokenGeneration(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddTransient<ITokenGenerator, TokenGenerator>();
            serviceCollection.TryAddSingleton<ITokenUtils, TokenUtils>();

            return serviceCollection;
        }

        /// <summary>
        /// Adds the token validation logic to service collection.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>service collection.</returns>
        public static IServiceCollection AddTokenValidation(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddTransient<ITokenValidator, TokenValidator>();
            serviceCollection.TryAddSingleton<ITokenUtils, TokenUtils>();

            var tokenUtils = serviceCollection.BuildServiceProvider().GetService<ITokenUtils>();

            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        RequireExpirationTime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = tokenUtils.GetSecurityKey(),
                    };
                });

            return serviceCollection;
        }
    }
}
