
namespace FileSystem.Controllers
{
    using System.Web.Mvc;

    using FileSystemDAL.Manage;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// The repository management controller.
    /// </summary>
    public class RepositoryManagementController : Controller
    {
        /// <summary>
        /// The admin.
        /// </summary>
        private readonly Admin admin;

        /// <summary>
        /// The authorization.
        /// </summary>
        private readonly Authorization authorization;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryManagementController"/> class.
        /// </summary>
        public RepositoryManagementController()
        {
            this.admin = new Admin();
            this.authorization = new Authorization();
        }

        /// <summary>
        /// GET: /RepositoryManagement/
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = this.authorization.GetPerson(int.Parse(User.Identity.GetUserId()));
                if (user.Permission > 0)
                {
                    return this.RedirectToAction("Index", "Home");
                }

                var listRepository = this.admin.GetListRepository();
                return this.View(listRepository);
            }

            return this.RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// The active repository.
        /// </summary>
        /// <param name="idRepository">
        /// The id repository.
        /// </param>
        /// <param name="isActive">
        /// The is active.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult ActiveRepository(int idRepository, bool isActive)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = this.authorization.GetPerson(int.Parse(User.Identity.GetUserId()));
                if (user.Permission > 0)
                {
                    return this.RedirectToAction("Index", "Home");
                }

                if (isActive)
                {
                    this.admin.ActiveRepository(idRepository);
                }
                else
                {
                    this.admin.DeactiveRepository(idRepository);
                }

                return this.Json(new { Success = true });
            }

            return this.RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// The delete repository.
        /// </summary>
        /// <param name="idRepository">
        /// The id repository.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult DeleteRepository(int idRepository)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Login", "Account");
            }

            var user = this.authorization.GetPerson(int.Parse(this.User.Identity.GetUserId()));
            if (user.Permission > 0)
            {
                return this.RedirectToAction("Index", "Home");
            }

            this.admin.DeleteRepository(idRepository);

            return this.Json(new { Success = true });
        }
    }
}