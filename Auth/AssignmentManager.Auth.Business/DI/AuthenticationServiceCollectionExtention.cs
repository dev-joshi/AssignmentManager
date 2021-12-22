namespace AssignmentManager.Auth.Business.DI
{
    using AssignmentManager.Auth.Business.AuthToken.Implementation;
    using Microsoft.Extensions.DependencyInjection;

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
            serviceCollection.AddTransient<ITokenGenerator, TokenGenerator>();
            serviceCollection.AddSingleton<TokenUtils>();

            return serviceCollection;
        }

        /// <summary>
        /// Adds the token validation logic to service collection.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>service collection.</returns>
        public static IServiceCollection AddTokenValidation(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ITokenValidator, TokenValidator>();
            serviceCollection.AddSingleton<TokenUtils>();

            return serviceCollection;
        }
    }
}
