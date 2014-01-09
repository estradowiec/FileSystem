
namespace FileSystemDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Text;
    using FileSystemDAL.Enum;
    using FileSystemDAL.Models;
    using FileSystemLogger;

    /// <summary>
    /// The file system repository.
    /// </summary>
    ///
    /*
    public class FileSystemRepository : IFileSystemRepository
    {
        #region Members

        /// <summary>
        /// The connection string.
        /// </summary>
        private readonly string connectionString;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemRepository"/> class.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        public FileSystemRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        #endregion

        #region GetUser

        /// <summary>
        /// The get user.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <returns>
        /// The <see cref="Users"/>.
        /// </returns>
        public Users GetUser(string email)
        {
            Log.Info("GetUser");
            SqlConnection sqlConnection = null;
            SqlCommand sqlRequest = null;

            var sqlCommand = new StringBuilder();
            var user = new Users();

            sqlCommand.AppendFormat(
                "select UserId, UserName, Password, Email, RepositoryID, PermissionId, DateAttach from Users where EMail = '{0}';",
                email);

            try
            {
                sqlConnection = new SqlConnection(this.connectionString);
                sqlConnection.Open();

                sqlRequest = new SqlCommand(sqlCommand.ToString(), sqlConnection);

                var dataReader = sqlRequest.ExecuteReader();

                if (dataReader.Read())
                {
                    user.UserId = Guid.Parse(dataReader["UserId"].ToString());
                    user.UserName = dataReader["UserName"].ToString();
                    user.Password = dataReader["Password"].ToString();
                    user.Email = dataReader["Email"].ToString();
                    user.RepositoryId = Guid.Parse(dataReader["RepositoryID"].ToString());
                    user.PermissionId = Guid.Parse(dataReader["PermissionId"].ToString());
                    user.DateAttach = DateTime.Parse(dataReader["DateAttach"].ToString());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetUser error!", ex);
                throw;
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Dispose();
                }

                if (sqlRequest != null)
                {
                    sqlRequest.Dispose();
                }
            }

            return user;
        }
        #endregion

        #region GetRepository

        /// <summary>
        /// The get repository.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="Repository"/>.
        /// </returns>
        public Repository GetRepository(Users user)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlRequest = null;

            var sqlCommand = new StringBuilder();
            var repository = new Repository();

            sqlCommand.AppendFormat(
                @"select Repository.RepositoryID, Repository.RepositoryName, Repository.DateAttach, Repository.IsActive
                from Repository  inner join Users on {0} = Repository.RepositoryID;", 
                                                                                    user.RepositoryId);

            try
            {
                sqlConnection = new SqlConnection(this.connectionString);
                sqlConnection.Open();

                sqlRequest = new SqlCommand(sqlCommand.ToString(), sqlConnection);

                var dataReader = sqlRequest.ExecuteReader();

                if (dataReader.Read())
                {
                    repository.RepositoryId = Guid.Parse(dataReader["RepositoryID"].ToString());
                    repository.RepositoryName = dataReader["RepositoryName"].ToString();
                    repository.DateAttach = DateTime.Parse(dataReader["DateAttach"].ToString());
                    repository.IsActive = Convert.ToBoolean(dataReader["IsActive"].ToString());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetRepository error!", ex);
                throw;
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Dispose();
                }

                if (sqlRequest != null)
                {
                    sqlRequest.Dispose();
                }
            }

            return repository;
        }
        #endregion

        #region GetRepositoryList
        public List<Repository> GetRepositoryList()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region GetPermission

        /// <summary>
        /// The get permission.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="Permission"/>.
        /// </returns>
        public Permission GetPermission(Users user)
        {
            var permission = this.GetPermission(user.PermissionId);
            return permission;
        }
        #endregion

        #region GetPermission

        /// <summary>
        /// The get permission.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        /// <returns>
        /// The <see cref="Permission"/>.
        /// </returns>
        public Permission GetPermission(Files file)
        {
            var permission = this.GetPermission(file.PermissionId);
            return permission;
        }
        #endregion

        #region GetPermission

        /// <summary>
        /// The get permission.
        /// </summary>
        /// <param name="folder">
        /// The folder.
        /// </param>
        /// <returns>
        /// The <see cref="Permission"/>.
        /// </returns>
        public Permission GetPermission(Folder folder)
        {
            var permission = this.GetPermission(folder.PermissionId);
            return permission;
        }
        #endregion

        #region GetFileList

        /// <summary>
        /// The get file list.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="folder">
        /// The folder.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Files> GetFileList(Users user, Folder folder)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlRequest = null;

            var listFile = new List<Models.Files>();

            var sqlCommand = new StringBuilder();

            sqlCommand.AppendFormat(
                @"select Files.FileID, Files.FileNames, Files.FileSize, Files.FileExtension, Files.DateAttach, 
                  Files.RepositoryID, Files.StorageID, Files.PermissionId, Files.FolderID from Files 
                    inner join Folder on Files.FolderID = {0}
                    inner join Users on Files.RepositoryID = {1}
                    where Files.PermissionId >= {2};",
                folder.FolderId,
                user.RepositoryId,
                user.PermissionId);

            try
            {
                sqlConnection = new SqlConnection(this.connectionString);
                sqlConnection.Open();

                sqlRequest = new SqlCommand(sqlCommand.ToString(), sqlConnection);

                var dataReader = sqlRequest.ExecuteReader();

                while (dataReader.Read())
                {
                    var file = new Files
                                   {
                                       FileId = Guid.Parse(dataReader["FileID"].ToString()),
                                       FileName = dataReader["FileNames"].ToString(),
                                       FileSize = Convert.ToDecimal(dataReader["FileSize"].ToString()),
                                       FileExtension = dataReader["FileExtension"].ToString(),
                                       DateAttach = DateTime.Parse(dataReader["DateAttach"].ToString()),
                                       RepositoryId = Guid.Parse(dataReader["RepositoryID"].ToString()),
                                       StorageId = Guid.Parse(dataReader["StorageID"].ToString()),
                                       PermissionId = Guid.Parse(dataReader["PermissionId"].ToString()),
                                       FolderId = Guid.Parse(dataReader["FolderID"].ToString())
                                   };

                    listFile.Add(file);
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetRepository error!", ex);
                throw;
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Dispose();
                }

                if (sqlRequest != null)
                {
                    sqlRequest.Dispose();
                }
            }

            return listFile;
        }
        #endregion

        #region GetFolderList

        /// <summary>
        /// The get folder list.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="folder">
        /// The folder.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Folder> GetFolderList(Users user, Folder folder)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlRequest = null;

            var listFolder = new List<Folder>();

            var sqlCommand = new StringBuilder();

            sqlCommand.AppendFormat(
                @"select Folder.FolderID, Folder.FolderName, Folder.DateAttach, Folder.RepositoryID, Folder.PermissionId, Folder.ParrentID  from Folder 
                    inner join Users on Folder.RepositoryID = {0}
                    where Folder.PermissionId >= {1} and
                    Folder.ParrentID = {2};",
                user.RepositoryId,
                user.PermissionId,
                folder.ParentId);

            try
            {
                sqlConnection = new SqlConnection(this.connectionString);
                sqlConnection.Open();

                sqlRequest = new SqlCommand(sqlCommand.ToString(), sqlConnection);

                var dataReader = sqlRequest.ExecuteReader();

                while (dataReader.Read())
                {
                    var newFolder = new Folder
                                        {
                                            FolderId = Guid.Parse(dataReader["FolderID"].ToString()),
                                            FolderName = dataReader["FolderName"].ToString(),
                                            DateAttach = DateTime.Parse(dataReader["DateAttach"].ToString()),
                                            RepositoryId = Guid.Parse(dataReader["RepositoryID"].ToString()),
                                            PermissionId = Guid.Parse(dataReader["PermissionId"].ToString()),
                                            ParentId = Guid.Parse(dataReader["ParentID"].ToString())
                                        };

                    listFolder.Add(newFolder);
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetFolderList error!", ex);
                throw;
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Dispose();
                }

                if (sqlRequest != null)
                {
                    sqlRequest.Dispose();
                }
            }

            return listFolder;
        }
        #endregion

        #region AddUserToRepository

        /// <summary>
        /// The add user to repository.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public void AddUserToRepository(Users user, Repository repository)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlRequest = null;

            var sqlCommand = new StringBuilder();

            sqlCommand.AppendFormat(
                @"insert into Users values(NEWID(), {0}, {1}, {2}, getdate(), {3}, {4});",
                user.UserName,
                user.Password,
                user.Email,
                repository.RepositoryId,
                user.PermissionId);

            try
            {
                sqlConnection = new SqlConnection(this.connectionString);
                sqlConnection.Open();

                sqlRequest = new SqlCommand(sqlCommand.ToString(), sqlConnection);

                sqlRequest.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Log.Error("AddUserToRepository error!", ex);
                throw;
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Dispose();
                }

                if (sqlRequest != null)
                {
                    sqlRequest.Dispose();
                }
            }
        }
        #endregion

        #region RegisterRepository

        /// <summary>
        /// The register repository.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        public void RegisterRepository(string name)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlRequest = null;

            var sqlCommand = new StringBuilder();

            sqlCommand.AppendFormat(
                @"insert into Repository values(NEWID(), {0}, getdate(), 0);",
                name);

            try
            {
                sqlConnection = new SqlConnection(this.connectionString);
                sqlConnection.Open();

                sqlRequest = new SqlCommand(sqlCommand.ToString(), sqlConnection);

                sqlRequest.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Log.Error("RegisterRepository error!", ex);
                throw;
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Dispose();
                }

                if (sqlRequest != null)
                {
                    sqlRequest.Dispose();
                }
            }
        }
        #endregion

        #region GetRepositoryFriendship
        public List<Repository> GetRepositoryFriendship(Repository repository)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region GetPermission

        /// <summary>
        /// The get permission.
        /// </summary>
        /// <param name="permissionId">
        /// The permission id.
        /// </param>
        /// <returns>
        /// The <see cref="Permission"/>.
        /// </returns>
        private Permission GetPermission(Guid permissionId)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlRequest = null;

            var sqlCommand = new StringBuilder();
            var permission = new Permission();

            sqlCommand.AppendFormat(
                @"select PermissionId, PermissionValue where PermissionId = {0}",
                permissionId.ToString());

            try
            {
                sqlConnection = new SqlConnection(this.connectionString);
                sqlConnection.Open();

                sqlRequest = new SqlCommand(sqlCommand.ToString(), sqlConnection);

                var dataReader = sqlRequest.ExecuteReader();

                if (dataReader.Read())
                {
                    permission.PermissionId = new Guid(dataReader["PermissionId"].ToString());
                    permission.PermissionValue = (EPermission)Convert.ToInt32(dataReader["PermissionValue"].ToString());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetPermission error!", ex);
                throw;
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Dispose();
                }

                if (sqlRequest != null)
                {
                    sqlRequest.Dispose();
                }
            }

            return permission;
        }
        #endregion
    }
    */
}
