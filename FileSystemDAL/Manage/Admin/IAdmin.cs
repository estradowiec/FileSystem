
namespace FileSystemDAL.Manage
{
    using FileSystemDAL.Models;

    /// <summary>
    /// The Admin interface.
    /// </summary>
    public interface IAdmin
    {
        /// <summary>
        /// The delete repository.
        /// </summary>
        /// <param name="respository">
        /// The respository.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        void DeleteRepository(Repository respository);

        /// <summary>
        /// The active repository.
        /// </summary>
        /// <param name="respository">
        /// The respository.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        void ActiveRepository(Repository respository);

        /// <summary>
        /// The deactive repository.
        /// </summary>
        /// <param name="respository">
        /// The respository.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        void DeactiveRepository(Repository respository);
    }
}
