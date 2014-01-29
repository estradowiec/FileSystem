
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
    public class Admin
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
        /// <param name="repositoryId">
        /// The repository id.
        /// </param>
        public void DeleteRepository(int repositoryId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                   var files = session.CreateCriteria(typeof(Files))
                        .Add(Restrictions.Eq("RepositoryId", repositoryId))
                        .List<Files>();
                    foreach (var file in files)
                    {
                        session.Delete(string.Format("from {0} where FileID = {1}", typeof(SharedFile), file.FileId));
                    }

                    var folders = session.CreateCriteria(typeof(Folder))
                        .Add(Restrictions.Eq("RepositoryId", repositoryId))
                        .List<Folder>();
                    foreach (var folder in folders)
                    {
                        session.Delete(string.Format("from {0} where FolderID = {1}", typeof(SharedFolder), folder.FolderId));
                    }

                    session.Delete(string.Format("from {0} where RepositoryID = {1}", typeof(Files), repositoryId));
                    session.Delete(string.Format("from {0} where RepositoryID = {1}", typeof(Folder), repositoryId));
                    session.Delete(string.Format("from {0} where RepositoryID = {1}", typeof(Person), repositoryId));
                    session.Delete(string.Format("from {0} where RelatingFromRepositoryID = {1} or RelatingToRepositoryID = {2}", typeof(Partnership), repositoryId, repositoryId));
                    session.Delete(string.Format("from {0} where RepositoryID = {1}", typeof(Repository), repositoryId));

                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// The active repository.
        /// </summary>
        /// <param name="repositoryId">
        /// The repository id.
        /// </param>
        public void ActiveRepository(int repositoryId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var repository =
                        session.CreateCriteria(typeof(Repository))
                            .Add(Restrictions.Eq("RepositoryId", repositoryId))
                            .UniqueResult<Repository>();
                    repository.IsActive = true;
                    session.SaveOrUpdate(repository);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// The deactive repository.
        /// </summary>
        /// <param name="repositoryId">
        /// The repository id.
        /// </param>
        public void DeactiveRepository(int repositoryId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var repository =
                        session.CreateCriteria(typeof(Repository))
                            .Add(Restrictions.Eq("RepositoryId", repositoryId))
                            .UniqueResult<Repository>();
                    repository.IsActive = false;
                    session.SaveOrUpdate(repository);
                    transaction.Commit();
                }
            }
        }
    }
}
