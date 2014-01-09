
namespace FileSystemDAL.Manage
{
    using System;
    using System.Collections.Generic;

    using FileSystemDAL.Models;

    /// <summary>
    /// The Respository interface.
    /// </summary>
    public interface IRepositoryManager
    {
        /// <summary>
        /// The get files.
        /// </summary>
        /// <param name="folder">
        /// The folder.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        IList<Files> GetFiles(Folder folder);

        /// <summary>
        /// The get folders.
        /// </summary>
        /// <param name="folder">
        /// The folder.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        IList<Folder> GetFolders(Folder folder);
    }
}
