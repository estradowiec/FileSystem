

namespace FileSystem.Models
{
    using System.Globalization;
    using FileSystemDAL.Enum;
    using FileSystemDAL.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// The application user.
    /// </summary>
    public sealed class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUser"/> class.
        /// </summary>
        /// <param name="person">
        /// The person.
        /// </param>
        public ApplicationUser(Person person)
        {
            this.Id = person.PersonId.ToString(CultureInfo.InvariantCulture);
            this.UserName = person.PersonName;
            this.PasswordHash = person.Password;
            this.Email = person.Email;
            this.Permission = person.Permission;
        }

        /// <summary>
        /// Gets the email.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Gets the permission.
        /// </summary>
        public EPermission Permission { get; private set; }
    }
}