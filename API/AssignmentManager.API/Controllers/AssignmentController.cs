namespace AssignmentManager.API.Controllers
{
    using System;
    using System.Collections.Generic;
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
        /// Gets the response api/assignments/user/{id}.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>assignments for the given user.</returns>
        [HttpGet("user/{id}")]
        public ActionResult<IEnumerable<Assignment>> GetAssignmentsByUser(int id)
        {
            var assignments = new List<Assignment>();

            if (id > 0)
            {
                // Adding test data to debug UI.
                for (int i = 1; i <= id; i++)
                {
                    assignments.Add(new Assignment(
                        i,
                        (i % 2) + 1,
                        new List<string> { i % 2 == 0 ? "two" : "one" },
                        $"Assignment {i}",
                        $"The assignment number {i}",
                        null,
                        DateTime.Now.AddDays(-5 - new Random().Next(1, 5)),
                        DateTime.Now,
                        DateTime.Now.AddDays(5 + new Random().Next(1, 5))));
                }
            }

            return assignments;
        }

        /// <summary>
        /// Gets the response api/assignments/owner/{id}.
        /// </summary>
        /// <param name="id">The owner id.</param>
		/// <returns>assignments for the given owner.</returns>
        [HttpGet("owner/{id}")]
        public ActionResult<IEnumerable<Assignment>> GetAssignmentsByOwner(int id)
		{
			var assignments = new List<Assignment>();

			if (id > 0)
			{
                var num = new Random().Next(1, 6);

				// Adding test data to debug UI.
                for (int i = 1; i <= num; i++)
				{
					assignments.Add(new Assignment(
						i,
						id,
						new List<string> { id % 2 == 0 ? "two" : "one" },
						$"Assignment {i}",
						$"The assignment number {i}",
						null,
						DateTime.Now.AddDays(-5 - new Random().Next(1, 5)),
						DateTime.Now,
						DateTime.Now.AddDays(5 + new Random().Next(1, 5))));
				}
            }

			return assignments;
        }
    }
}
