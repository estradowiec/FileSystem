
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
        private const string Path = @"E:\Git\FileSystem\Files\";

        /// <summary>
        /// The person repository.
        /// </summary>
        private AdminRepository personRepository;

        /// <summary>
        /// The friend repository.
        /// </summary>
        private AdminRepository friendRepository;

        /// <summary>
        /// The file id.
        /// </summary>
        private int? fileId;

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
                                         FolderName = "NewFolder",
                                         ParrentId = null,
                                         Permission = EPermission.RepositoryAdmin,
                                         RepositoryId = repository.RepositoryId
                                     };

                    this.folder.FolderId = (int)session.Save(this.folder);
                    transaction.Commit();
                }
            }

            this.fileId = this.personRepository.InitFile("NewFile.jpg", File.OpenRead(string.Format(@"{0}\..\..\TestFile\TestFileAll.jpg", AppDomain.CurrentDomain.BaseDirectory)).Length, this.folder.FolderId, EPermission.RepositoryAdmin);
            for (var i = 1; i <= 10; i++)
            {
                this.personRepository.UploadFile(
                    Path,
                    this.fileId.Value,
                    File.OpenRead(string.Format(@"{0}\..\..\TestFile\TestFile{1}.jpg", AppDomain.CurrentDomain.BaseDirectory, i)));
            }

            this.fileId = this.personRepository.FinishUploadFile(this.fileId.Value, Path);
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
                    .Add(Restrictions.Eq("FileId", this.fileId))
                    .UniqueResult<Files>();
            }

            Assert.AreEqual(file.FileId, this.fileId);
        }

        /// <summary>
        /// The download file test.
        /// </summary>
        [Test]
        public void DownloadFileTest()
        {
            var fileStream = this.personRepository.DownloadFile(Path, this.fileId.Value);
            Assert.AreEqual(fileStream, File.OpenRead(string.Format(@"{0}\..\..\TestFile\TestFileAll.jpg", AppDomain.CurrentDomain.BaseDirectory)));
            fileStream.Close();
        }

        /// <summary>
        /// The share file test.
        /// </summary>
        [Test]
        public void ShareFileTest()
        {
            this.personRepository.PartnershipManager.InviteFriend(this.friendRepository.MyRepository.Repository.RepositoryId);
            this.friendRepository.PartnershipManager.AcceptInvitation(this.personRepository.MyRepository.Repository.RepositoryId);
            this.personRepository.ShareFile(this.fileId.Value, this.friendRepository.MyRepository.Repository.RepositoryId);
            var friendFile = this.friendRepository.MyRepository.GetFiles(this.folder.FolderId, this.personRepository.MyRepository.Repository.RepositoryId).FirstOrDefault();
            this.personRepository.PartnershipManager.RemoveFriend(this.friendRepository.MyRepository.Repository.RepositoryId);
            this.personRepository.UnshareFile(this.fileId.Value, this.friendRepository.MyRepository.Repository.RepositoryId);

            Assert.AreEqual(friendFile.FileId, this.fileId);
        }

        /// <summary>
        /// The cleanup.
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            this.personRepository.DeleteFile(Path, this.fileId.Value);
            Files file;
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    file = session.CreateCriteria(typeof(Files))
                        .Add(Restrictions.Eq("FileId", this.fileId))
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
