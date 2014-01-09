
namespace FileSystemDAL.Models
{
    /// <summary>
    /// The shared file.
    /// </summary>
    public class SharedFile
    {
        /// <summary>
        /// Gets or sets the shared file id.
        /// </summary>
        public virtual int SharedFileId { get; set; }

        /// <summary>
        /// Gets or sets the file id.
        /// </summary>
        public virtual int FileId { get; set; }

        /// <summary>
        /// Gets or sets the repository id.
        /// </summary>
        public virtual int RepositoryId { get; set; }
    }
}
