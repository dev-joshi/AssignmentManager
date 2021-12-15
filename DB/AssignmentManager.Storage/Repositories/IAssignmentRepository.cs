namespace AssignmentManager.DB.Storage.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AssignmentManager.Entities;

    /// <summary>
    /// Assingment Repository.
    /// </summary>
    public interface IAssignmentRepository
    {
        /// <summary>
        /// Gets the assignment.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The Assignment.</returns>
        Task<Assignment> GetAssignmentAsync(int id);

        /// <summary>
        /// Gets the assignment by owner id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The Assignment.</returns>
        Task<IEnumerable<Assignment>> GetAssignmentByOwnerAsync(int id);

        /// <summary>
        /// Gets all assignments.
        /// </summary>
        /// <returns>The Assignments.</returns>
        Task<IEnumerable<Assignment>> GetAllAssignmentsAsync();

        /// <summary>
        /// Add or update the given Assignment.
        /// </summary>
        /// <param name="assignment">The assignment.</param>
        /// <returns>Task representing async operation.</returns>
        Task AddOrUpdateAsync(Assignment assignment);

        /// <summary>
        /// Deletes the Assignment.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task representing async operation.</returns>
        Task DeleteAsync(int id);
    }
}
