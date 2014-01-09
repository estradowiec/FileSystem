
namespace FileSystemDAL.Models
{
    /// <summary>
    /// The partnership.
    /// </summary>
    public class Partnership
    {
        /// <summary>
        /// Gets or sets the partnership id.
        /// </summary>
        public virtual int PartnershipId { get; set; }

        /// <summary>
        /// Gets or sets the relating repository id.
        /// </summary>
        public virtual int RelatingFromRepositoryId { get; set; }

        /// <summary>
        /// Gets or sets the related company id.
        /// </summary>
        public virtual int RelatingToRepositoryId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is accept.
        /// </summary>
        public virtual bool IsAccept { get; set; }
    }
}
