namespace FileSystemDAL.Manage
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FileSystemDAL.Enum;
    using FileSystemDAL.Helper;
    using FileSystemDAL.Models;

    using Microsoft.AspNet.Identity;

    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.Hql.Ast.ANTLR;
    using NHibernate.Transform;

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
        /// The validate folder name async.
        /// </summary>
        /// <param name="parentFolderId">
        /// The parent folder id.
        /// </param>
        /// <param name="folderName">
        /// The folder name.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public IdentityResult ValidateFolderNameAsync(int? parentFolderId, string folderName)
        {
            var errors = new List<string>();
            using (var session = NHibernateHelper.OpenSession())
            {
                var exist =
                    session.CreateCriteria<Folder>()
                        .Add(Restrictions.Eq("FolderName", folderName))
                        .Add(Restrictions.Eq("RepositoryId", this.Repository.RepositoryId))
                        .Add(this.EqOrNull("ParrentId", parentFolderId))
                        .List<Folder>()
                        .Any();

                if (exist)
                {
                    errors.Add("Folder name exist in repository. Enter different name.");
                }
            }

            return errors.Any() ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }

        /// <summary>
        /// The get shared files.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<SharedFile> GetSharedFiles()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var query =
                    session.CreateSQLQuery(
                        string.Format(
                            "select SharedFile.* from SharedFile inner join Files ON SharedFile.FileID=Files.FileID where Files.RepositoryID={0}",
                            this.Repository.RepositoryId))
                        .AddScalar("SharedFileId", NHibernateUtil.Int32)
                        .AddScalar("FileId", NHibernateUtil.Int32)
                        .AddScalar("RepositoryId", NHibernateUtil.Int32)
                        .SetResultTransformer(Transformers.AliasToBean(typeof(SharedFile)));

                var sharedFiles = query.List<SharedFile>();

                return sharedFiles;
            }
        }

        /// <summary>
        /// The get shared files.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<SharedFolder> GetSharedFolders()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var query =
                    session.CreateSQLQuery(
                        string.Format(
                            "select SharedFolder.* from SharedFolder inner join Folder ON SharedFolder.FolderID=Folder.FolderID where Folder.RepositoryID={0}",
                            this.Repository.RepositoryId))
                        .AddScalar("SharedFolderId", NHibernateUtil.Int32)
                        .AddScalar("FolderId", NHibernateUtil.Int32)
                        .AddScalar("RepositoryId", NHibernateUtil.Int32)
                        .SetResultTransformer(Transformers.AliasToBean(typeof(SharedFolder)));

                var sharedFolder = query.List<SharedFolder>();

                return sharedFolder;
            }
        }

        /// <summary>
        /// The get files.
        /// </summary>
        /// <param name="folderId">
        /// The folder id.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<Files> GetFiles(int? folderId)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var fileList = session.CreateCriteria(typeof(Files))
                    .Add(this.EqOrNull("FolderId", folderId))
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
