

namespace FileSystemDAL.Manage
{
    using System.Collections.Generic;
    using FileSystemDAL.Enum;
    using FileSystemDAL.Helper;
    using FileSystemDAL.Models;
    using NHibernate.Criterion;

    /// <summary>
    /// The repository manager.
    /// </summary>
    public sealed class RepositoryManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryManager"/> class.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public RepositoryManager(Repository repository, EPermission permission)
        {
            this.Repository = repository;
            this.Permission = permission;
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        public Repository Repository { get; private set; }

        /// <summary>
        /// Gets the permission.
        /// </summary>
        public EPermission Permission { get; private set; }

        /// <summary>
        /// The get files.
        /// </summary>
        /// <param name="folderId">
        /// The folder id.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<Files> GetFiles(int folderId)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var fileList = session.CreateCriteria(typeof(Files))
                    .Add(Restrictions.Eq("FolderId", folderId))
                    .Add(Restrictions.Eq("RepositoryId", this.Repository.RepositoryId))
                    .Add(Restrictions.Ge("Permission", this.Permission))
                    .List<Files>();
                return fileList;
            }
        }

        /// <summary>
        /// The get folders.
        /// </summary>
        /// <param name="parentFolderId">
        /// The parent folder id.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<Folder> GetFolders(int? parentFolderId)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var folderList = session.CreateCriteria(typeof(Folder))
                    .Add(this.EqOrNull("ParrentId", parentFolderId))
                    .Add(Restrictions.Eq("RepositoryId", this.Repository.RepositoryId))
                    .Add(Restrictions.Ge("Permission", this.Permission))
                    .List<Folder>();
                return folderList;
            }
        }

        /// <summary>
        /// The get files.
        /// </summary>
        /// <param name="folderId">
        /// The folder id.
        /// </param>
        /// <param name="repositoryId">
        /// The repository id.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<Files> GetFiles(int folderId, int repositoryId)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var fileList = session.CreateCriteria(typeof(Files))
                    .Add(Restrictions.Eq("FolderId", folderId))
                    .Add(Restrictions.Eq("RepositoryId", repositoryId))
                    .Add(Restrictions.Ge("Permission", this.Permission))
                    .List<Files>();
                return fileList;
            }
        }

        /// <summary>
        /// The get folders.
        /// </summary>
        /// <param name="parentFolderId">
        /// The parent folder id.
        /// </param>
        /// <param name="repositoryId">
        /// The repository id.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<Folder> GetFolders(int? parentFolderId, int repositoryId)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var folderList = session.CreateCriteria(typeof(Folder))
                    .Add(this.EqOrNull("ParrentId", parentFolderId))
                    .Add(Restrictions.Eq("RepositoryId", repositoryId))
                    .Add(Restrictions.Ge("Permission", this.Permission))
                    .List<Folder>();
                return folderList;
            }
        }

        /// <summary>
        /// The eq or null.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="AbstractCriterion"/>.
        /// </returns>
        private AbstractCriterion EqOrNull(string property, object value)
        {
            if (value == null)
            {
                return Restrictions.IsNull(property);
            }

            return Restrictions.Eq(property, value);
        }
    }
}
