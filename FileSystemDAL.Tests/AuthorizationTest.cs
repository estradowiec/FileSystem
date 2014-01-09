
namespace FileSystemDAL.Tests
{
    using System;

    using FileSystemDAL.Helper;
    using FileSystemDAL.Manage;
    using FileSystemDAL.Models;

    using NHibernate;
    using NHibernate.Criterion;

    using NUnit.Framework;

    /// <summary>
    /// The person test.
    /// </summary>
    [TestFixture]
    public class AuthorizationTest
    {
        /// <summary>
        /// The test person.
        /// </summary>
        private string personEmail;

        /// <summary>
        /// The password.
        /// </summary>
        private string personPassword;

        /// <summary>
        /// The repository name.
        /// </summary>
        private string repositoryName;

        /// <summary>
        /// The authorization.
        /// </summary>
        private Authorization authorization;

        /// <summary>
        /// The init.
        /// </summary>
        [SetUp]
        public void Init()
        {
            this.personEmail = "anonymus@wp.pl";
            this.personPassword = "anonymus";
            this.repositoryName = "Anonymus";
            this.authorization = new Authorization();
        }

        /// <summary>
        /// The can add new person.
        /// </summary>
        [Test]
        public void LoginFailTest()
        {
            Assert.AreEqual(this.authorization.VerifyEmail(this.personEmail), false);
            Assert.AreEqual(this.authorization.VerifyPerson(this.personEmail, this.personPassword), false);
        }

        /// <summary>
        /// The login success test.
        /// </summary>
        [Test]
        public void LoginSuccessTest()
        {
            this.authorization.RegisterPerson(this.personEmail, "Name", this.personPassword, this.repositoryName);

            Assert.AreEqual(this.authorization.VerifyEmail(this.personEmail), true);
            Assert.AreEqual(this.authorization.VerifyPerson(this.personEmail, this.personPassword), true);

            this.Cleanup();
        }

        /// <summary>
        /// The registration test.
        /// </summary>
        [Test]
        public void RegistrationTest()
        {
            this.authorization.RegisterPerson(this.personEmail, "Name", this.personPassword, this.repositoryName);
            Assert.AreEqual(this.authorization.VerifyEmail(this.personEmail), true);
            Assert.AreEqual(this.authorization.VerifyPerson(this.personEmail, this.personPassword), true);
            Assert.AreEqual(this.authorization.VerifyRepository(this.repositoryName), true);
            this.Cleanup();
        }

        /// <summary>
        /// The cleanup.
        /// </summary>
        public void Cleanup()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var personFromDb =
                        session.CreateCriteria(typeof(Person))
                            .Add(Restrictions.Eq("Email", this.personEmail))
                            .UniqueResult<Person>();

                    var repositoryFromDb =
                        session.CreateCriteria(typeof(Repository))
                            .Add(Restrictions.Eq("RepositoryName", this.repositoryName))
                            .UniqueResult<Repository>();

                    session.Delete(personFromDb);
                    session.Delete(repositoryFromDb);
                    transaction.Commit();
                }
            }
        }
    }
}
