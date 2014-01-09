
namespace FileSystemDAL.Manage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FileSystemDAL.Helper;
    using FileSystemDAL.Models;

    using NHibernate;
    using NHibernate.Criterion;

    /// <summary>
    /// The admin.
    /// </summary>
    public class Admin : IAdmin
    {
        /// <summary>
        /// The get list repository.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public IList<Repository> GetListRepository()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var listRepository = session.CreateCriteria(typeof(Repository)).List<Repository>();
                return listRepository;
            }
        }

        /// <summary>
        /// The delete repository.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public void DeleteRepository(Repository repository)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var repositoryFromDb =
                        session.CreateCriteria(typeof(Repository))
                            .Add(Restrictions.Eq("RepositoryName", repository.RepositoryName))
                            .UniqueResult<Repository>();

                    session.Delete(repositoryFromDb);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// The active repository.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public void ActiveRepository(Repository repository)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    repository.IsActive = true;
                    session.SaveOrUpdate(repository);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// The deactive repository.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public void DeactiveRepository(Repository repository)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    repository.IsActive = false;
                    session.SaveOrUpdate(repository);
                    transaction.Commit();
                }
            }
        }
    }
}
