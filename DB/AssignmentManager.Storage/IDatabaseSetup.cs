namespace AssignmentManager.DB.Storage
{
    /// <summary>
    /// Setup the Database.
    /// </summary>
    public interface IDatabaseSetup
    {
        /// <summary>
        /// Setups the database.
        /// </summary>
        void SetupDatabase();

        /// <summary>
        /// Seeds the data.
        /// </summary>
        void SeedData();

        /// <summary>
        /// Determines whether database can be connected.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance can connect; otherwise, <c>false</c>.
        /// </returns>
        bool CanConnect();
    }
}
