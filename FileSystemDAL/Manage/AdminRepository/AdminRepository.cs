

namespace FileSystemDAL.Manage
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;

    using FileSystemDAL.Enum;
    using FileSystemDAL.Helper;
    using FileSystemDAL.Models;

    using NHibernate;
    using NHibernate.Criterion;

    /// <summary>
    /// The admin repository.
    /// </summary>
    public class AdminRepository : PersonRepository, IAdminRepository
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminRepository"/> class.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public AdminRepository(Repository repository) : base(repository, EPermission.RepositoryAdmin)
        {
        }

        /// <summary>
        /// The invite friend.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public void InviteFriend(Repository repository)
        {
            PartnershipManager.InviteFriend(repository);
        }

        /// <summary>
        /// The remove friend.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public void RemoveFriend(Repository repository)
        {
            PartnershipManager.RemoveFriend(repository);
        }

        /// <summary>
        /// The accept invitation.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public void AcceptInvitation(Repository repository)
        {
            PartnershipManager.AcceptInvitation(repository);
        }

        /// <summary>
        /// The search repository.
        /// </summary>
        /// <param name="repositoryName">
        /// The repository name.
        /// </param>
        /// <returns>
        /// The <see cref="Repository"/>.
        /// </returns>
        public Repository SearchRepository(string repositoryName)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.CreateCriteria(typeof(Repository))
                    .Add(Restrictions.Eq("RepositoryName", repositoryName))
                    .UniqueResult<Repository>();
            }
        }
    }
}
