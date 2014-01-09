#region Copyright
//-----------------------------------------------------------------------------
// <copyright file="Folder.cs" company="tnocon">
//     Copyright (c) Tomasz Nocoń All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
#endregion

namespace FileSystemDAL.Models
{
    using System;

    using FileSystemDAL.Enum;

    /// <summary>
    /// The folder.
    /// </summary>
    public class Folder: IEquatable<Folder>
    {
        /// <summary>
        /// Gets or sets the folder id.
        /// </summary>
        public virtual int FolderId { get; set; }

        /// <summary>
        /// Gets or sets the folder name.
        /// </summary>
        public virtual string FolderName { get; set; }

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
        /// Gets or sets the parent id.
        /// </summary>
        public virtual int? ParrentId { get; set; }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (Folder)obj;
            return this.FolderId.Equals(other.FolderId) && this.FolderName.Equals(other.FolderName)
                && this.ParrentId.Equals(other.ParrentId) && this.Permission.Equals(other.Permission)
                && this.RepositoryId.Equals(other.RepositoryId);
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool Equals(Folder other)
        {
            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            return this.FolderId.Equals(other.FolderId) && this.FolderName.Equals(other.FolderName)
                   && this.ParrentId.Equals(other.ParrentId) && this.Permission.Equals(other.Permission)
                   && this.RepositoryId.Equals(other.RepositoryId);
        }
    }
}
