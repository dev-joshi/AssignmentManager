namespace AssignmentManager.Entities.ApiModel.Response
{
    using System;

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
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the expiration time.
        /// </summary>
        /// <value>
        /// The expiration time.
        /// </value>
        public DateTime Expires { get; set; }

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
        /// <param name="expires">The expiration time.</param>
        /// <returns>Login Success Reponse.</returns>
        public static LoginResponse Success(string token, DateTime expires)
        {
            return new LoginResponse(Success())
            {
                Token = token,
                Expires = expires,
            };
        }
    }
}
