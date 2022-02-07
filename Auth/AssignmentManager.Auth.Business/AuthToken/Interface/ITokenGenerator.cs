namespace AssignmentManager.Auth.Business
{
    using System.Threading.Tasks;
    using AssignmentManager.Entities;

    /// <summary>
    /// Used to Generate Tokens.
    /// </summary>
    public interface ITokenGenerator
    {
        /// <summary>
        /// Generates the token for given user.
        /// </summary>
        /// <param name="userId">The User Id.</param>
        /// <returns>
        /// The token.
        /// </returns>
        Task<Token> GenerateTokenForUserAsync(int userId);

        /// <summary>
        /// Generates the token for given service.
        /// </summary>
        /// <param name="serviceId">The Service Id.</param>
        /// <returns>
        /// The token.
        /// </returns>
        Task<Token> GenerateTokenForServiceAsync(int serviceId);
    }
}
