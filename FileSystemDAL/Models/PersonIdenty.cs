#region Copyright
//-----------------------------------------------------------------------------
// <copyright file="PersonIdenty.cs" company="tnocon">
//     Copyright (c) Tomasz Nocoń All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
#endregion

namespace FileSystemDAL.Models
{
    using FileSystemDAL.Enum;

    /// <summary>
    /// The person identy.
    /// </summary>
    public class PersonIdenty
    {
        /// <summary>
        /// Gets or sets the person id.
        /// </summary>
        public virtual int PersonId { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets the permission.
        /// </summary>
        public virtual EPermission Permission { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public virtual string PersonName { get; set; }
    }
}
