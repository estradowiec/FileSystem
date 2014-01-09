
namespace FileSystemDAL.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using FileSystemDAL.Enum;
    using FileSystemDAL.Helper;
    using FileSystemDAL.Manage;
    using FileSystemDAL.Models;

    using NHibernate;
    using NHibernate.Cache;
    using NHibernate.Linq;
    using NHibernate.Mapping;
    using NHibernate.Util;

    using NUnit.Framework;

    /// <summary>
    /// The person test.
    /// </summary>
    [TestFixture]
    public class RepositoryManagerTest
    {
        /// <summary>
        /// The repository manager.
        /// </summary>
        private RepositoryManager repositoryManager;

        /// <summary>
        /// The list file.
        /// </summary>
        private List<Files> listFileRootFolder;

        /// <summary>
        /// The list file.
        /// </summary>
        private List<Files> listFileParrentFolder;

        /// <summary>
        /// The root folder.
        /// </summary>
        private Folder rootFolder;

        /// <summary>
        /// The parrent folder.
        /// </summary>
        private Folder parrentFolder;

        /// <summary>
        /// The inaccessible folder.
        /// </summary>
        private Folder inaccessibleFolder;

        /// <summary>
        /// The init.
        /// </summary>
        [SetUp]
        public void Init()
        {
            this.listFileParrentFolder = new List<Files>();
            this.listFileRootFolder = new List<Files>();
            var repository = new Repository { DateAttach = DateTime.Now, IsActive = true, RepositoryName = "Anonymus" };
            repository.RepositoryId = this.AddRepository(repository);
            this.repositoryManager = new RepositoryManager(repository, EPermission.UserHigh);
            this.rootFolder = new Folder
                             {
                                 DateAttach = DateTime.Now,
                                 FolderName = "Anonym1",
                                 ParrentId = null,
                                 Permission = EPermission.UserHigh,
                                 RepositoryId = repository.RepositoryId
                             };
            this.rootFolder.FolderId = this.AddFolder(this.rootFolder);

            this.inaccessibleFolder = new Folder
            {
                DateAttach = DateTime.Now,
                FolderName = "InAccessible",
                ParrentId = null,
                Permission = EPermission.RepositoryAdmin,
                RepositoryId = repository.RepositoryId
            };
            this.inaccessibleFolder.FolderId = this.AddFolder(this.inaccessibleFolder);

            this.parrentFolder = new Folder
            {
                DateAttach = DateTime.Now,
                FolderName = "Anonym2",
                ParrentId = this.rootFolder.FolderId,
                Permission = EPermission.UserHigh,
                RepositoryId = repository.RepositoryId
            };
            this.parrentFolder.FolderId = this.AddFolder(this.parrentFolder);

            var file1 = new Files
                            {
                                DateAttach = DateTime.Now,
                                RepositoryId = repository.RepositoryId,
                                Permission = EPermission.UserHigh,
                                FileNames = "Anon1",
                                FileExtension = "exe",
                                FileSize = 46,
                                FolderId = this.rootFolder.FolderId
                            };
            file1.FileId = this.AddFile(file1);
            this.listFileRootFolder.Add(file1);

            var file2 = new Files
            {
                DateAttach = DateTime.Now,
                RepositoryId = repository.RepositoryId,
                Permission = EPermission.UserLow,
                FileNames = "Anon2",
                FileExtension = "exe",
                FileSize = 46,
                FolderId = this.rootFolder.FolderId
            };
            file2.FileId = this.AddFile(file2);
            this.listFileRootFolder.Add(file2);

            var file3 = new Files
            {
                DateAttach = DateTime.Now,
                RepositoryId = repository.RepositoryId,
                Permission = EPermission.UserMedium,
                FileNames = "Anon3",
                FileExtension = "exe",
                FileSize = 46,
                FolderId = this.parrentFolder.FolderId
            };
            file3.FileId = this.AddFile(file3);
            this.listFileParrentFolder.Add(file3);

            var file4 = new Files
            {
                DateAttach = DateTime.Now,
                RepositoryId = repository.RepositoryId,
                Permission = EPermission.RepositoryAdmin,
                FileNames = "Anon4",
                FileExtension = "exe",
                FileSize = 46,
                FolderId = this.parrentFolder.FolderId
            };
            file4.FileId = this.AddFile(file4);
            this.listFileParrentFolder.Add(file4);
        }

        /// <summary>
        /// The get files test folder root test.
        /// </summary>
        [Test]
        public void GetFilesTest()
        {
            var rootFiles = this.repositoryManager.GetFiles(this.rootFolder.FolderId);
            Assert.AreEqual(rootFiles.Count(), this.listFileRootFolder.Count);

            foreach (var file in rootFiles)
            {
                if (file.Permission >= this.repositoryManager.Permission)
                {
                    Assert.AreEqual(this.listFileRootFolder.Contains(file), true);
                }
                else
                {
                    Assert.Fail("Get files list contains higher permission");
                }
            }

            var parrentFiles = this.repositoryManager.GetFiles(this.parrentFolder.FolderId);
            Assert.AreEqual(parrentFiles.Count(), this.listFileParrentFolder.Count - 1);

            foreach (var file in parrentFiles)
            {
                if (file.Permission >= this.repositoryManager.Permission)
                {
                    Assert.AreEqual(this.listFileParrentFolder.Contains(file), true);
                }
                else
                {
                    Assert.Fail("Get files list contains higher permission");
                }
            }
        }

        /// <summary>
        /// The get folder test.
        /// </summary>
        [Test]
        public void GetFolderTest()
        {
            var rootFolders = this.repositoryManager.GetFolders(null);
            Assert.AreEqual(rootFolders.Count, 1);

            foreach (var folder in rootFolders)
            {
                if (folder.Permission >= this.repositoryManager.Permission)
                {
                    Assert.AreEqual(folder, this.rootFolder);
                }
                else
                {
                    Assert.Fail("Get folders list contains higher permission");
                }
            }

            var parentFolders = this.repositoryManager.GetFolders(this.rootFolder.FolderId);
            Assert.AreEqual(parentFolders.Count, 1);

            foreach (var folder in parentFolders)
            {
                if (folder.Permission >= this.repositoryManager.Permission)
                {
                    Assert.AreEqual(folder, this.parrentFolder);
                }
                else
                {
                    Assert.Fail("Get folders list contains higher permission");
                }
            }
        }

        /// <summary>
        /// The cleanup.
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    foreach (var file in this.listFileRootFolder)
                    {
                        session.Delete(file);
                    }

                    foreach (var file in this.listFileParrentFolder)
                    {
                        session.Delete(file);
                    }

                    session.Delete(this.inaccessibleFolder);
                    session.Delete(this.parrentFolder);
                    session.Delete(this.rootFolder);
                    session.Delete(this.repositoryManager.Repository);

                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// The add folder.
        /// </summary>
        /// <param name="folder">
        /// The folder.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int AddFolder(Folder folder)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var folderId = (int)session.Save(folder);
                    transaction.Commit();
                    return folderId;
                }
            }
        }

        /// <summary>
        /// The add file.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int AddFile(Files file)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var fileId = (int)session.Save(file);
                    transaction.Commit();
                    return fileId;
                }
            }
        }

        /// <summary>
        /// The add repository.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int AddRepository(Repository repository)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var repositoryIdentity = (int)session.Save(repository);
                    transaction.Commit();
                    return repositoryIdentity;
                }
            }
        }
    }
}
