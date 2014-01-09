#region Copyright
//-----------------------------------------------------------------------------
// <copyright file="Person.cs" company="tnocon">
//     Copyright (c) Tomasz Nocoń All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
#endregion

namespace FileSystemDAL.Models
{
    using System;

    /// <summary>
    /// The user.
    /// </summary>
    public class Person : PersonIdenty
    {
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// Gets or sets the date attach.
        /// </summary>
        public virtual DateTime DateAttach { get; set; }

        /// <summary>
        /// Gets or sets the repository id.
        /// </summary>
        public virtual int RepositoryId { get; set; }
    }
}
