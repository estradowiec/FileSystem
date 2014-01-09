
namespace FileSystemDAL.Manage
{
    using FileSystemDAL.Models;

    /// <summary>
    /// The PersonRepository interface.
    /// </summary>
    public interface IPersonRepository
    {
        void DeleteFile(int storageId);

        /// <summary>
        /// The share file.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        /// <param name="repository">
        /// The repository.
        /// </param>
        void ShareFile(Files file, Repository repository);

        /// <summary>
        /// The share folder.
        /// </summary>
        /// <param name="folder">
        /// The folder.
        /// </param>
        /// <param name="repository">
        /// The repository.
        /// </param>
        void ShareFolder(Folder folder, Repository repository);
    }
}
