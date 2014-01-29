#region Copyright
//-----------------------------------------------------------------------------
// <copyright file="EPermission.cs" company="tnocon">
//     Copyright (c) Tomasz Nocoń All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
#endregion

namespace FileSystemDAL.Enum
{
    /// <summary>
    /// The e permission.
    /// </summary>
    public enum EPermission
    {
        /// <summary>
        /// The admin.
        /// </summary>
        Admin = 0,

        /// <summary>
        /// The repository admin.
        /// </summary>
        RepositoryAdmin = 1,

        /// <summary>
        /// The user high.
        /// </summary>
        UserHigh = 2,

        /// <summary>
        /// The user low.
        /// </summary>
        UserMedium = 3,

        /// <summary>
        /// The user low.
        /// </summary>
        UserLow = 4
    }
}
