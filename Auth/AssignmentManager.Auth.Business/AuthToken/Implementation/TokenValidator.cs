namespace AssignmentManager.Auth.Business.AuthToken.Implementation
{
    using System.Collections.Generic;
    using AssignmentManager.DB.Storage.Repositories;
    using AssignmentManager.Entities;

    /// <inheritdoc />
    public class TokenValidator : ITokenValidator
    {
        /// <summary>
        /// The role repository.
        /// </summary>
        private IRoleRepository roleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenValidator"/> class.
        /// </summary>
        /// <param name="roleRepository">The role repository.</param>
        public TokenValidator(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        /// <inheritdoc />
        public IEnumerable<Role> GetRoles(string token)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                var split = token.Split(",");

                if (split.Length > 0)
                {
                    foreach (var id in split)
                    {
                        yield return this.roleRepository.GetRoleAsync(int.Parse(id))
                            .ConfigureAwait(false).GetAwaiter().GetResult();
                    }
                }
            }
        }
    }
}
