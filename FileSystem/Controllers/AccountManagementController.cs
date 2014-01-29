
namespace FileSystem.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.Routing;

    using FileSystem.Models;

    using FileSystemDAL.Enum;
    using FileSystemDAL.Manage;

    using Microsoft.AspNet.Identity;

    /// <summary>
    /// The account management controller.
    /// </summary>
    public class AccountManagementController : Controller
    {
        /// <summary>
        /// The authorization.
        /// </summary>
        private AdminRepository adminRepository;

        /// <summary>
        /// The authorization.
        /// </summary>
        private Authorization authorization;

        /// <summary>
        /// GET: /AccountManagement/
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// The create user.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult CreateUser()
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
        /// The create user.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.adminRepository.ValidateUserAccountAsync(model.Email, model.UserName);
                if (result.Succeeded)
                {
                    this.adminRepository.CreatePersonAccount(
                        model.Email,
                        model.UserName,
                        model.Password,
                        (EPermission)(int)model.UserPermission,
                        this.adminRepository.MyRepository.Repository.RepositoryId);

                    ModelState.Clear();
                    this.TempData["success"] = true;
                    return this.View(model);
                }

                this.AddErrors(result);
            }

            return this.View(model);
        }

        /// <summary>
        /// The user management.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult UserManagement()
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

            var repositoryUsers = this.adminRepository.GetRepositoryUsers();
            return this.View(repositoryUsers);
        }

        /// <summary>
        /// The update user permission.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="permission">
        /// The permission.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult UpdateUserPermission(int userId, EUserPermission permission)
        {
            this.adminRepository.UpdatePersonPermission(userId, (EPermission)(int)permission);

            return this.Json(new { Success = true });
        }

        /// <summary>
        /// The delete user.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult DeleteUser(int userId)
        {
            this.adminRepository.DeletePerson(userId);

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
                this.adminRepository = new AdminRepository(this.authorization.GetRepository(user.PersonId));
            }
        }

        /// <summary>
        /// The add errors.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }
    }
}