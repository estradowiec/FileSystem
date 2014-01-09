
namespace FileSystemDAL.Manage
{
    using System.Collections.Generic;
    using System.Dynamic;

    using FileSystemDAL.Helper;
    using FileSystemDAL.Models;

    using NHibernate;
    using NHibernate.Criterion;

    /// <summary>
    /// The patnership manager.
    /// </summary>
    public class PartnershipManager : IPartnershipManager
    {
        /// <summary>
        /// The repository.
        /// </summary>
        private readonly Repository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PartnershipManager"/> class.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public PartnershipManager(Repository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// The invite friend.
        /// </summary>
        /// <param name="repositoryToInvite">
        /// The repository to invite.
        /// </param>
        public void InviteFriend(Repository repositoryToInvite)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var relatingWithAccept = new Partnership
                                         {
                                             IsAccept = true,
                                             RelatingFromRepositoryId = this.repository.RepositoryId,
                                             RelatingToRepositoryId = repositoryToInvite.RepositoryId
                                         };
                    var relatingWithInvite = new Partnership
                                                 {
                                                     IsAccept = false,
                                                     RelatingFromRepositoryId = repositoryToInvite.RepositoryId,
                                                     RelatingToRepositoryId = this.repository.RepositoryId
                                                 };

                    session.Save(relatingWithAccept);
                    session.Save(relatingWithInvite);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// The remove friend.
        /// </summary>
        /// <param name="repositoryToDelete">
        /// The repository to delete.
        /// </param>
        public void RemoveFriend(Repository repositoryToDelete)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(
                        session.CreateCriteria(typeof(Partnership))
                            .Add(Restrictions.Eq("RelatingFromRepositoryId", this.repository.RepositoryId))
                            .Add(Restrictions.Eq("RelatingToRepositoryId", repositoryToDelete.RepositoryId))
                            .UniqueResult<Partnership>());
                    session.Delete(
                        session.CreateCriteria(typeof(Partnership))
                            .Add(Restrictions.Eq("RelatingFromRepositoryId", repositoryToDelete.RepositoryId))
                            .Add(Restrictions.Eq("RelatingToRepositoryId", this.repository.RepositoryId))
                            .UniqueResult<Partnership>());
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// The accept invitation.
        /// </summary>
        /// <param name="repositoryToAccept">
        /// The repository to accept.
        /// </param>
        public void AcceptInvitation(Repository repositoryToAccept)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var invitation =
                        session.CreateCriteria(typeof(Partnership))
                            .Add(Restrictions.Eq("RelatingFromRepositoryId", repositoryToAccept.RepositoryId))
                            .Add(Restrictions.Eq("RelatingToRepositoryId", this.repository.RepositoryId))
                            .UniqueResult<Partnership>();
                    invitation.IsAccept = true;
                    session.SaveOrUpdate(invitation);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// The get invitation.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Repository> GetInvitation()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var listPartnership = session.CreateCriteria(typeof(Partnership))
                    .Add(Restrictions.Eq("RelatingFromRepositoryId", this.repository.RepositoryId))
                    .Add(Restrictions.Eq("IsAccept", false)).List<Partnership>();

                var listRepository = new List<Repository>();
                foreach (var repositoryId in listPartnership)
                {
                    listRepository.Add(session.CreateCriteria(typeof(Repository))
                       .Add(Restrictions.Eq("RepositoryId", repositoryId.RelatingToRepositoryId))
                       .UniqueResult<Repository>());
                }
                return listRepository;
            }
        }

        /// <summary>
        /// The get friends.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Repository> GetFriends()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var listPartnership = session.CreateCriteria(typeof(Partnership))
                    .Add(Restrictions.Eq("RelatingToRepositoryId", this.repository.RepositoryId))
                    .Add(Restrictions.Eq("IsAccept", true)).List<Partnership>();

                var listRepository = new List<Repository>();
                foreach (var repositoryId in listPartnership)
                {
                    listRepository.Add(session.CreateCriteria(typeof(Repository))
                       .Add(Restrictions.Eq("RepositoryId", repositoryId.RelatingFromRepositoryId))
                       .UniqueResult<Repository>());
                }

                return listRepository;
            }
        }
    }
}
