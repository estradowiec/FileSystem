
namespace FileSystemDAL.Manage
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;

    using FileSystemDAL.Helper;
    using FileSystemDAL.Models;

    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.Util;

    /// <summary>
    /// The patnership manager.
    /// </summary>
    public class PartnershipManager
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
        /// <param name="repositoryId">
        /// The repository id.
        /// </param>
        public void InviteFriend(int repositoryId)
        {
            if (!this.IsRepositoryExist(repositoryId))
            {
                return;
            }

            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var relatingWithAccept = new Partnership
                                                 {
                                                     IsAccept = true,
                                                     RelatingFromRepositoryId = this.repository.RepositoryId,
                                                     RelatingToRepositoryId = repositoryId
                                                 };
                    var relatingWithInvite = new Partnership
                                                 {
                                                     IsAccept = false,
                                                     RelatingFromRepositoryId = repositoryId,
                                                     RelatingToRepositoryId = this.repository.RepositoryId
                                                 };

                    session.Save(relatingWithAccept);
                    session.Save(relatingWithInvite);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// The search repository.
        /// </summary>
        /// <param name="repositoryName">
        /// The repository name.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<Repository> SearchRepository(string repositoryName)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var repositoryList =
                    session.CreateCriteria(typeof(Repository))
                    .Add(Restrictions.InsensitiveLike("RepositoryName", repositoryName, MatchMode.Anywhere))
                    .Add(Restrictions.Not(Restrictions.Eq("RepositoryId", this.repository.RepositoryId)))
                        .List<Repository>();

                return repositoryList;
            }
        }

        /// <summary>
        /// The remove friend.
        /// </summary>
        /// <param name="repositoryId">
        /// The repository to delete.
        /// </param>
        public void RemoveFriend(int repositoryId)
        {
            if (!this.IsRepositoryExist(repositoryId))
            {
                return;
            }

            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(
                            session.CreateCriteria(typeof(Partnership))
                                .Add(Restrictions.Eq("RelatingFromRepositoryId", this.repository.RepositoryId))
                                .Add(Restrictions.Eq("RelatingToRepositoryId", repositoryId))
                                .UniqueResult<Partnership>());
                        session.Delete(
                            session.CreateCriteria(typeof(Partnership))
                                .Add(Restrictions.Eq("RelatingFromRepositoryId", repositoryId))
                                .Add(Restrictions.Eq("RelatingToRepositoryId", this.repository.RepositoryId))
                                .UniqueResult<Partnership>());
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        ////TODO: Log exception
                    }
                }
            }
        }

        /// <summary>
        /// The accept invitation.
        /// </summary>
        /// <param name="repositoryId">
        /// The repository id.
        /// </param>
        public void AcceptInvitation(int repositoryId)
        {
            if (!this.IsRepositoryExist(repositoryId))
            {
                return;
            }

            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var invitation =
                        session.CreateCriteria(typeof(Partnership))
                            .Add(Restrictions.Eq("RelatingFromRepositoryId", this.repository.RepositoryId))
                            .Add(Restrictions.Eq("RelatingToRepositoryId", repositoryId))
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
        /// The get invitation.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Repository> GetSentInvitation()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var listPartnership = session.CreateCriteria(typeof(Partnership))
                    .Add(Restrictions.Eq("RelatingToRepositoryId", this.repository.RepositoryId))
                    .Add(Restrictions.Eq("IsAccept", false)).List<Partnership>();

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

        /// <summary>
        /// The is repository exist.
        /// </summary>
        /// <param name="repositoryId">
        /// The repository id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool IsRepositoryExist(int repositoryId)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var exist = session.QueryOver<Repository>().Where(x => x.RepositoryId == repositoryId).List().Any();
                return exist;
            }
        }
    }
}
