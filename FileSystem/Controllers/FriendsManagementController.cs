

namespace FileSystem.Controllers
{
    using System.IO;
    using System.Web.Mvc;
    using System.Web.Routing;
    using FileSystem.Models;
    using FileSystemDAL.Enum;
    using FileSystemDAL.Manage;
    using Microsoft.AspNet.Identity;

    /// <summary>
    /// The friends management controller.
    /// </summary>
    public class FriendsManagementController : Controller
    {
        /// <summary>
        /// The authorization.
        /// </summary>
        private PartnershipManager partnershipManager;

        /// <summary>
        /// The authorization.
        /// </summary>
        private Authorization authorization;

        /// <summary>
        /// GET: /FriendsManagement/
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Login", "Account");
            }

            var user = this.authorization.GetPerson(int.Parse(this.User.Identity.GetUserId()));
            if (user.Permission > (EPermission)1)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        /// <summary>
        /// The search friend.
        /// </summary>
        /// <param name="repositorySearch">
        /// The repository search.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchFriend(RepositorySearch repositorySearch)
        {
            if (ModelState.IsValid)
            {
                repositorySearch.SearchRepositoryList =
                    this.partnershipManager.SearchRepository(repositorySearch.RepositoryName);

                repositorySearch.SentInvitationList = this.partnershipManager.GetSentInvitation();
                repositorySearch.IncomingInvitationList = this.partnershipManager.GetInvitation();
                repositorySearch.AcceptedInvitationList = this.partnershipManager.GetFriends();

                return this.View(repositorySearch);
            }

            return this.View(repositorySearch);
        }

        /// <summary>
        /// The search friend.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult SearchFriend()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Login", "Account");
            }

            var user = this.authorization.GetPerson(int.Parse(this.User.Identity.GetUserId()));
            if (user.Permission > (EPermission)1)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(new RepositorySearch());
        }

        /// <summary>
        /// The invite friend.
        /// </summary>
        /// <param name="repositoryId">
        /// The repository id.
        /// </param>
        /// <param name="searchName">
        /// The search name.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult InviteFriend(int repositoryId, string searchName)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Login", "Account");
            }

            var user = this.authorization.GetPerson(int.Parse(this.User.Identity.GetUserId()));
            if (user.Permission > (EPermission)1)
            {
                return this.RedirectToAction("Index", "Home");
            }

            this.partnershipManager.InviteFriend(repositoryId);

            var repositorySearch = new RepositorySearch();
            repositorySearch.RepositoryName = searchName;
            repositorySearch.SearchRepositoryList = this.partnershipManager.SearchRepository(repositorySearch.RepositoryName);
            repositorySearch.SentInvitationList = this.partnershipManager.GetSentInvitation();
            repositorySearch.IncomingInvitationList = this.partnershipManager.GetInvitation();
            repositorySearch.AcceptedInvitationList = this.partnershipManager.GetFriends();

            return this.PartialView("_SearchResultPartial", repositorySearch);
        }

        /// <summary>
        /// The cancel invite friend.
        /// </summary>
        /// <param name="repositoryId">
        /// The repository id.
        /// </param>
        /// <param name="searchName">
        /// The search string.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult CancelInviteFriend(int repositoryId, string searchName)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Login", "Account");
            }

            var user = this.authorization.GetPerson(int.Parse(this.User.Identity.GetUserId()));
            if (user.Permission > (EPermission)1)
            {
                return this.RedirectToAction("Index", "Home");
            }

            this.partnershipManager.RemoveFriend(repositoryId);

            var repositorySearch = new RepositorySearch();
            repositorySearch.RepositoryName = searchName;
            repositorySearch.SearchRepositoryList = this.partnershipManager.SearchRepository(repositorySearch.RepositoryName);
            repositorySearch.SentInvitationList = this.partnershipManager.GetSentInvitation();
            repositorySearch.IncomingInvitationList = this.partnershipManager.GetInvitation();
            repositorySearch.AcceptedInvitationList = this.partnershipManager.GetFriends();

            return this.PartialView("_SearchResultPartial", repositorySearch);
        }

        /// <summary>
        /// The friends management.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult FriendsList()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Login", "Account");
            }

            var user = this.authorization.GetPerson(int.Parse(this.User.Identity.GetUserId()));
            if (user.Permission > (EPermission)1)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var friendsList = this.partnershipManager.GetFriends();
            return this.View(friendsList);
        }

        /// <summary>
        /// The remove friend.
        /// </summary>
        /// <param name="repositoryId">
        /// The repository id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult RemoveFriend(int repositoryId)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Login", "Account");
            }

            var user = this.authorization.GetPerson(int.Parse(this.User.Identity.GetUserId()));
            if (user.Permission > (EPermission)1)
            {
                return this.RedirectToAction("Index", "Home");
            }

            this.partnershipManager.RemoveFriend(repositoryId);

            return this.Json(new { Success = true });
        }

        /// <summary>
        /// The invitation.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Invitation()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Login", "Account");
            }

            var user = this.authorization.GetPerson(int.Parse(this.User.Identity.GetUserId()));
            if (user.Permission > (EPermission)1)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var invitationList = this.partnershipManager.GetInvitation();
            return this.View(invitationList);
        }

        /// <summary>
        /// The accept invitation.
        /// </summary>
        /// <param name="repositoryId">
        /// The repository id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult AcceptInvitation(int repositoryId)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Login", "Account");
            }

            this.partnershipManager.AcceptInvitation(repositoryId);

            return this.Json(new { Success = true });
        }

        /// <summary>
        /// The deny invitation.
        /// </summary>
        /// <param name="repositoryId">
        /// The repository id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult DenyInvitation(int repositoryId)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Login", "Account");
            }

            this.partnershipManager.RemoveFriend(repositoryId);

            return this.Json(new { Success = true });
        }

        /// <summary>
        /// The initialize.
        /// </summary>
        /// <param name="requestContext">
        /// The request context.
        /// </param>
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            if (!this.User.Identity.IsAuthenticated)
            {
                return;
            }

            this.authorization = new Authorization();
            var user = this.authorization.GetPerson(int.Parse(this.User.Identity.GetUserId()));
            if (user.Permission.Equals(EPermission.RepositoryAdmin))
            {
                this.partnershipManager = new PartnershipManager(this.authorization.GetRepository(user.PersonId));
            }
        }
    }
}