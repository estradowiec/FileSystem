
namespace FileSystemDAL.Manage
{
    using System.Collections;
    using System.Collections.Generic;
    using FileSystemDAL.Enum;
    using FileSystemDAL.Helper;
    using FileSystemDAL.Models;
    using NHibernate;
    using NHibernate.Transform;

    /// <summary>
    /// The friends repository.
    /// </summary>
    public class FriendRepository
    {
        /// <summary>
        /// The permission.
        /// </summary>
        private readonly EPermission permission;

        /// <summary>
        /// The my repository id.
        /// </summary>
        private readonly int myRepositoryId;

        /// <summary>
        /// Initializes a new instance of the <see cref="FriendRepository"/> class.
        /// </summary>
        /// <param name="permission">
        /// The permission.
        /// </param>
        /// <param name="myRepositoryId">
        /// The my repository id.
        /// </param>
        public FriendRepository(EPermission permission, int myRepositoryId)
        {
            this.permission = permission;
            this.myRepositoryId = myRepositoryId;
        }

        /// <summary>
        /// The get friends files.
        /// </summary>
        /// <param name="folderId">
        /// The folder id.
        /// </param>
        /// <param name="friendRepositoryId">
        /// The friend repository id.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<Files> GetFriendsFiles(int? folderId, int friendRepositoryId)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                string select;
                if (folderId.HasValue)
                {
                    select =
                        string.Format(
                            "select Files.* from SharedFile inner join Files ON SharedFile.FileID=Files.FileID where"
                            + " Files.RepositoryID={0} and SharedFile.RepositoryId={1} and Files.FolderID={2} and Files.Permission>={3}",
                            friendRepositoryId,
                            this.myRepositoryId,
                            folderId,
                            (int)this.permission);
                }
                else
                {
                    select =
                        string.Format(
                            "select Files.* from SharedFile inner join Files ON SharedFile.FileID=Files.FileID where"
                            + " Files.RepositoryID={0} and SharedFile.RepositoryId={1} and Files.FolderID is NULL and Files.Permission>={2}",
                            friendRepositoryId,
                            this.myRepositoryId,
                            (int)this.permission);
                }

                var query =
                    session.CreateSQLQuery(select)
                        .AddScalar("FileId", NHibernateUtil.Int32)
                        .AddScalar("FileNames", NHibernateUtil.String)
                        .AddScalar("FileSize", NHibernateUtil.Decimal)
                        .AddScalar("DateAttach", NHibernateUtil.DateTime)
                        .AddScalar("Permission", NHibernateUtil.Int32)
                        .AddScalar("RepositoryId", NHibernateUtil.Int32)
                        .AddScalar("FolderId", NHibernateUtil.Int32)
                        .SetResultTransformer(Transformers.AliasToBean(typeof(Files)));

                var friendFiles = query.List<Files>();

                return friendFiles;
            }
        }

        /// <summary>
        /// The get friends folders.
        /// </summary>
        /// <param name="parentFolderId">
        /// The parent folder id.
        /// </param>
        /// <param name="friendRepositoryId">
        /// The friend repository id.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<Folder> GetFriendsFolders(int? parentFolderId, int friendRepositoryId)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                string select;
                if (parentFolderId.HasValue)
                {
                    select =
                        string.Format(
                            "select Folder.* from SharedFolder inner join Folder ON SharedFolder.FolderID=Folder.FolderID where"
                            + " Folder.RepositoryID={0} and SharedFolder.RepositoryId={1} and Folder.ParrentID={2} and Folder.Permission>={3}",
                            friendRepositoryId,
                            this.myRepositoryId,
                            parentFolderId,
                            (int)this.permission);
                }
                else
                {
                    select =
                        string.Format(
                            "select Folder.* from SharedFolder inner join Folder ON SharedFolder.FolderID=Folder.FolderID where"
                            + " Folder.RepositoryID={0} and SharedFolder.RepositoryId={1} and Folder.ParrentID is NULL and Folder.Permission>={2}",
                            friendRepositoryId,
                            this.myRepositoryId,
                            (int)this.permission);
                }

                var query =
                    session.CreateSQLQuery(select)
                        .AddScalar("FolderId", NHibernateUtil.Int32)
                        .AddScalar("FolderName", NHibernateUtil.String)
                        .AddScalar("DateAttach", NHibernateUtil.DateTime)
                        .AddScalar("Permission", NHibernateUtil.Int32)
                        .AddScalar("RepositoryId", NHibernateUtil.Int32)
                        .AddScalar("ParrentId", NHibernateUtil.Int32)
                        .SetResultTransformer(Transformers.AliasToBean(typeof(Folder)));

                var friendFiles = query.List<Folder>();

                return friendFiles;
            }
        }
    }
}
