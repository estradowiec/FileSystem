
namespace FileSystemDAL.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Security;
    using System.Transactions;

    using FileSystemDAL.Enum;
    using FileSystemDAL.Helper;
    using FileSystemDAL.Manage;
    using FileSystemDAL.Models;

    using NHibernate;
    using NHibernate.Criterion;

    using NUnit.Framework;

    /// <summary>
    /// The person repository test.
    /// </summary>
    [TestFixture]
    public class PersonRepositoryTest
    {
        /// <summary>
        /// The path.
        /// </summary>
        private const string Path = @"E:\Programowanie\ASP .NET\FileSystem\Files\";

        /// <summary>
        /// The person repository.
        /// </summary>
        private AdminRepository personRepository;

        /// <summary>
        /// The friend repository.
        /// </summary>
        private AdminRepository friendRepository;

        /// <summary>
        /// The files.
        /// </summary>
        private Files files;

        /// <summary>
        /// The folder.
        /// </summary>
        private Folder folder;

        /// <summary>
        /// The init.
        /// </summary>
        [SetUp]
        public void Init()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var repository = new Repository { DateAttach = DateTime.Now, IsActive = true, RepositoryName = "Anonymus1" };
                    var friendRep = new Repository { DateAttach = DateTime.Now, IsActive = true, RepositoryName = "Anonymus2" };
                    repository.RepositoryId = (int)session.Save(repository);
                    friendRep.RepositoryId = (int)session.Save(friendRep);
                    this.personRepository = new AdminRepository(repository);
                    this.friendRepository = new AdminRepository(friendRep);
                    this.folder = new Folder
                                     {
                                         DateAttach = DateTime.Now,
                                         FolderName = "NewFolder.exe",
                                         ParrentId = null,
                                         Permission = EPermission.RepositoryAdmin,
                                         RepositoryId = repository.RepositoryId
                                     };

                    this.folder.FolderId = (int)session.Save(this.folder);
                    transaction.Commit();
                }
            }
            
            this.files = this.personRepository.InitFile("NewFolder.exe", 1000, this.folder.FolderId, EPermission.RepositoryAdmin);
            for (var i = 1; i <= 10; i++)
            {
                this.personRepository.UploadFile(
                    Path,
                    this.files.FileId,
                    File.OpenRead(string.Format(@"{0}\..\..\TestFile\TestFile{1}.jpg", AppDomain.CurrentDomain.BaseDirectory, i)));
            }
        }

        /// <summary>
        /// The insert file test.
        /// </summary>
        [Test]
        public void UploadFileTest()
        {
            Files file;
            using (var session = NHibernateHelper.OpenSession())
            {
                file = session.CreateCriteria(typeof(Files))
                    .Add(Restrictions.Eq("FileId", this.files.FileId))
                    .UniqueResult<Files>();
            }

            Assert.AreEqual(file.FileId, this.files.FileId);
        }

        /// <summary>
        /// The download file test.
        /// </summary>
        [Test]
        public void DownloadFileTest()
        {
            var fileStream = this.personRepository.DownloadFile(Path, this.files.FileId);
            Assert.AreEqual(fileStream, File.OpenRead(string.Format(@"{0}\..\..\TestFile\TestFileAll.jpg", AppDomain.CurrentDomain.BaseDirectory)));
            fileStream.Close();
        }

        /// <summary>
        /// The share file test.
        /// </summary>
        [Test]
        public void ShareFileTest()
        {
            this.personRepository.PartnershipManager.InviteFriend(this.friendRepository.MyRepository.Repository);
            this.friendRepository.AcceptInvitation(this.personRepository.MyRepository.Repository);
            this.personRepository.ShareFile(this.files.FileId, this.friendRepository.MyRepository.Repository.RepositoryId);
            var friendFile = this.friendRepository.MyRepository.GetFiles(this.folder, this.personRepository.MyRepository.Repository).FirstOrDefault();
            this.personRepository.RemoveFriend(this.friendRepository.MyRepository.Repository);
            this.personRepository.UnsharedFile(this.files.FileId, this.friendRepository.MyRepository.Repository.RepositoryId);
            
            Assert.AreEqual(friendFile.FileId, this.files.FileId);
            Assert.AreEqual(friendFile.FileNames, this.files.FileNames);
            Assert.AreEqual(friendFile.FileSize, this.files.FileSize);
            Assert.AreEqual(friendFile.FolderId, this.files.FolderId);
            Assert.AreEqual(friendFile.Permission, this.files.Permission);
            Assert.AreEqual(friendFile.RepositoryId, this.files.RepositoryId);
        }

        /// <summary>
        /// The cleanup.
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            this.personRepository.DeleteFile(Path, this.files.FileId);
            Files file;
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    file = session.CreateCriteria(typeof(Files))
                        .Add(Restrictions.Eq("FileId", this.files.FileId))
                        .UniqueResult<Files>();
                    session.Delete(this.folder);
                    session.Delete(this.personRepository.MyRepository.Repository);
                    session.Delete(this.friendRepository.MyRepository.Repository);
                    transaction.Commit();
                }
            }

            Assert.IsNull(file);
        }
    }
}
