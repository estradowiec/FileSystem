
namespace FileSystemDAL.Tests
{
    using System;

    using FileSystemDAL.Helper;
    using FileSystemDAL.Models;

    using NHibernate;
    using NHibernate.Criterion;

    using NUnit.Framework;

    /// <summary>
    /// The repository test.
    /// </summary>
    [TestFixture]
    public class RepositoryTest
    {
        /// <summary>
        /// The test repository.
        /// </summary>
        private Repository testRepository;

        /// <summary>
        /// The init.
        /// </summary>
        [SetUp]
        public void Init()
        {
            this.testRepository = new Repository
                                      {
                                          RepositoryName = "Anonymus",
                                          DateAttach = DateTime.Now,
                                          IsActive = false
                                      };

            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    this.testRepository.RepositoryId = (int)session.Save(this.testRepository);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// The can add new repository.
        /// </summary>
        [Test]
        public void CanAddNewRepository()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var repositoryFromDb = session.CreateCriteria(typeof(Repository))
                    .Add(Restrictions.Eq("RepositoryName", this.testRepository.RepositoryName))
                    .UniqueResult<Repository>();

                Assert.IsNotNull(repositoryFromDb);
                Assert.AreNotSame(this.testRepository, repositoryFromDb);
                Assert.AreEqual(this.testRepository.IsActive, repositoryFromDb.IsActive);
                Assert.AreEqual(this.testRepository.RepositoryName, repositoryFromDb.RepositoryName);
            }
        }

        /// <summary>
        /// The person test cleanup.
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var repositoryFromDb =
                        session.CreateCriteria(typeof(Repository))
                            .Add(Restrictions.Eq("RepositoryName", this.testRepository.RepositoryName))
                            .UniqueResult<Repository>();

                    session.Delete(repositoryFromDb);
                    transaction.Commit();
                }
            }
        }
    }
}
