#region Copyright
//-----------------------------------------------------------------------------
// <copyright file="Permission.cs" company="tnocon">
//     Copyright (c) Tomasz Nocoń All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
#endregion

namespace FileSystemDAL.Models
{
    using FileSystemDAL.Enum;

    /// <summary>
    /// The permission.
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// Gets or sets the permission id.
        /// </summary>
        public virtual int PermissionId { get; set; }

        /// <summary>
        /// Gets or sets the permission value.
        /// </summary>
        public virtual EPermission PermissionValue { get; set; }
    }
}
