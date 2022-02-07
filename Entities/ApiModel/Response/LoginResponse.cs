namespace AssignmentManager.Entities.ApiModel.Response
{
    /// <summary>
    /// Login Response.
    /// </summary>
    /// <seealso cref="AssignmentManager.Entities.ApiModel.Response.BaseResponse" />
    public class LoginResponse : BaseResponse
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public Token Token { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginResponse"/> class.
        /// </summary>
        /// <param name="baseResponse">The base response.</param>
        public LoginResponse(BaseResponse baseResponse) : base(baseResponse)
        {
        }

        /// <summary>
        /// Compose success response with the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Login Success Reponse.</returns>
        public static LoginResponse Success(Token token)
        {
            return new LoginResponse(Success())
            {
                Token = token,
            };
        }
    }
}
