using System;
using System.Collections.Generic;

namespace FileSystemDAL.Manage
{
    using FileSystemDAL.Enum;
    using FileSystemDAL.Helper;
    using FileSystemDAL.Models;

    using NHibernate.Criterion;

    /// <summary>
    /// The repository manager.
    /// </summary>
    public sealed class RepositoryManager : IRepositoryManager
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
        /// <param name="folder">
        /// The folder.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<Files> GetFiles(Folder folder)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var fileList = session.CreateCriteria(typeof(Files))
                    .Add(Restrictions.Eq("FolderId", folder.FolderId))
                    .Add(Restrictions.Eq("RepositoryId", this.Repository.RepositoryId))
                    .Add(Restrictions.Ge("Permission", this.Permission))
                    .List<Files>();
                return fileList;
            }
        }

        /// <summary>
        /// The get folders.
        /// </summary>
        /// <param name="folder">
        /// The folder.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<Folder> GetFolders(Folder folder)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var folderList = session.CreateCriteria(typeof(Folder))
                    .Add(Restrictions.Eq("ParrentId", folder.ParrentId))
                    .Add(Restrictions.Eq("RepositoryId", this.Repository.RepositoryId))
                    .Add(Restrictions.Ge("Permission", this.Permission))
                    .List<Folder>();
                return folderList;
            }
        }

        /// <summary>
        /// The get files.
        /// </summary>
        /// <param name="folder">
        /// The folder.
        /// </param>
        /// <param name="repository">
        /// The repository.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<Files> GetFiles(Folder folder, Repository repository)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var fileList = session.CreateCriteria(typeof(Files))
                    .Add(Restrictions.Eq("FolderId", folder.FolderId))
                    .Add(Restrictions.Eq("RepositoryId", repository.RepositoryId))
                    .Add(Restrictions.Ge("Permission", this.Permission))
                    .List<Files>();
                return fileList;
            }
        }

        /// <summary>
        /// The get folders.
        /// </summary>
        /// <param name="folder">
        /// The folder.
        /// </param>
        /// <param name="repository">
        /// The repository.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<Folder> GetFolders(Folder folder, Repository repository)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var folderList = session.CreateCriteria(typeof(Folder))
                    .Add(Restrictions.Eq("ParrentId", folder.ParrentId))
                    .Add(Restrictions.Eq("RepositoryId", repository.RepositoryId))
                    .Add(Restrictions.Ge("Permission", this.Permission))
                    .List<Folder>();
                return folderList;
            }
        }
    }
}
