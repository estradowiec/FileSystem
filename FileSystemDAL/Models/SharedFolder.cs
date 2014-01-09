
namespace FileSystemDAL.Models
{
    /// <summary>
    /// The shared folder.
    /// </summary>
    public class SharedFolder
    {
        /// <summary>
        /// Gets or sets the shared folder id.
        /// </summary>
        public virtual int SharedFolderId { get; set; }

        /// <summary>
        /// Gets or sets the folder id.
        /// </summary>
        public virtual int FolderId { get; set; }

        /// <summary>
        /// Gets or sets the repository id.
        /// </summary>
        public virtual int RepositoryId { get; set; }
    }
}
