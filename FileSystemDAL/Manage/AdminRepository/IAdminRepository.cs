
namespace FileSystemDAL.Manage
{
    using FileSystemDAL.Models;

    /// <summary>
    /// The AdminRepository interface.
    /// </summary>
    public interface IAdminRepository
    {
        /// <summary>
        /// The invite friend.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        void InviteFriend(Repository repository);

        /// <summary>
        /// The remove friend.
        /// </summary>
        /// <param name="repository">
        /// The respository.
        /// </param>
        void RemoveFriend(Repository repository);

        /// <summary>
        /// The accept invitation.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        void AcceptInvitation(Repository repository);
    }
}
