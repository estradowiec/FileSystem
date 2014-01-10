using Microsoft.AspNet.Identity.EntityFramework;

namespace FileSystem.Models
{
    using System.Globalization;
    using FileSystemDAL.Enum;
    using FileSystemDAL.Models;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
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