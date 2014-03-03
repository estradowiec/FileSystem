
namespace FileSystemDAL.Tests
{
    using System;

    using FileSystemDAL.Helper;
    using FileSystemDAL.Manage;
    using FileSystemDAL.Models;

    using NHibernate;

    using NUnit.Framework;

    /// <summary>
    /// The partnership manager test.
    /// </summary>
    [TestFixture]
    public class PartnershipManagerTest
    {
        /// <summary>
        /// The repository relating from.
        /// </summary>
        private Repository repositoryRelatingFrom;

        /// <summary>
        /// The repository relating to.
        /// </summary>
        private Repository repositoryRelatingTo;

        /// <summary>
        /// The partnership manager from.
        /// </summary>
        private PartnershipManager partnershipManagerFrom;

        /// <summary>
        /// The partnership manager to.
        /// </summary>
        private PartnershipManager partnershipManagerTo;

        /// <summary>
        /// The init.
        /// </summary>
        [SetUp]
        public void Init()
        {
            this.repositoryRelatingFrom = new Repository
                                              {
                                                  DateAttach = DateTime.Now,
                                                  IsActive = true,
                                                  RepositoryName = "Anonymus1"
                                              };
            this.repositoryRelatingTo = new Repository
                                            {
                                                DateAttach = DateTime.Now,
                                                IsActive = true,
                                                RepositoryName = "Anonymus2"
                                            };
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    this.repositoryRelatingFrom.RepositoryId = (int)session.Save(this.repositoryRelatingFrom);
                    this.repositoryRelatingTo.RepositoryId = (int)session.Save(this.repositoryRelatingTo);
                    transaction.Commit();
                }
            }

            this.partnershipManagerFrom = new PartnershipManager(this.repositoryRelatingFrom);
            this.partnershipManagerTo = new PartnershipManager(this.repositoryRelatingTo);
        }

        /// <summary>
        /// The cleanup.
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(this.repositoryRelatingFrom);
                    session.Delete(this.repositoryRelatingTo);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// The invite friend test.
        /// </summary>
        [Test]
        public void InviteFriendTest()
        {
            this.partnershipManagerFrom.InviteFriend(this.repositoryRelatingTo.RepositoryId);

            var invitation = this.partnershipManagerFrom.GetInvitation();
            var invitedFriend = invitation.Find(x => x.RepositoryId == this.repositoryRelatingTo.RepositoryId);
            Assert.IsNull(invitedFriend);

            invitation = this.partnershipManagerTo.GetInvitation();
            invitedFriend = invitation.Find(x => x.RepositoryId == this.repositoryRelatingFrom.RepositoryId);
            Assert.IsNotNull(invitedFriend);
            Assert.AreEqual(invitedFriend.RepositoryName, this.repositoryRelatingFrom.RepositoryName);

            this.partnershipManagerFrom.RemoveFriend(this.repositoryRelatingTo.RepositoryId);
        }

        /// <summary>
        /// The accept invitation test.
        /// </summary>
        [Test]
        public void AcceptInvitationTest()
        {
            this.partnershipManagerFrom.InviteFriend(this.repositoryRelatingTo.RepositoryId);
            this.partnershipManagerTo.AcceptInvitation(this.repositoryRelatingFrom.RepositoryId);

            var invitation = this.partnershipManagerTo.GetFriends();
            var invitedFriend = invitation.Find(x => x.RepositoryId == this.repositoryRelatingFrom.RepositoryId);
            Assert.IsNotNull(invitedFriend);
            Assert.AreEqual(invitedFriend.RepositoryName, this.repositoryRelatingFrom.RepositoryName);

            this.partnershipManagerFrom.RemoveFriend(this.repositoryRelatingTo.RepositoryId);
        }

        /// <summary>
        /// The remove friend test.
        /// </summary>
        [Test]
        public void RemoveFriendTest()
        {
            this.partnershipManagerFrom.InviteFriend(this.repositoryRelatingTo.RepositoryId);
            this.partnershipManagerFrom.RemoveFriend(this.repositoryRelatingTo.RepositoryId);

            var invitation = this.partnershipManagerTo.GetInvitation();
            var invitedFriend = invitation.Find(x => x.RepositoryId == this.repositoryRelatingFrom.RepositoryId);

            Assert.IsNull(invitedFriend);

            var allFriends = this.partnershipManagerFrom.GetFriends();
            var friend = allFriends.Find(x => x.RepositoryId == this.repositoryRelatingTo.RepositoryId);

            Assert.IsNull(friend);
        }
    }
}
