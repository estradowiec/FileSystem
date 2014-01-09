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
    public class Folder
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
    }
}
