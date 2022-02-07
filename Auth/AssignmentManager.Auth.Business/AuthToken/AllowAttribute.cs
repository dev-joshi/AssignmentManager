namespace AssignmentManager.Auth.Business.AuthToken
{
    using AssignmentManager.Entities;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Custom authorization Attribute.
    /// </summary>
    public class AllowAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AllowAttribute"/> class.
        /// </summary>
        public AllowAttribute()
            : base(typeof(AllowFilter))
        {
            this.Arguments = new object[] { this.Filter };
        }

        /// <summary>
        /// Gets or sets the Role to allow.
        /// </summary>
        public Roles Role
        {
            get { return this.Filter.Role; }
            set { this.Filter.Role = value; }
        }

        /// <summary>
        /// Gets or sets the User id to allow.
        /// </summary>
        public int UserId
        {
            get { return this.Filter.UserId; }
            set { this.Filter.UserId = value; }
        }

        /// <summary>
        /// Gets or sets the Service id to allow.
        /// </summary>
        public int ServiceId
        {
            get { return this.Filter.ServiceId; }
            set { this.Filter.ServiceId = value; }
        }

        /// <summary>
        /// Gets or sets the UserName to allow.
        /// </summary>
        public string UserName
        {
            get { return this.Filter.UserName; }
            set { this.Filter.UserName = value; }
        }

        /// <summary>
        /// Gets or sets the ServiceName to allow.
        /// </summary>
        public string ServiceName
        {
            get { return this.Filter.ServiceName; }
            set { this.Filter.ServiceName = value; }
        }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        private Filter Filter { get; set; } = new Filter();
    }
}
