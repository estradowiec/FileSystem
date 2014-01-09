#region Copyright
//-----------------------------------------------------------------------------
// <copyright file="Company.cs" company="tnocon">
//     Copyright (c) Tomasz Nocoń All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
#endregion

namespace FileSystemDAL.Models
{
    using System;

    using FileSystemDAL.Enum;

    /// <summary>
    /// The company.
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// Gets or sets the company id.
        /// </summary>
        public virtual int RepositoryId { get; set; }

        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        public virtual string RepositoryName { get; set; }

        /// <summary>
        /// Gets or sets the date attach.
        /// </summary>
        public virtual DateTime DateAttach { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public virtual bool IsActive { get; set; }
    }
}
