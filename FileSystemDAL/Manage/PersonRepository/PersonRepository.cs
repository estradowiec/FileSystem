
namespace FileSystemDAL.Manage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using FileSystemDAL.Enum;
    using FileSystemDAL.Helper;
    using FileSystemDAL.Models;
    using NHibernate;
    using NHibernate.Criterion;

    /// <summary>
    /// The person repository.
    /// </summary>
    public class PersonRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonRepository"/> class.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        /// <param name="permission">
        /// The permission.
        /// </param>
        public PersonRepository(Repository repository, EPermission permission)
        {
            this.MyRepository = new RepositoryManager(repository, permission);
            this.PartnershipManager = new PartnershipManager(repository);
        }

        /// <summary>
        /// The my repository.
        /// </summary>
        public RepositoryManager MyRepository { get; private set; }

        /// <summary>
        /// Gets the partnership manager.
        /// </summary>
        public PartnershipManager PartnershipManager { get; private set; }

        /// <summary>
        /// The friends repository.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Repository> FriendsRepository()
        {
            return PartnershipManager.GetFriends();
        }

        /// <summary>
        /// The create file storage.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="fileSize">
        /// The file size.
        /// </param>
        /// <param name="folderId">
        /// The folder id.
        /// </param>
        /// <param name="permission">
        /// The permission.
        /// </param>
        /// <returns>
        /// The <see cref="Files"/>.
        /// </returns>
        public Files InitFile(string fileName, decimal fileSize, int folderId, EPermission permission)
        {
            var files = new Files
                            {
                                DateAttach = DateTime.Now,
                                FileExtension = Path.GetExtension(fileName),
                                FileNames = Path.GetFileName(fileName),
                                FolderId = folderId,
                                FileSize = fileSize,
                                Permission = permission,
                                RepositoryId = this.MyRepository.Repository.RepositoryId
                            };

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    files.FileId = (int)session.Save(files);
                    transaction.Commit();
                    return files;
                }
            }
        }

        /// <summary>
        /// The upload file.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="fileId">
        /// The file id.
        /// </param>
        /// <param name="inputStream">
        /// The input stream.
        /// </param>
        public void UploadFile(string path, int fileId, Stream inputStream)
        {
            Files files;
            using (var session = NHibernateHelper.OpenSession())
            {
                files = session.CreateCriteria(typeof(Files)).Add(Restrictions.Eq("FileId", fileId)).UniqueResult<Files>();
            }

            var filePath = GetFilePath(path, files);
            using (var outpuStream = GetFileStream(filePath))
            {
                var buffer = new byte[32768];
                int read;
                outpuStream.Seek(outpuStream.Length, SeekOrigin.Begin);
                while ((read = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    outpuStream.Write(buffer, 0, read);
                }
            }
        }

        /// <summary>
        /// The download file.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="fileId">
        /// The file id.
        /// </param>
        /// <returns>
        /// The <see cref="Stream"/>.
        /// </returns>
        public Stream DownloadFile(string path, int fileId)
        {
            Files files;
            using (var session = NHibernateHelper.OpenSession())
            {
                files = session.CreateCriteria(typeof(Files)).Add(Restrictions.Eq("FileId", fileId)).UniqueResult<Files>();
            }

            var filePath = GetFilePath(path, files);
            return File.Exists(filePath) ? File.Open(filePath, FileMode.Open) : Stream.Null;
        }

        /// <summary>
        /// The delete file.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="fileId">
        /// The file id.
        /// </param>
        public void DeleteFile(string path, int fileId)
        {
            Files files;
            using (var session = NHibernateHelper.OpenSession())
            {
                    files =
                        session.CreateCriteria(typeof(Files))
                            .Add(Restrictions.Eq("FileId", fileId))
                            .UniqueResult<Files>();
                    //session.CreateSQLQuery(string.Format("Delete from Files where FileID ={0}", files.FileId));
            }

            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(files);
                    transaction.Commit();
                }
            }

            var filePath = GetFilePath(path, files);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// The share file.
        /// </summary>
        /// <param name="fileId">
        /// The file id.
        /// </param>
        /// <param name="repositoryId">
        /// The repository id.
        /// </param>
        public void ShareFile(int fileId, int repositoryId)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var sharedFile = new SharedFile
                                           {
                                               RepositoryId = repositoryId,
                                               FileId = fileId
                                           };
                    session.Save(sharedFile);
                    transaction.Commit();
                    ////TODO: Share folder
                }
            }
        }

        /// <summary>
        /// The unshared file.
        /// </summary>
        /// <param name="fileId">
        /// The file id.
        /// </param>
        /// <param name="repositoryId">
        /// The repository id.
        /// </param>
        public void UnsharedFile(int fileId, int repositoryId)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var sharedFile =
                        session.CreateCriteria(typeof(SharedFile))
                            .Add(Restrictions.Eq("RepositoryId", repositoryId))
                            .Add(Restrictions.Eq("FileId", fileId))
                            .UniqueResult<SharedFile>();

                    session.Delete(sharedFile);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// The share folder.
        /// </summary>
        /// <param name="folder">
        /// The folder.
        /// </param>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public void ShareFolder(Folder folder, Repository repository)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var sharedFolder = new SharedFolder
                    {
                        RepositoryId = repository.RepositoryId,
                        FolderId = folder.FolderId
                    };
                    session.Save(sharedFolder);
                    transaction.Commit();
                    ////TODO: Share files
                }
            }
        }

        /// <summary>
        /// The get file stream.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <returns>
        /// The <see cref="FileStream"/>.
        /// </returns>
        private static FileStream GetFileStream(string filePath)
        {
            var fileStream = File.Exists(filePath) ? File.Open(filePath, FileMode.Open) : File.Create(filePath);
            return fileStream;
        }

        /// <summary>
        /// The get file path.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="files">
        /// The files.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetFilePath(string path, Files files)
        {
            var fileName = string.Format("{0}-{1}", files.FileId, files.FileNames);
            return path + fileName;
        }
    }
}
