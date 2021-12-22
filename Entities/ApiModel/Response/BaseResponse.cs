namespace AssignmentManager.Entities.ApiModel.Response
{
    /// <summary>
    /// Base Reponse for all APIs.
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether login succeeds.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string Error { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseResponse"/> class.
        /// </summary>
        /// <param name="IsSuccessful">if set to <c>true</c> [is successful].</param>
        /// <param name="Error">The error.</param>
        public BaseResponse(
            bool IsSuccessful,
            string Error)
        {
            this.IsSuccessful = IsSuccessful;
            this.Error = Error;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseResponse"/> class.
        /// </summary>
        /// <param name="baseResponse">The base response.</param>
        public BaseResponse(BaseResponse baseResponse)
        {
            this.IsSuccessful = baseResponse.IsSuccessful;
            this.Error = baseResponse.Error;
        }

        /// <summary>
        /// Compose failure response with the specified error.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>Failure Response.</returns>
        public static BaseResponse Failure(string error)
        {
            return new BaseResponse(false, error);
        }

        /// <summary>
        /// Compose success response.
        /// </summary>
        /// <returns>Success Reponse.</returns>
        public static BaseResponse Success()
        {
            return new BaseResponse(true, string.Empty);
        }
    }
}
