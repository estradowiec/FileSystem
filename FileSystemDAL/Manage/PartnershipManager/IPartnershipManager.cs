

namespace FileSystemDAL.Manage
{
    using System.Collections.Generic;
    using FileSystemDAL.Models;

    /// <summary>
    /// The PartnershipManager interface.
    /// </summary>
    public interface IPartnershipManager
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
        /// The repository.
        /// </param>
        void RemoveFriend(Repository repository);

        /// <summary>
        /// The accept invitation.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        void AcceptInvitation(Repository repository);

        /// <summary>
        /// The get invitation.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        List<Repository> GetInvitation();

        /// <summary>
        /// The get friends.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        List<Repository> GetFriends();
    }
}
