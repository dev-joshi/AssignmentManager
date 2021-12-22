namespace AssignmentManager.Entities.ApiModel.Response
{
    /// <summary>
    /// Token Validation Reponse.
    /// </summary>
    /// <seealso cref="AssignmentManager.Entities.ApiModel.Response.BaseResponse" />
    public class TokenValidationResponse : BaseResponse
    {
        /// <summary>
        /// Gets or sets the validation message.
        /// </summary>
        /// <value>
        /// The validation message.
        /// </value>
        public string ValidationMessage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenValidationResponse"/> class.
        /// </summary>
        /// <param name="baseResponse">The base response.</param>
        public TokenValidationResponse(BaseResponse baseResponse) : base(baseResponse)
        {
        }

        /// <summary>
        /// Compose success response with the specified message.
        /// </summary>
        /// <param name="validationMessage">The validation message.</param>
        /// <returns>Validation Success Reponse.</returns>
        public static TokenValidationResponse Success(string validationMessage)
        {
            return new TokenValidationResponse(Success())
            {
                ValidationMessage = validationMessage,
            };
        }
    }
}
