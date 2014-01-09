

namespace FileSystemDAL
{
    using System.Collections.Generic;
    using FileSystemDAL.Models;

    /// <summary>
    /// The FileSystemRepository interface.
    /// </summary>
    public interface IFileSystemRepository
    {
        /*
        #region GetUser
        /// <summary>
        /// The get user.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <returns>
        /// The <see cref="Users"/>.
        /// </returns>
        Users GetUser(string email);
        #endregion

        #region GetRepository

        /// <summary>
        /// The get repository.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="Repository"/>.
        /// </returns>
        Repository GetRepository(Users user);
        #endregion

        #region GetRepositoryList

        /// <summary>
        /// The get repository list.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        List<Repository> GetRepositoryList();
        #endregion

        #region GetPermission

        /// <summary>
        /// The get permission.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="Permission"/>.
        /// </returns>
        Permission GetPermission(Users user);
        #endregion

        #region GetPermission

        /// <summary>
        /// The get permission.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        /// <returns>
        /// The <see cref="Permission"/>.
        /// </returns>
        Permission GetPermission(Files file);
        #endregion

        #region GetPermission

        /// <summary>
        /// The get permission.
        /// </summary>
        /// <param name="folder">
        /// The folder.
        /// </param>
        /// <returns>
        /// The <see cref="Permission"/>.
        /// </returns>
        Permission GetPermission(Folder folder);
        #endregion

        #region GetFileList

        /// <summary>
        /// The get file list.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="folder">
        /// The folder.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        List<Files> GetFileList(Users user, Folder folder);
        #endregion

        #region GetFolderList

        /// <summary>
        /// The get folder list.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="folder">
        /// The folder.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        List<Folder> GetFolderList(Users user, Folder folder);
        #endregion

        #region AddUserToRepository

        /// <summary>
        /// The add user to repository.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="repository">
        /// The repository.
        /// </param>
        void AddUserToRepository(Users user, Repository repository);
        #endregion

        #region RegisterRepository

        /// <summary>
        /// The register repository.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        void RegisterRepository(string name);
        #endregion

        #region GetRepositoryFriendship

        /// <summary>
        /// The get repository friendship.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        List<Repository> GetRepositoryFriendship(Repository repository);
        #endregion
         * */
    }
}
