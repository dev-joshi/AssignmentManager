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
    }
}
