namespace AssignmentManager.API.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AssignmentManager.DB.Repositories;
    using AssignmentManager.Entities;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///   Assignment Controller.
    /// </summary>
    [Route("api/assignments")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        /// <summary>
        /// The assignment repository.
        /// </summary>
        private readonly IAssignmentRepository assignmentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssignmentController"/> class.
        /// </summary>
        /// <param name="assignmentRepository">The assignment repository.</param>
        public AssignmentController(IAssignmentRepository assignmentRepository)
        {
            this.assignmentRepository = assignmentRepository;
        }

        /// <summary>
        /// Gets the response api/assignments/{id}.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>assignment for given id.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Assignment>> GetAssignment(int id)
        {
            var product = await this.assignmentRepository.GetAssignmentAsync(id);

            if (product is null)
            {
                return this.NotFound();
            }

            return this.Ok(product);
        }

        /// <summary>
        /// Gets the response api/assignments.
        /// </summary>
        /// <returns>all the assignments.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignments()
        {
            return this.Ok(await this.assignmentRepository.GetAllAssignmentsAsync());
        }

        /// <summary>
        /// Gets the response api/assignments/user/{id}.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>assignments for the given user.</returns>
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignmentsByUser(int id)
        {
            if (id > 0)
            {
                var assignments = await this.assignmentRepository.GetAssignmentByOwnerAsync(id);

                if ((assignments is not null) && (assignments.Count() > 0))
                {
                    return this.Ok(assignments);
                }

                return this.NotFound();
            }

            return this.BadRequest();
        }

        /// <summary>
        /// Gets the response api/assignments/Add.
        /// </summary>
        /// <param name="assignment">The assignment.</param>
        /// <returns>the response for addition.</returns>
        [HttpPatch("Add")]
        [HttpPost("Add")]
        [HttpPut("Add")]
        public async Task<ActionResult> AddAssignment(Assignment assignment)
        {
            await this.assignmentRepository.AddOrUpdateAsync(assignment);

            return this.Ok();
        }

        /// <summary>
        /// Gets the delete response for api/assignments/{id}.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the response for deletion.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAssignment(int id)
        {
            await this.assignmentRepository.DeleteAsync(id);
            return this.Ok();
        }
    }
}
