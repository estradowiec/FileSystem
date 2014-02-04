
namespace FileSystemDAL.Manage
{
    using System;
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
        }

        /// <summary>
        /// The my repository.
        /// </summary>
        public RepositoryManager MyRepository { get; private set; }

        /// <summary>
        /// The create folder.
        /// </summary>
        /// <param name="parrentFolderId">
        /// The parrent folder id.
        /// </param>
        /// <param name="folderName">
        /// The folder name.
        /// </param>
        /// <param name="permission">
        /// The permission.
        /// </param>
        public void CreateFolder(int? folderId, string folderName, EPermission permission)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var folder = new Folder
                                     {
                                         ParrentId = folderId,
                                         FolderName = folderName,
                                         Permission = permission,
                                         RepositoryId = this.MyRepository.Repository.RepositoryId,
                                         DateAttach = DateTime.Now
                                     };

                    session.Save(folder);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// The init file.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="fileHash">
        /// The file hash.
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
        /// The <see cref="int"/>.
        /// </returns>
        public int InitFile(string fileName, decimal fileSize, int? folderId, EPermission permission)
        {
            var fileUpload = new FileUpload
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
                    var fileId = (int)session.Save(fileUpload);
                    transaction.Commit();
                    return fileId;
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
            FileUpload file;
            using (var session = NHibernateHelper.OpenSession())
            {
                file = session.CreateCriteria(typeof(FileUpload)).Add(Restrictions.Eq("FileId", fileId)).UniqueResult<FileUpload>();
            }

            var filePath = GetFilePath(path, file.FileId, file.FileNames);
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
        /// The finish upload file.
        /// </summary>
        /// <param name="fileUploadId">
        /// The file upload id.
        /// </param>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <returns>
        /// The <see cref="int?"/>.
        /// </returns>
        public int? FinishUploadFile(int fileUploadId, string filePath)
        {
            FileUpload fileUpload;
            using (var session = NHibernateHelper.OpenSession())
            {
                fileUpload = session.CreateCriteria(typeof(FileUpload)).Add(Restrictions.Eq("FileId", fileUploadId)).UniqueResult<FileUpload>();
            }

            var fileInfo = new FileInfo(GetFilePath(filePath, fileUpload.FileId, fileUpload.FileNames));

            if (!fileUpload.FileSize.Equals(fileInfo.Length))
            {
                return null;
            }

            var file = new Files
            {
                DateAttach = DateTime.Now,
                FileExtension = fileUpload.FileExtension,
                FileNames = fileUpload.FileNames,
                FolderId = fileUpload.FolderId,
                FileSize = fileUpload.FileSize,
                Permission = fileUpload.Permission,
                RepositoryId = fileUpload.RepositoryId
            };

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var fileId = (int)session.Save(file);
                    transaction.Commit();
                    File.Move(fileInfo.FullName, string.Format("{0}\\{1}-{2}", fileInfo.DirectoryName, fileId, file.FileNames));
                    return fileId;
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
        public FileStream DownloadFile(string path, int fileId)
        {
            Files files;
            using (var session = NHibernateHelper.OpenSession())
            {
                files = session.CreateCriteria(typeof(Files)).Add(Restrictions.Eq("FileId", fileId)).UniqueResult<Files>();
            }

            var filePath = GetFilePath(path, files.FileId, files.FileNames);

            return File.Exists(filePath) ? File.Open(filePath, FileMode.Open) : Stream.Null as FileStream;
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
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var sharedFileList =
                        session.CreateCriteria(typeof(SharedFile))
                            .Add(Restrictions.Eq("FileId", fileId))
                            .List<SharedFile>();

                    foreach (var sharedFile in sharedFileList)
                    {
                        session.Delete(sharedFile);
                    }

                    transaction.Commit();

                    files =
                        session.CreateCriteria(typeof(Files))
                            .Add(Restrictions.Eq("FileId", fileId))
                            .UniqueResult<Files>();

                    session.Delete(files);
                    transaction.Commit();
                }
            }

            var filePath = GetFilePath(path, files.FileId, files.FileNames);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// The delete folder.
        /// </summary>
        /// <param name="folderId">
        /// The folder id.
        /// </param>
        /// <param name="path">
        /// The path.
        /// </param>
        public void DeleteFolder(int folderId, string path)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    this.DeleteFolder(folderId, path, session);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// The delete folder.
        /// </summary>
        /// <param name="folderId">
        /// The folder id.
        /// </param>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="session">
        /// The session.
        /// </param>
        private void DeleteFolder(int folderId, string path, ISession session)
        {
            var fileList =
                session.CreateCriteria<Files>().Add(Restrictions.Eq("FolderId", folderId)).List<Files>();

            foreach (var file in fileList)
            {
                this.DeleteFile(path, file.FileId, session);
            }

            var folderList = session.CreateCriteria<Folder>().Add(Restrictions.Eq("ParrentId", folderId)).List<Folder>();

            foreach (var folderItem in folderList)
            {
                this.DeleteFolder(folderItem.FolderId, path, session);
            }

            var sharedFolderList =
                session.CreateCriteria(typeof(SharedFolder))
                    .Add(Restrictions.Eq("FolderId", folderId))
                    .List<SharedFolder>();

            foreach (var sharedFolder in sharedFolderList)
            {
                session.Delete(sharedFolder);
            }

            var folder = session.CreateCriteria<Folder>().Add(Restrictions.Eq("FolderId", folderId)).UniqueResult<Folder>();
            session.Delete(folder);
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
        /// <param name="session">
        /// The session.
        /// </param>
        private void DeleteFile(string path, int fileId, ISession session)
        {
            var sharedFileList =
                session.CreateCriteria(typeof(SharedFile))
                    .Add(Restrictions.Eq("FileId", fileId))
                    .List<SharedFile>();

            foreach (var sharedFile in sharedFileList)
            {
                session.Delete(sharedFile);
            }

            var files = session.CreateCriteria(typeof(Files))
                .Add(Restrictions.Eq("FileId", fileId))
                .UniqueResult<Files>();

            session.Delete(files);

            var filePath = GetFilePath(path, files.FileId, files.FileNames);
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
        /// <param name="fileId">
        /// The file id.
        /// </param>
        /// <param name="fileNames">
        /// The file names.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetFilePath(string path, int fileId, string fileNames)
        {
            var fileName = string.Format("{0}-{1}", fileId, fileNames);
            return path + fileName;
        }
    }
}
