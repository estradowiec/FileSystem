#region Copyright
//-----------------------------------------------------------------------------
// <copyright file="Files.cs" company="tnocon">
//     Copyright (c) Tomasz Nocoń All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
#endregion

namespace FileSystemDAL.Models
{
    using System;
    using FileSystemDAL.Enum;

    /// <summary>
    /// The file.
    /// </summary>
    public class Files : IEquatable<Files>
    {
        /// <summary>
        /// Gets or sets the file id.
        /// </summary>
        public virtual int FileId { get; set; }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        public virtual string FileNames { get; set; }

        /// <summary>
        /// Gets or sets the file size.
        /// </summary>
        public virtual decimal FileSize { get; set; }

        /// <summary>
        /// Gets or sets the date attach.
        /// </summary>
        public virtual DateTime DateAttach { get; set; }

        /// <summary>
        /// Gets or sets the company id.
        /// </summary>
        public virtual int RepositoryId { get; set; }

        /// <summary>
        /// Gets or sets the permission id.
        /// </summary>
        public virtual EPermission Permission { get; set; }

        /// <summary>
        /// Gets or sets the folder id.
        /// </summary>
        public virtual int? FolderId { get; set; }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool Equals(Files other)
        {
            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            return this.FileId.Equals(other.FileId) && this.FileNames.Equals(other.FileNames)
                && this.FileSize.Equals(other.FileSize) && this.FolderId.Equals(other.FolderId)
                && this.RepositoryId.Equals(other.RepositoryId);
        }
    }
}
